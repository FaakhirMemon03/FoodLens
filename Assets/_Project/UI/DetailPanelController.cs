using UnityEngine;
using TMPro;

namespace FoodLens.UI
{
    /// <summary>
    /// Controls the sliding animation (up/down) of the UI Details Card
    /// and populates it with selected menu data.
    /// </summary>
    public class DetailPanelController : MonoBehaviour
    {
        [Tooltip("The RectTransform of the sliding panel.")]
        public RectTransform panelRect;

        [Tooltip("Text labels inside the details card.")]
        public TMP_Text titleText;
        public TMP_Text descriptionText;
        public TMP_Text priceText;

        [Tooltip("Anchored Y coordinate values when hidden vs visible.")]
        public Vector2 hiddenAnchorPosition = new Vector2(0f, -600f);
        public Vector2 visibleAnchorPosition = new Vector2(0f, 50f);

        [Tooltip("Animation speed multiplier.")]
        public float slideSpeed = 8f;

        private Vector2 targetAnchorPosition;

        private void Start()
        {
            if (panelRect != null)
            {
                panelRect.anchoredPosition = hiddenAnchorPosition;
                targetAnchorPosition = hiddenAnchorPosition;
            }
        }

        private void Update()
        {
            if (panelRect != null)
            {
                panelRect.anchoredPosition = Vector2.Lerp(
                    panelRect.anchoredPosition,
                    targetAnchorPosition,
                    Time.deltaTime * slideSpeed
                );
            }
        }

        /// <summary>
        /// Populates details card fields and slides it into view.
        /// </summary>
        public void ShowPanel(string title, string description, string price)
        {
            if (titleText != null) titleText.text = title;
            if (descriptionText != null) descriptionText.text = description;
            if (priceText != null) priceText.text = price;

            targetAnchorPosition = visibleAnchorPosition;
        }

        /// <summary>
        /// Slides the card out of view.
        /// </summary>
        public void HidePanel()
        {
            targetAnchorPosition = hiddenAnchorPosition;
        }

        /// <summary>
        /// Triggered by click events on the main action button.
        /// </summary>
        public void AddToCartClicked()
        {
            string item = titleText != null ? titleText.text : "Unknown Item";
            Debug.Log($"[FoodLens UI] Order / Add to Cart triggered: {item}");
        }
    }
}
