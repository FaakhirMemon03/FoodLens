using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using FoodLens.Core.ModeDetection;

namespace FoodLens.Core.Managers
{
    /// <summary>
    /// Entry point component. Waits for ModeDetectionSystem to finish scanning,
    /// then routes the execution thread to the designated visualizer scene.
    /// </summary>
    public class BootLoader : MonoBehaviour
    {
        private IEnumerator Start()
        {
            Debug.Log("[FoodLens BootLoader] Waiting for device capability check to finish...");

            // Wait until the detection system determines the active tier.
            while (!ModeDetectionSystem.IsDetectionComplete)
            {
                yield return null;
            }

            ARMode mode = ModeDetectionSystem.CurrentMode;
            Debug.Log($"[FoodLens BootLoader] Routing device to visualization scene: {mode}");

            switch (mode)
            {
                case ARMode.FullAR:
                    SceneManager.LoadScene("FullAR");
                    break;

                case ARMode.LiteAR:
                    SceneManager.LoadScene("LiteAR");
                    break;

                case ARMode.FakeAR:
                    SceneManager.LoadScene("FakeAR");
                    break;

                default:
                    Debug.LogError($"[FoodLens BootLoader] Unknown ARMode {mode}. Redirecting to FakeAR.");
                    SceneManager.LoadScene("FakeAR");
                    break;
            }
        }
    }
}
