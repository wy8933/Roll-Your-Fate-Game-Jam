using UnityEngine;
using UnityEngine.UI;
namespace Minigame 
{ 
    public class ChargerMinigame : MinigameController
    {
        public Slider playerSlider;
        public Slider targetSlider;

        public float autoDecreaseScale;
        public float increaseScale;

        public float targetValue = 100;
        public float currentValue = 0;

        public float targetValueChangeCooldown = 3;
        public float targetSliderValue;

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if(playerSlider.value > 0)
                playerSlider.value = Mathf.Lerp(playerSlider.value, playerSlider.value - autoDecreaseScale * Time.deltaTime, 1);

            if (timer >= 3) 
            {
                
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