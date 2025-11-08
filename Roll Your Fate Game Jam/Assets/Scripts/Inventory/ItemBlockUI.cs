using UnityEngine;
using UnityEngine.UI;
public class ItemBlockUI : MonoBehaviour
{
    public Image icon;

    public void SetIcon(Sprite icon) 
    {
        this.icon.sprite = icon;
    }
}
