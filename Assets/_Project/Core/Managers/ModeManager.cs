using UnityEngine;
using FoodLens.Core.ModeDetection;

namespace FoodLens.Core.Managers
{
    /// <summary>
    /// Global persistent manager that tracks the active AR mode across scene changes.
    /// </summary>
    public class ModeManager : MonoBehaviour
    {
        public static ModeManager Instance { get; private set; }

        /// <summary>
        /// Retrieves the current active AR Mode determined at startup.
        /// </summary>
        public ARMode CurrentMode => ModeDetectionSystem.CurrentMode;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
