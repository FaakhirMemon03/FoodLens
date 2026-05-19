using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FoodLens.UI
{
    /// <summary>
    /// Holds the translation dictionaries and routes language-switching event updates.
    /// </summary>
    public class LanguageLocalizer : MonoBehaviour
    {
        public static bool IsUrdu { get; private set; } = false;
        public static event Action OnLanguageChanged;

        private static readonly Dictionary<string, string> englishToUrdu = new Dictionary<string, string>()
        {
            { "Add to Cart", "کارٹ میں شامل کریں" },
            { "Order Now", "ابھی آرڈر کریں" },
            { "Scan Table", "میز اسکین کریں" },
            { "Move phone around to detect surface", "سطح تلاش کرنے کے لیے فون کو گھمائیں" },
            { "Double-tap to remove", "ختم کرنے کے لیے دو بار دبائیں" },
            { "Burger", "برگر" },
            { "Pizza", "پیزا" },
            { "Biryani", "بریانی" },
            { "Family Mode", "فیملی موڈ" },
            { "Settings", "ترتیبات" },
            { "Price", "قیمت" }
        };

        /// <summary>
        /// Translates a given English phrase into Urdu if Urdu is activated.
        /// </summary>
        public static string GetTranslation(string key)
        {
            if (!IsUrdu) return key;
            return englishToUrdu.TryGetValue(key, out string translation) ? translation : key;
        }

        /// <summary>
        /// Updates the system language selection and broadcasts the change.
        /// </summary>
        public void SetUrduLanguage(bool active)
        {
            IsUrdu = active;
            Debug.Log($"[FoodLens Localizer] System language updated. Urdu active: {IsUrdu}");
            OnLanguageChanged?.Invoke();
        }
    }

    /// <summary>
    /// Add to UI elements to translate their TMP_Text values on language changes.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizableText : MonoBehaviour
    {
        private TMP_Text textComponent;
        private string originalText;

        private void Awake()
        {
            textComponent = GetComponent<TMP_Text>();
            originalText = textComponent.text;
        }

        private void OnEnable()
        {
            LanguageLocalizer.OnLanguageChanged += UpdateText;
            UpdateText();
        }

        private void OnDisable()
        {
            LanguageLocalizer.OnLanguageChanged -= UpdateText;
        }

        private void UpdateText()
        {
            if (textComponent != null)
            {
                textComponent.text = LanguageLocalizer.GetTranslation(originalText);
                
                // Adjust text layout alignment for Urdu (Right-to-Left feel)
                if (LanguageLocalizer.IsUrdu)
                {
                    textComponent.alignment = TextAlignmentOptions.Right;
                }
                else
                {
                    textComponent.alignment = TextAlignmentOptions.Left;
                }
            }
        }
    }
}
