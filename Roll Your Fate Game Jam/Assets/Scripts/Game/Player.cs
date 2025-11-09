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
        public float MaxOxygen;
        public float OxygenPercentage => curOxygen / MaxOxygen;
        
        [HideInInspector]
        public UnityEvent<float> OxygenChanged = new UnityEvent<float>();
        [HideInInspector]
        public UnityEvent NoOxygen = new UnityEvent();

        public bool isConsumingOxygen = false;

        private void Start()
        {
            curOxygen = MaxOxygen = setting.GameSetting.initialOxygen;
        }

        private void OnDisable()
        {
            OxygenChanged.RemoveAllListeners();
        }

        public void RechargeOxygen()
        {
            curOxygen = MaxOxygen;
        }
        
        void Update()
        {
            
        }

        void FixedUpdate()
        {
            if (isConsumingOxygen)
            {
                curOxygen -= setting.GameSetting.oxygenConsumingRate * Time.deltaTime;
                OxygenChanged?.Invoke(OxygenPercentage);
            }
        }
    }
}