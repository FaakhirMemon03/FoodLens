using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace FoodLens.Core.ModeDetection
{
    /// <summary>
    /// Checks ARCore availability and runs a device capability scoring system
    /// to assign the application rendering mode (FullAR, LiteAR, FakeAR).
    /// </summary>
    public class ModeDetectionSystem : MonoBehaviour
    {
        public static ARMode CurrentMode { get; private set; } = ARMode.FakeAR;
        public static bool IsDetectionComplete { get; private set; } = false;

        public DeviceThresholds thresholds = new DeviceThresholds();

        private void Awake()
        {
            StartCoroutine(DetectMode());
        }

        private IEnumerator DetectMode()
        {
            Debug.Log("[FoodLens] Starting Device Mode Detection...");

            // Check if AR Foundation subsystem is available/supported.
            if (ARSession.state == ARSessionState.None ||
                ARSession.state == ARSessionState.CheckingAvailability)
            {
                yield return ARSession.CheckAvailability();
            }

            if (ARSession.state == ARSessionState.Unsupported)
            {
                Debug.LogWarning("[FoodLens] ARCore is Unsupported on this device. Routing to FakeAR.");
                CurrentMode = ARMode.FakeAR;
            }
            else
            {
                // ARCore is supported. Evaluate CPU/GPU/RAM score.
                EvaluateDevice();
            }

            IsDetectionComplete = true;
            Debug.Log($"[FoodLens] Mode Detection Completed. Selected Mode: {CurrentMode}");
        }

        private void EvaluateDevice()
        {
            int ram = SystemInfo.systemMemorySize;
            int gpu = SystemInfo.graphicsMemorySize;
            int cpuCores = SystemInfo.processorCount;

            int score = 0;

            // RAM scoring
            if (ram >= thresholds.minRAM_Full) score += 2;
            else if (ram >= thresholds.minRAM_Lite) score += 1;

            // GPU memory scoring
            if (gpu >= thresholds.minGPU_Full) score += 2;
            else if (gpu >= thresholds.minGPU_Lite) score += 1;

            // CPU cores scoring
            if (cpuCores >= 8) score += 2;
            else if (cpuCores >= 4) score += 1;

            Debug.Log($"[FoodLens] Device capability report - RAM: {ram}MB, GPU: {gpu}MB, CPU Cores: {cpuCores}. Capability Score: {score}");

            DecideMode(score);
        }

        private void DecideMode(int score)
        {
            if (score >= 5)
            {
                CurrentMode = ARMode.FullAR;
            }
            else if (score >= 3)
            {
                CurrentMode = ARMode.LiteAR;
            }
            else
            {
                CurrentMode = ARMode.FakeAR;
            }
        }
    }
}
