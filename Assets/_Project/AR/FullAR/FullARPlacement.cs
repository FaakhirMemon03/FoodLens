using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace FoodLens.AR.FullAR
{
    /// <summary>
    /// Handles ARCore horizontal surface detection and tap-to-place anchoring with haptic feedback,
    /// ensuring only one active food model is present in the scene.
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    [RequireComponent(typeof(ARPlaneManager))]
    [RequireComponent(typeof(ARAnchorManager))]
    public class FullARPlacement : MonoBehaviour
    {
        [Tooltip("The selected 3D food prefab to instantiate.")]
        public GameObject foodPrefab;
        
        private GameObject spawnedObject;
        private ARRaycastManager raycastManager;
        private ARAnchorManager anchorManager;
        private ARPlaneManager planeManager;
        private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

        private void Awake()
        {
            raycastManager = GetComponent<ARRaycastManager>();
            anchorManager = GetComponent<ARAnchorManager>();
            planeManager = GetComponent<ARPlaneManager>();
        }

        private void Update()
        {
            if (Input.touchCount == 0) return;

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Prevent placing objects if tapping UI buttons.
                if (UnityEngine.EventSystems.EventSystem.current != null &&
                    UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
                }

                // Raycast against detected planes.
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    ARPlane plane = planeManager.GetPlane(hits[0].trackableId);

                    // Restrict placement exclusively to horizontal upward surfaces (tables, countertops).
                    if (plane != null && plane.alignment == PlaneAlignment.HorizontalUpward)
                    {
                        PlaceFoodObject(hitPose);
                    }
                }
            }
        }

        private void PlaceFoodObject(Pose hitPose)
        {
            // Maintain single active object rule: destroy previously placed item.
            if (spawnedObject != null)
            {
                Destroy(spawnedObject);
            }

            // Trigger short haptic vibration.
#if UNITY_ANDROID && !UNITY_EDITOR
            Handheld.Vibrate();
#endif
            Debug.Log("[FoodLens] Horizontal upward plane hit. Placing food item.");

            // Instantiate and orient.
            spawnedObject = Instantiate(foodPrefab, hitPose.position, hitPose.rotation);

            // Anchor the food object to maintain reference stability.
            ARAnchor anchor = anchorManager.AddAnchor(new Pose(hitPose.position, hitPose.rotation));
            if (anchor != null)
            {
                spawnedObject.transform.SetParent(anchor.transform);
            }
        }
    }
}
