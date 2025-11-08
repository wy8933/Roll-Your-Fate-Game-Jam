using UnityEngine;
using UnityEngine.UI;

namespace UI { 
    public class ItemBlockUI : MonoBehaviour
    {
        public Image icon;

        public void SetIcon(Sprite icon) 
        {
            this.icon.sprite = icon;
        }
    }
}