using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace FoodLens.AR.FullAR
{
    /// <summary>
    /// Captures lighting estimation data from ARCore camera frames
    /// and applies intensity, color temperature, and spherical harmonics
    /// to matching directional lights and ambient render settings.
    /// </summary>
    [RequireComponent(typeof(Light))]
    public class LightingEstimation : MonoBehaviour
    {
        [Tooltip("The AR Camera Manager providing real-world frame analysis.")]
        public ARCameraManager cameraManager;
        
        private Light directionalLight;

        private void Awake()
        {
            directionalLight = GetComponent<Light>();
            directionalLight.useColorTemperature = true;
        }

        private void OnEnable()
        {
            if (cameraManager != null)
            {
                cameraManager.frameReceived += OnFrameReceived;
            }
        }

        private void OnDisable()
        {
            if (cameraManager != null)
            {
                cameraManager.frameReceived -= OnFrameReceived;
            }
        }

        private void OnFrameReceived(ARCameraFrameEventArgs args)
        {
            ARLightEstimation estimation = args.lightEstimation;

            // 1. Average Brightness
            if (estimation.averageBrightness.HasValue)
            {
                directionalLight.intensity = estimation.averageBrightness.Value;
            }

            // 2. Average Color Temperature
            if (estimation.averageColorTemperature.HasValue)
            {
                directionalLight.colorTemperature = estimation.averageColorTemperature.Value;
            }

            // 3. Ambient Spherical Harmonics for ambient room color
            if (estimation.ambientSphericalHarmonics.HasValue)
            {
                RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
                RenderSettings.ambientProbe = estimation.ambientSphericalHarmonics.Value;
            }

            // 4. Main Light Direction
            if (estimation.mainLightDirection.HasValue)
            {
                directionalLight.transform.rotation = Quaternion.LookRotation(estimation.mainLightDirection.Value);
            }

            // 5. Main Light Intensity & Color (if supported by device hardware)
            if (estimation.mainLightIntensityLumens.HasValue)
            {
                directionalLight.intensity = estimation.mainLightIntensityLumens.Value / 1000f;
            }
            
            if (estimation.mainLightColor.HasValue)
            {
                directionalLight.color = estimation.mainLightColor.Value;
            }
        }
    }
}
