using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerV2
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] PlayerTemplate template;

        private void Awake()
        {
            //template.LoadDataFromFile();    
        }

        #region Temperature bar
        public float TemperatureLevel1 => template.TemperatureLevel1;
        public float TemperatureLevel2 => template.TemperatureLevel2;
        public float TemperatureLevel3 => template.TemperatureLevel3;
        public float TemperatureCap => template.TemperatureCap;
        public float CoolingTime => template.CoolingTime;

        #region Temperature bar colors
        [Header("Temperature bar")]
        [SerializeField] private Color temperature1Color;
        [SerializeField] private Color temperature2Color;
        [SerializeField] private Color temperature3Color;
        [SerializeField] private Color temperatureCapColor;

        public Color Temperature1Color => temperature1Color;
        public Color Temperature2Color => temperature2Color;
        public Color Temperature3Color => temperature3Color;
        public Color TemperatureCapColor => temperatureCapColor;
        #endregion
        #endregion

        #region Movement

        public float IdleCooling => template.IdleCooling;
        public float WalkSpeed => template.WalkSpeed;

        public float RunSpeed => template.RunSpeed;

        public float JumpStrength => template.JumpStrength;
        #endregion

        #region Energy
        public float Energy => template.Energy;
        public float EnergyWasteSpeed => template.EnergyWasteSpeed;

        public float WalkCooling => template.WalkCooling;
        public float RunCooling => template.RunCooling;

        public float OverheatDamage => template.OverheatDamage;
        #endregion

        #region Special Actions
        public float BlinkDistance => template.BlinkDistance;
        public float BlinkHeat => template.BlinkHeat;
        public float BlinkCooldown => template.BlinkCooldown;

        public float StrongAttackLength => template.StrongAttackLength;
        public float StrongAttackHeat => template.StrongAttackHeat;
        public float StrongAttackDamage => template.StrongAttackDamage;
        #endregion
    }
}
