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
        private float MaxOxygen;
        public float OxygenPercentage => curOxygen / MaxOxygen;
        
        [HideInInspector]
        public UnityEvent<float> OxygenChanged = new UnityEvent<float>();

        public bool isConsumingOxygen = false;

        private void Start()
        {
            curOxygen = MaxOxygen = setting.GameSetting.initialOxygen;
        }

        private void OnDisable()
        {
            OxygenChanged.RemoveAllListeners();
        }

        public void SetOxygen(float oxygen)
        {
            curOxygen = MaxOxygen = oxygen;
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

            if (curOxygen <= 0)
            {
                GameManager.Instance.RunOutOxygen();
            }
        }
        
        
    }
}