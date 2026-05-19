using UnityEngine;
using UnityEngine.UI;

namespace FoodLens.AR.FakeAR
{
    /// <summary>
    /// Renders the phone's live back-camera feed on a RawImage background
    /// with correct rotation and aspect ratio calculations.
    /// </summary>
    public class WebCamBackground : MonoBehaviour
    {
        private WebCamTexture webCamTexture;
        public RawImage backgroundDisplay;
        public AspectRatioFitter aspectFitter;

        private void Start()
        {
            // Scan for the rear-facing camera.
            WebCamDevice[] devices = WebCamTexture.devices;
            string rearCameraName = "";

            for (int i = 0; i < devices.Length; i++)
            {
                if (!devices[i].isFrontFacing)
                {
                    rearCameraName = devices[i].name;
                    break;
                }
            }

            // Fallback if no specific rear-facing camera is identified.
            if (string.IsNullOrEmpty(rearCameraName) && devices.Length > 0)
            {
                rearCameraName = devices[0].name;
            }

            if (!string.IsNullOrEmpty(rearCameraName))
            {
                webCamTexture = new WebCamTexture(rearCameraName, Screen.width, Screen.height);
                
                if (backgroundDisplay != null)
                {
                    backgroundDisplay.texture = webCamTexture;
                }

                webCamTexture.Play();
                Debug.Log($"[FoodLens] WebCamTexture playing: {rearCameraName}");
            }
            else
            {
                Debug.LogError("[FoodLens] WebCamBackground failed: No camera device detected.");
            }
        }

        private void Update()
        {
            if (webCamTexture == null || !webCamTexture.isPlaying)
                return;

            // Correct orientation issues dynamically.
            int videoRotationAngle = webCamTexture.videoRotationAngle;
            if (backgroundDisplay != null)
            {
                backgroundDisplay.rectTransform.localEulerAngles = new Vector3(0, 0, -videoRotationAngle);
            }

            // Adjust aspect ratio to prevent camera feed stretching.
            float videoRatio = (float)webCamTexture.width / (float)webCamTexture.height;
            if (aspectFitter != null)
            {
                aspectFitter.aspectRatio = videoRatio;
            }
        }

        private void OnDestroy()
        {
            if (webCamTexture != null && webCamTexture.isPlaying)
            {
                webCamTexture.Stop();
            }
        }
    }
}
