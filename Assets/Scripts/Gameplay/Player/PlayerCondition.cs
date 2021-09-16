using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Player
{
    public class PlayerCondition : MonoBehaviour
    {
        [SerializeField]
        PlayerV2.PlayerModel model;

        float energy;
        float maxEnergy;
        [SerializeField]
        UnityEngine.UI.Image energyBar;

        float temperature;
        public float Temperature => temperature;
        float maxTemperature;
        [SerializeField]
        UnityEngine.UI.Image temperatureBar;

        [SerializeField]
        Animator animator;

        public delegate void SimpleEventHandler ();
        
        public event SimpleEventHandler overheatEvent;
        private void RaiseOverheatEvent ()
        {
            overheatEvent.Invoke();
        }
        
        public event SimpleEventHandler zeroEnergyEvent;
        private void RaiseZeroEnergyEvent()
        {
            zeroEnergyEvent.Invoke();
        }

        private void UpdateTemperatureBar (bool withColor)
        {
            temperatureBar.fillAmount = temperature / maxTemperature;

            if (withColor)
            {
                if (temperature < model.TemperatureLevel1)
                {
                    temperatureBar.color = model.Temperature1Color;
                }
                else if (temperature < model.TemperatureLevel2)
                {
                    temperatureBar.color = model.Temperature2Color;
                }
                else if (temperature < model.TemperatureLevel3)
                {
                    temperatureBar.color = model.Temperature3Color;
                }
                else
                {
                    temperatureBar.color = model.TemperatureCapColor;
                }
            }
        }

        private void UpdateEnergyBar ()
        {
            energyBar.fillAmount = energy / maxEnergy;
        }

        private void Start() 
        {
            maxEnergy = model.Energy;
            energy = maxEnergy;

            maxTemperature = model.TemperatureCap;
            temperature = 0;

            UpdateEnergyBar();
            UpdateTemperatureBar(true);
        }

        public void RaiseTemperature (float value, bool withColorEffect)
        {
            temperature = Mathf.Clamp(temperature + value, 0 , maxTemperature);
            UpdateTemperatureBar(withColorEffect);
            if (temperature == maxTemperature)
            {
                RaiseOverheatEvent();
            }
        }

        public void DropEnergy (float value)
        {
            energy = Mathf.Clamp(energy - value, 0, maxEnergy);
            UpdateEnergyBar();
            if (energy == 0)
            {
                RaiseZeroEnergyEvent();
            }
        }

        public void GetHit (float value)
        {
            DropEnergy(value);
            animator.SetTrigger("Hit");
        }

        
        private void FixedUpdate()
        {
            DropEnergy(Time.fixedDeltaTime * model.EnergyWasteSpeed);
        }
        
    }
}