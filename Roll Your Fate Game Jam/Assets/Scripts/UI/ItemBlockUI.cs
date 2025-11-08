using UnityEngine;
using UnityEngine.UI;

namespace UI { 
    public class ItemBlockUI : MonoBehaviour
    {
        public Image icon;
        public CanvasGroup iconCanvasGroup;

        public void SetIcon(Sprite icon) 
        {
            this.icon.sprite = icon;

            iconCanvasGroup.alpha = 1;
        }

        public void SetIconAlphaZero() 
        {
            iconCanvasGroup.alpha = 0;
        }
    }
}