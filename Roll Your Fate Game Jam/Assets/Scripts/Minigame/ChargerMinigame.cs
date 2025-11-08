using UnityEngine;
using UnityEngine.UI;
namespace Minigame 
{ 
    public class ChargerMinigame : MinigameController
    {
        public Slider playerSlider;
        public Slider targetSlider;
        public Slider endGameSlider;

        public float autoDecreaseScale;
        public float increaseScale;

        public float endGameValue = 100;
        public float currentValue = 0;
        public float engameValueIncrease = 1;

        public float targetValueChangeCooldown = 3;
        public float targetSliderNextValue;

        public float valueChangeOffset = 0.2f;

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if(playerSlider.value > 0)
                playerSlider.value = Mathf.Lerp(playerSlider.value, playerSlider.value - autoDecreaseScale * Time.deltaTime, 1);

            if (timer >= targetValueChangeCooldown) 
            {
                targetSliderNextValue = Random.value;
                timer = 0;
            }

            float lerpProcess = timer/2/targetValueChangeCooldown;
            targetSlider.value = Mathf.Lerp(targetSlider.value, targetSliderNextValue, lerpProcess/2);

            if (Mathf.Abs(playerSlider.value - targetSlider.value) < valueChangeOffset) 
            {
                currentValue += engameValueIncrease * Time.deltaTime;
                endGameSlider.value = currentValue/100;
            }

            if (currentValue >= endGameValue) 
            {
                GameClear();
            }
        }

        public override void Launch()
        {
            base.Launch();
        }

        public override void OnClick() 
        {
            playerSlider.value = Mathf.Lerp(playerSlider.value, playerSlider.value + increaseScale, 5f);
        }
    }
}