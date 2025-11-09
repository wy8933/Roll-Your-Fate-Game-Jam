using UnityEngine;
using UnityEngine.UI;

namespace UI { 
    public class ItemBlockUI : MonoBehaviour
    {
        public Image icon;
        public Image background;
        public CanvasGroup iconCanvasGroup;
        [SerializeField] private float _selectedScale = 1.08f;

        public void SetIcon(Sprite icon) 
        {
            this.icon.sprite = icon;

            iconCanvasGroup.alpha = 1;
        }

        public void SetIconAlphaZero() 
        {
            iconCanvasGroup.alpha = 0;
        }

        public void SetSelected(bool selected)
        {
            if (icon != null)
                icon.rectTransform.localScale = selected ? Vector3.one * _selectedScale : Vector3.one;
            if (background != null)
                background.rectTransform.localScale = selected ? Vector3.one * _selectedScale : Vector3.one;
        }
    }
}