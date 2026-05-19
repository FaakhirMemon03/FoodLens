using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FoodLens.UI
{
    /// <summary>
    /// Implements accessibility scaling adjustments for interactive buttons and fonts,
    /// magnifying click targets by a scale multiplier when Family Mode is enabled.
    /// </summary>
    public class FamilyModeManager : MonoBehaviour
    {
        /// <summary>
        /// Combines layout references to resize rect and font simultaneously.
        /// </summary>
        [System.Serializable]
        public class ButtonConfig
        {
            public RectTransform buttonRect;
            public TMP_Text buttonText;
            
            [HideInInspector] public Vector2 defaultSize;
            [HideInInspector] public float defaultFontSize;
        }

        [Tooltip("The interactive buttons in the canvas to scale.")]
        public List<ButtonConfig> interactiveButtons = new List<ButtonConfig>();

        [Tooltip("Sizing multiplier for buttons and fonts in Family Mode.")]
        public float scaleMultiplier = 1.35f;

        private bool isFamilyModeActive = false;

        private void Start()
        {
            // Record original design sizes to enable reverting later.
            foreach (var btn in interactiveButtons)
            {
                if (btn.buttonRect != null)
                {
                    btn.defaultSize = btn.buttonRect.sizeDelta;
                }
                if (btn.buttonText != null)
                {
                    btn.defaultFontSize = btn.buttonText.fontSize;
                }
            }
        }

        /// <summary>
        /// Toggles button and font scaling according to Family Mode state.
        /// </summary>
        public void SetFamilyMode(bool active)
        {
            isFamilyModeActive = active;
            Debug.Log($"[FoodLens FamilyMode] Accessibility scaling updated. FamilyMode active: {isFamilyModeActive}");

            foreach (var btn in interactiveButtons)
            {
                if (btn.buttonRect != null)
                {
                    btn.buttonRect.sizeDelta = isFamilyModeActive 
                        ? btn.defaultSize * scaleMultiplier 
                        : btn.defaultSize;
                }

                if (btn.buttonText != null)
                {
                    btn.buttonText.fontSize = isFamilyModeActive 
                        ? btn.defaultFontSize * scaleMultiplier 
                        : btn.defaultFontSize;
                }
            }
        }
    }
}
