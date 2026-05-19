using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FoodLens.UI
{
    /// <summary>
    /// Holds information about specific dishes displayed in the menu.
    /// </summary>
    [Serializable]
    public class MenuItemData
    {
        public string itemName;
        public string itemDescription;
        public string price;
        public GameObject arPrefab;
    }

    /// <summary>
    /// Manages the horizontal menu selection carousel.
    /// Smoothly snaps to items on swipe releases and alerts placement systems of changes.
    /// </summary>
    public class MenuCarousel : MonoBehaviour
    {
        [Tooltip("The ScrollRect containing the carousel cards.")]
        public ScrollRect scrollRect;

        [Tooltip("The Content viewport container.")]
        public RectTransform contentPanel;

        [Tooltip("List of menu dishes.")]
        public List<MenuItemData> menuItems = new List<MenuItemData>();

        [Tooltip("Snapping adjustment speed.")]
        public float snapSpeed = 10f;

        private int currentItemIndex = 0;
        private bool isSnapping = false;
        private float[] itemPositions;

        /// <summary>
        /// Triggered whenever the focused menu item changes.
        /// </summary>
        public event Action<MenuItemData> OnItemChanged;

        private void Start()
        {
            int count = menuItems.Count;
            if (count == 0) return;

            itemPositions = new float[count];
            
            // Re-render layout values before computation.
            Canvas.ForceUpdateCanvases();

            // Establish relative normalized positions [0..1] for snapping.
            float denominator = count - 1 > 0 ? count - 1 : 1f;
            for (int i = 0; i < count; i++)
            {
                itemPositions[i] = i / denominator;
            }

            // Emit initial selection.
            OnItemChanged?.Invoke(menuItems[currentItemIndex]);
        }

        private void Update()
        {
            if (menuItems.Count == 0) return;

            // Pause auto-snapping if user is dragging or input touches are active.
            if (scrollRect.velocity.magnitude > 0.1f || Input.touchCount > 0)
            {
                isSnapping = false;
                return;
            }

            if (!isSnapping)
            {
                // Identify the closest normalized scroll destination.
                float currentPos = Mathf.Clamp01(scrollRect.horizontalNormalizedPosition);
                float minDistance = float.MaxValue;
                int closestIndex = 0;

                for (int i = 0; i < itemPositions.Length; i++)
                {
                    float dist = Mathf.Abs(currentPos - itemPositions[i]);
                    if (dist < minDistance)
                    {
                        minDistance = dist;
                        closestIndex = i;
                    }
                }

                if (closestIndex != currentItemIndex)
                {
                    currentItemIndex = closestIndex;
                    OnItemChanged?.Invoke(menuItems[currentItemIndex]);
                }

                isSnapping = true;
            }

            if (isSnapping)
            {
                // Smoothly snap ScrollRect view.
                scrollRect.horizontalNormalizedPosition = Mathf.Lerp(
                    scrollRect.horizontalNormalizedPosition,
                    itemPositions[currentItemIndex],
                    Time.deltaTime * snapSpeed
                );
            }
        }

        /// <summary>
        /// Manual scroll to the next item.
        /// </summary>
        public void SelectNext()
        {
            if (currentItemIndex < menuItems.Count - 1)
            {
                currentItemIndex++;
                isSnapping = true;
                OnItemChanged?.Invoke(menuItems[currentItemIndex]);
            }
        }

        /// <summary>
        /// Manual scroll to the previous item.
        /// </summary>
        public void SelectPrevious()
        {
            if (currentItemIndex > 0)
            {
                currentItemIndex--;
                isSnapping = true;
                OnItemChanged?.Invoke(menuItems[currentItemIndex]);
            }
        }
    }
}
