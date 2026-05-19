using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

namespace FoodLens.AR
{
    /// <summary>
    /// Implements touch gestures for placing and manipulating 3D food items:
    /// - Swipe left/right: 360° rotation on the Y-axis.
    /// - Pinch: scaling restricted between 0.8x and 1.2x.
    /// - Long-press (>0.5s): transition to repositioning/dragging along active planes.
    /// - Double-tap: deletion trigger.
    /// </summary>
    public class InteractionHandler : MonoBehaviour
    {
        [Tooltip("The raycast manager used to translate screen touches to horizontal plane positions.")]
        public ARRaycastManager raycastManager;

        private GameObject selectedObject;
        private float defaultScale = 1.0f;
        private float currentScaleMultiplier = 1.0f;
        private float minScale = 0.8f;
        private float maxScale = 1.2f;

        private float touchTime = 0f;
        private bool isLongPress = false;
        private const float LONG_PRESS_DURATION = 0.5f;

        private float lastTapTime = 0f;
        private const float DOUBLE_TAP_TIMEOUT = 0.3f;

        private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

        private void Update()
        {
            int touchesCount = Input.touchCount;
            if (touchesCount == 0) return;

            if (touchesCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchTime = 0f;
                    isLongPress = false;

                    // Raycast to check if user tapped a food item collider
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("FoodItem"))
                        {
                            selectedObject = hit.collider.gameObject;
                            defaultScale = selectedObject.transform.localScale.x / currentScaleMultiplier;

                            // Detect double-tap deletion request
                            if (Time.time - lastTapTime < DOUBLE_TAP_TIMEOUT)
                            {
                                RequestObjectDeletion(selectedObject);
                            }
                            lastTapTime = Time.time;
                        }
                        else
                        {
                            selectedObject = null;
                        }
                    }
                    else
                    {
                        selectedObject = null;
                    }
                }

                if (selectedObject == null) return;

                // Detect long-press drag trigger
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    touchTime += Time.deltaTime;
                    if (touchTime >= LONG_PRESS_DURATION && !isLongPress)
                    {
                        isLongPress = true;
                        
#if UNITY_ANDROID && !UNITY_EDITOR
                        Handheld.Vibrate();
#endif
                        Debug.Log("[FoodLens] Long-press active. Dragging model across surfaces.");
                    }
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    if (isLongPress && raycastManager != null)
                    {
                        // Reposition object by raycasting to horizontal planes
                        if (raycastManager.Raycast(touch.position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                        {
                            Pose hitPose = hits[0].pose;
                            Transform targetTransform = selectedObject.transform.parent != null ? selectedObject.transform.parent : selectedObject.transform;
                            targetTransform.position = hitPose.position;
                        }
                    }
                    else
                    {
                        // Standard swipe performs 360-degree rotation along Y-axis
                        float rotationAngle = touch.deltaPosition.x * 0.25f;
                        selectedObject.transform.Rotate(Vector3.up, -rotationAngle, Space.World);
                    }
                }
            }
            else if (touchesCount == 2 && selectedObject != null)
            {
                // Pinch to scale
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                Vector2 prevPos0 = touch0.position - touch0.deltaPosition;
                Vector2 prevPos1 = touch1.position - touch1.deltaPosition;

                float prevDistance = Vector2.Distance(prevPos0, prevPos1);
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);

                float distanceDelta = currentDistance - prevDistance;

                currentScaleMultiplier += distanceDelta * 0.005f;
                currentScaleMultiplier = Mathf.Clamp(currentScaleMultiplier, minScale, maxScale);

                selectedObject.transform.localScale = Vector3.one * (defaultScale * currentScaleMultiplier);
            }
        }

        private void RequestObjectDeletion(GameObject obj)
        {
            Debug.Log("[FoodLens] Confirming double-tap object deletion.");
            
            // Delete anchor root if spawned with ARAnchor, otherwise delete mesh
            if (obj.transform.parent != null && obj.transform.parent.GetComponent<ARAnchor>() != null)
            {
                Destroy(obj.transform.parent.gameObject);
            }
            else
            {
                Destroy(obj);
            }
            
            selectedObject = null;
        }
    }
}
