using UnityEngine;

namespace FoodLens.AR.FakeAR
{
    /// <summary>
    /// Handles user gesture interaction in Fake AR Mode.
    /// Supports:
    /// - One-finger drag to rotate on the Y-axis.
    /// - Long-press (>0.5s) followed by drag to translate/reposition in screen-space.
    /// - Two-finger pinch to scale between 0.8x and 1.2x.
    /// - Positions a blob shadow beneath the active object.
    /// </summary>
    public class FakeARController : MonoBehaviour
    {
        public Transform foodObject;
        public Transform blobShadow;
        public float placementDistance = 1.5f;
        public float translationSensitivity = 0.003f;
        public float rotationSensitivity = 0.25f;

        private float defaultScale = 1.0f;
        private float currentScaleMultiplier = 1.0f;
        private float minScale = 0.8f;
        private float maxScale = 1.2f;

        private float touchTime = 0f;
        private bool isLongPress = false;
        private const float LONG_PRESS_DURATION = 0.5f;

        private void Start()
        {
            if (foodObject != null)
            {
                Camera mainCam = Camera.main;
                if (mainCam != null)
                {
                    // Place 1.5 meters in front of the camera viewport.
                    foodObject.position = mainCam.transform.position + mainCam.transform.forward * placementDistance;
                    
                    // Rotate to face camera on Y plane.
                    Vector3 lookDir = foodObject.position - mainCam.transform.position;
                    lookDir.y = 0; // lock vertical tilt
                    if (lookDir != Vector3.zero)
                    {
                        foodObject.rotation = Quaternion.LookRotation(lookDir);
                    }
                }
                defaultScale = foodObject.localScale.x;
            }
        }

        private void Update()
        {
            if (foodObject == null) return;

            int touchesCount = Input.touchCount;

            if (touchesCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchTime = 0f;
                    isLongPress = false;
                }
                else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    touchTime += Time.deltaTime;
                    if (touchTime >= LONG_PRESS_DURATION && !isLongPress)
                    {
                        isLongPress = true;
                        
                        // Trigger short haptic vibration on Android devices if supported.
#if UNITY_ANDROID && !UNITY_EDITOR
                        Handheld.Vibrate();
#endif
                        Debug.Log("[FoodLens] Long-press detected. Switching to Reposition mode.");
                    }
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    if (isLongPress)
                    {
                        // Reposition mode: Translate object relative to camera perspective.
                        Vector3 translation = new Vector3(touch.deltaPosition.x, touch.deltaPosition.y, 0) * translationSensitivity;
                        Camera mainCam = Camera.main;
                        if (mainCam != null)
                        {
                            Vector3 rightMovement = mainCam.transform.right * translation.x;
                            Vector3 upMovement = mainCam.transform.up * translation.y;
                            foodObject.position += rightMovement + upMovement;
                        }
                    }
                    else
                    {
                        // Rotation mode: One-finger drag rotates 360° on Y-axis.
                        float rotationAngle = touch.deltaPosition.x * rotationSensitivity;
                        foodObject.Rotate(Vector3.up, -rotationAngle, Space.World);
                    }
                }
            }
            else if (touchesCount == 2)
            {
                // Scaling mode: Two-finger pinch scales between 0.8x and 1.2x.
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                Vector2 prevPos0 = touch0.position - touch0.deltaPosition;
                Vector2 prevPos1 = touch1.position - touch1.deltaPosition;

                float prevDistance = Vector2.Distance(prevPos0, prevPos1);
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);

                float distanceDelta = currentDistance - prevDistance;

                currentScaleMultiplier += distanceDelta * 0.005f;
                currentScaleMultiplier = Mathf.Clamp(currentScaleMultiplier, minScale, maxScale);

                foodObject.localScale = Vector3.one * (defaultScale * currentScaleMultiplier);
            }

            // Sync the blob shadow position/scale underneath the food object.
            if (blobShadow != null)
            {
                blobShadow.position = foodObject.position + Vector3.down * 0.15f;
                blobShadow.rotation = Quaternion.Euler(90, 0, 0); // lay flat
                blobShadow.localScale = new Vector3(1.2f, 1.2f, 1.2f) * currentScaleMultiplier;
            }
        }
    }
}
