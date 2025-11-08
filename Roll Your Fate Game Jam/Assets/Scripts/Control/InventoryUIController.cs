using UnityEngine;


namespace Control { 
    public class InventoryUIController : MonoBehaviour
    {
        public Animator anim1;
        public Animator anim2;

        public bool isShowing = false;

        public void OnEnable()
        {
            PlayerInputHandler.Instance.ToggleHUD += ToggleHUDAnimation;
        }

        public void OnDisable()
        {
            PlayerInputHandler.Instance.ToggleHUD -= ToggleHUDAnimation;
        }

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

        public void ToggleHUDAnimation() 
        {
            if (isShowing)
            {
                PlayHideUIAnimation();
            }
            else 
            {
                PlayShowUIAnimation();
            }

            isShowing = !isShowing;
        }
    }
}