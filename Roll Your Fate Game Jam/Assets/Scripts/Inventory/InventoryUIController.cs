using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public Animator anim1;
    public Animator anim2;

    [ContextMenu("Show UI")]
    public void PlayShowUIAnimation() 
    {
        anim1.SetTrigger("ShowUI");
        anim2.SetTrigger("ShowUI");
    }

    [ContextMenu("Hide UI")]
    public void PlayHideUIAnimation()
    {
        anim1.SetTrigger("HideUI");
        anim2.SetTrigger("HideUI");
    }
}
