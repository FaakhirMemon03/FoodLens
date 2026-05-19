using UnityEngine;
using UnityEngine.SceneManagement;
using FoodLens.Core.ModeDetection;

namespace FoodLens.Core.Managers
{
    /// <summary>
    /// Tracks system frame rate at runtime. If the frame rate stays below 30 FPS
    /// for 3 consecutive seconds in Full AR mode, it triggers an automatic downgrade
    /// to Lite AR mode, saving placement coordinates to prevent progress loss.
    /// </summary>
    public class PerformanceMonitor : MonoBehaviour
    {
        [Tooltip("The frame rate boundary below which the system registers a slowdown.")]
        public float fpsThreshold = 30f;

        [Tooltip("Number of seconds low performance is tolerated before fallback triggers.")]
        public float durationThreshold = 3.0f;

        private float fpsBuffer = 60f;
        private float lowFpsTimer = 0f;
        private const int FRAME_RANGE = 10;
        private int frameCount = 0;
        private float accumulatedTime = 0f;

        // Static parameters to preserve placed item position/rotation across scene switches.
        public static bool HasPersistedObject { get; private set; } = false;
        public static Vector3 PersistedPosition { get; private set; }
        public static Quaternion PersistedRotation { get; private set; }
        public static string PersistedPrefabName { get; private set; }

        private void Update()
        {
            // Only run performance evaluations in FullAR mode.
            if (ModeManager.Instance == null || ModeManager.Instance.CurrentMode != ARMode.FullAR)
                return;

            frameCount++;
            accumulatedTime += Time.unscaledDeltaTime;

            // Update average FPS every 10 frames to avoid micro-stutter noise.
            if (frameCount >= FRAME_RANGE)
            {
                fpsBuffer = frameCount / accumulatedTime;
                
                EvaluatePerformance(fpsBuffer);

                frameCount = 0;
                accumulatedTime = 0f;
            }
        }

        private void EvaluatePerformance(float currentFps)
        {
            if (currentFps < fpsThreshold)
            {
                lowFpsTimer += accumulatedTime;
                if (lowFpsTimer >= durationThreshold)
                {
                    TriggerDowngrade();
                }
            }
            else
            {
                // Reset duration clock if frame rate recovers.
                lowFpsTimer = 0f;
            }
        }

        private void TriggerDowngrade()
        {
            Debug.LogWarning($"[FoodLens Performance] Performance dropped to {fpsBuffer:F1} FPS. Executing downgrade fallback to LiteAR.");

            // Cache active food position and details.
            GameObject activeFood = GameObject.FindWithTag("FoodItem");
            if (activeFood != null)
            {
                HasPersistedObject = true;
                PersistedPosition = activeFood.transform.position;
                PersistedRotation = activeFood.transform.rotation;
                PersistedPrefabName = activeFood.name.Replace("(Clone)", "").Trim();
            }

            // Route scene thread.
            SceneManager.LoadScene("LiteAR");
        }

        /// <summary>
        /// Resets temporary persistence parameters after they have been restored in the new scene.
        /// </summary>
        public static void ClearPersistedState()
        {
            HasPersistedObject = false;
            PersistedPrefabName = null;
        }
    }
}
