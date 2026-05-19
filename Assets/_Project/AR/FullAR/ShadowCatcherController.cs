using UnityEngine;

namespace FoodLens.AR.FullAR
{
    /// <summary>
    /// Manages the activation and physical positioning of a transparent shadow catcher plane
    /// directly beneath placed virtual food objects to display realistic ground shadows.
    /// </summary>
    public class ShadowCatcherController : MonoBehaviour
    {
        [Tooltip("The flat shadow-receiving plane GameObject reference.")]
        public Transform shadowPlane;

        /// <summary>
        /// Positions and activates the shadow catcher plane directly under the target position.
        /// </summary>
        public void AlignShadowCatcher(Vector3 targetPosition)
        {
            if (shadowPlane != null)
            {
                // Align the shadow catcher plane horizontally directly under the object's base pivot.
                shadowPlane.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
                shadowPlane.rotation = Quaternion.Euler(90f, 0f, 0f); // Ensure the plane is flat.
                shadowPlane.gameObject.SetActive(true);
                Debug.Log($"[FoodLens] Shadow catcher plane aligned at position: {shadowPlane.position}");
            }
        }

        /// <summary>
        /// Deactivates the shadow catcher plane (e.g. when food items are cleared).
        /// </summary>
        public void HideShadowCatcher()
        {
            if (shadowPlane != null)
            {
                shadowPlane.gameObject.SetActive(false);
            }
        }
    }
}
