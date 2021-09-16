using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerV2
{
    [CreateAssetMenu(fileName = "PlayerTemplate", menuName = "SO/Player/PlayerTemplate", order = 1)]
    public class PlayerTemplate : ScriptableObject
    {
        #region fields
        [Header("Temperature")]

        [SerializeField]
        private float temperatureLevel1;
        [SerializeField]
        private float temperatureLevel2;
        [SerializeField]
        private float temperatureLevel3;
        [SerializeField]
        private float temperatureCap;

        [SerializeField]
        private float coolingTime;

        [Header("Energy")]
        [SerializeField]
        private float energy;
        [SerializeField]
        private float energyWasteSpeed;

        [Header("Idle")]
        [SerializeField]
        private float idleCooling;

        [Header("Walk")]
        [SerializeField]
        private float walkSpeed;

        [SerializeField]
        private float walkCooling;

        [Header("Run")]
        [SerializeField]
        private float runSpeed;
        [SerializeField]
        private float runCooling;

        [Header("Jump")]
        [SerializeField]
        private float jumpStrength;

        [Header("Blink")]
        [SerializeField]
        private float blinkDistance;
        [SerializeField]
        private float blinkHeat;
        [SerializeField]
        private float blinkCooldown;

        [Header("Strong Attack")]
        [SerializeField]
        private float strongAttackLength;
        [SerializeField]
        private float strongAttackHeat;
        [SerializeField]
        private float strongAttackDamage;
        [SerializeField]
        private float strongAttackSpeedMultiplier;
        #endregion

        #region access methods
        public float TemperatureLevel1 => temperatureLevel1;
        public float TemperatureLevel2 => temperatureLevel2;
        public float TemperatureLevel3 => temperatureLevel3;
        public float TemperatureCap => temperatureCap;
        public float CoolingTime => coolingTime;

        public float Energy => energy;
        public float EnergyWasteSpeed => energyWasteSpeed;

        public float IdleCooling => idleCooling;
        public float WalkSpeed => walkSpeed;
        public float WalkCooling => walkCooling;

        public float RunSpeed => runSpeed;
        public float RunCooling => runCooling;

        public float JumpStrength => jumpStrength;

        public float BlinkDistance => blinkDistance;
        public float BlinkHeat => blinkHeat;
        public float BlinkCooldown => blinkCooldown;

        public float StrongAttackLength => strongAttackLength;
        public float StrongAttackHeat => strongAttackHeat;
        public float StrongAttackDamage => strongAttackDamage;
        #endregion

        #region WIP_Deep
        [SerializeField] private float overheatDamage;
        public float OverheatDamage => overheatDamage;
        #endregion

        public void SaveDataToFile()
        {
            string destination = Application.persistentDataPath + "/PlayerTemplatelInfo.json";
            System.IO.File.WriteAllText(destination, JsonUtility.ToJson(this));
        }

        public void LoadDataFromFile()
        {
            string destination = Application.persistentDataPath + "/PlayerTemplatelInfo.json";
            JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(destination), this);
        }
    }
}
