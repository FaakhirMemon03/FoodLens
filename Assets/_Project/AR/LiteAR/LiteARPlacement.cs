using UnityEngine;

namespace FoodLens.AR.LiteAR
{
    /// <summary>
    /// Handles simple tap-to-place mechanics for Lite AR mode.
    /// Spawns the food item at a fixed floor height relative to the main camera,
    /// bypassing active plane recalculations.
    /// </summary>
    public class LiteARPlacement : MonoBehaviour
    {
        [Tooltip("The selected 3D food prefab to instantiate.")]
        public GameObject foodPrefab;

        [Tooltip("Height offset from the camera's initial position representing the ground.")]
        public float spawnHeightOffset = -1.2f;

        [Tooltip("Distance in front of the camera to spawn the item.")]
        public float placementDistance = 1.5f;

        private GameObject spawnedObject;

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

                PlaceFoodObject();
            }
        }

        private void PlaceFoodObject()
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
            Debug.Log("[FoodLens Lite] Placing food item on simulated plane offset.");

            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                Vector3 cameraPosition = mainCam.transform.position;
                Vector3 forwardHorizontal = mainCam.transform.forward;
                forwardHorizontal.y = 0; // Lock vertical tilt.
                forwardHorizontal.Normalize();

                // Compute position on the horizontal offset floor.
                Vector3 spawnPosition = cameraPosition + (forwardHorizontal * placementDistance);
                spawnPosition.y = cameraPosition.y + spawnHeightOffset;

                Quaternion spawnRotation = Quaternion.LookRotation(forwardHorizontal);

                spawnedObject = Instantiate(foodPrefab, spawnPosition, spawnRotation);
            }
        }
    }
}
