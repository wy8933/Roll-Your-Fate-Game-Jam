using UnityEngine;
using UnityEngine.UI;
using Control;
using Game;
using TMPro;

namespace UI { 
    public class HUDManager : MonoBehaviour
    {
        public static HUDManager Instance;

        [Header("References")]
        public Animator anim1;
        public Animator anim2;
        public Slider slider;
        public TMP_Text informationText;

        public bool isShowing = false;
        public float timer;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void OnEnable()
        {
            PlayerInputHandler.Instance.ToggleHUD += ToggleHUDAnimation;
            Player.Instance.OxygenChanged.AddListener(SetSliderValue);
        }

        public void OnDisable()
        {
            PlayerInputHandler.Instance.ToggleHUD -= ToggleHUDAnimation;
            Player.Instance.OxygenChanged.RemoveListener(SetSliderValue);
        }

        [ContextMenu("Show UI")]
        public void PlayShowUIAnimation() 
        {
            anim1.SetTrigger("ShowUI");
            anim2.SetTrigger("ShowUI");
            timer = 0;
        }

        [ContextMenu("Hide UI")]
        public void PlayHideUIAnimation()
        {
            anim1.SetTrigger("HideUI");
            anim2.SetTrigger("HideUI");
            timer = 0;
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

        public void SetSliderValue(float value)
        {
            if (slider == null)
                return;

            if (value > 1)
                value = 1;

            slider.value = value;
        }

        public void SetText(string text) 
        {
            if (informationText == null && text == null)
                return;

            informationText.text = text;
        }
    }
}