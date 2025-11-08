 using System;
using Template;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Player: SingletonBehavior<Player>
    {
        [SerializeField] SettingSO setting;
        
        private float curOxygen;
        private float maxOxygen;
        public float OxygenPercentage => curOxygen / maxOxygen;
        
        [HideInInspector]
        public UnityEvent<float> OxygenChanged = new UnityEvent<float>();
        [HideInInspector]
        public UnityEvent NoMoreOxygen = new UnityEvent();

        private void Start()
        {
            curOxygen = maxOxygen = setting.GameSetting.initialOxygen;
        }

        private void OnDisable()
        {
            OxygenChanged.RemoveAllListeners();
        }

        public void RechargeOxygen()
        {
            curOxygen = maxOxygen;
        }
            
        void Update()
        {
            
        }

        void FixedUpdate()
        {
            curOxygen -= setting.GameSetting.oxygenConsumingRate * Time.deltaTime;
            OxygenChanged?.Invoke(OxygenPercentage);
        }
    }
}