using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public Animation anim1;
    public Animation anim2;

    [ContextMenu("Play Animation")]
    public void PlayAnimation() 
    {
        anim1.Play();
        anim2.Play();
    }
}
