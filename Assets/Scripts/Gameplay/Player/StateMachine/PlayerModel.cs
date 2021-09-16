using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(fileName = "Player Model", menuName = "SO/Player/Model", order = 1)]
    public class PlayerModel : ScriptableObject
    {
        #region fields
        [Header("Temperature")]
        
        [SerializeField]
        private float temperatureLevel1;
        [SerializeField]
        private Color temperature1Color;
        
        [SerializeField]
        private float temperatureLevel2;
        [SerializeField]
        private Color temperature2Color;

        [SerializeField]
        private float temperatureLevel3;
        [SerializeField]
        private Color temperature3Color;

        [SerializeField]
        private float temperatureCap;
        [SerializeField]
        private Color temperatureCapColor;
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
        #endregion

        #region access methods
        public float TemperatureLevel1      => temperatureLevel1;
        public float TemperatureLevel2      => temperatureLevel2;
        public float TemperatureLevel3      => temperatureLevel3;
        public float TemperatureCap         => temperatureCap;
        public float CoolingTime            => coolingTime;

        public Color Temperature1Color      => temperature1Color;
        public Color Temperature2Color      => temperature2Color;
        public Color Temperature3Color      => temperature3Color;
        public Color TemperatureCapColor    => temperatureCapColor;

        public float Energy                 => energy;
        public float EnergyWasteSpeed       => energyWasteSpeed;

        public float IdleCooling            => idleCooling;
        public float WalkSpeed              => walkSpeed;
        public float WalkCooling            => walkCooling;

        public float RunSpeed               => runSpeed;
        public float RunCooling             => runCooling;

        public float JumpStrength           => jumpStrength;

        public float BlinkDistance          => blinkDistance;
        public float BlinkHeat              => blinkHeat;
        public float BlinkCooldown          => blinkCooldown;

        public float StrongAttackLength     => strongAttackLength;
        public float StrongAttackHeat       => strongAttackHeat;
        public float StrongAttackDamage     => strongAttackDamage;
        #endregion

        #region WIP_Deep
        [SerializeField] private float overheatDamage;
        public float OverheatDamage => overheatDamage;
        #endregion

        [Serializable]
        public struct SaveablePlayerData
        {
            public float temperatureLevel1;
            public float temperatureLevel2;
            public float temperatureLevel3;
            public float temperatureCap;
            public float coolingTime;

            public float maxEnergy;
            public float energyWasteSpeed;
            public float idleCooling;
            public float walkSpeed;
            public float walkCooling;
            public float runSpeed;
            public float runCooling;
            public float jumpStrength;
            public float blinkDistance;
            public float blinkHeat;
            public float blinkCooldown;
            public float strongAttackHeat;
            public float strongAttackDamage;

            public SaveablePlayerData(
                float temperature1, float temperature2, float temperature3, float temperatureMax, float coolingDuration,
                float energyMax, float energyConsumption,
                float idleCool, float walkCool, float runCool,
                float walkS, float runS,
                float jumpForce,
                float blinkDist, float blinkCost, float blinkCd,
                float strongAttackCost, float strongAttackDam
                )
            {
                temperatureLevel1 = temperature1;
                temperatureLevel2 = temperature2;
                temperatureLevel3 = temperature3;
                temperatureCap = temperatureMax;
                coolingTime = coolingDuration;

                maxEnergy = energyMax;
                energyWasteSpeed = energyConsumption;

                idleCooling = idleCool;
                walkCooling = walkCool;
                runCooling = runCool;
                walkSpeed = walkS;
                runSpeed = runS;

                jumpStrength = jumpForce;
                blinkDistance = blinkDist;
                blinkHeat = blinkCost;
                blinkCooldown = blinkCd;
                strongAttackHeat = strongAttackCost;
                strongAttackDamage = strongAttackDam;
                
            }
        }

        public void SaveDataToFile ()
        {
            SaveablePlayerData save = new SaveablePlayerData
                (
                TemperatureLevel1, TemperatureLevel2, TemperatureLevel3, TemperatureCap, CoolingTime,
                Energy, EnergyWasteSpeed,
                IdleCooling, WalkCooling, RunCooling,
                WalkSpeed, RunSpeed,
                JumpStrength, BlinkDistance, BlinkHeat, BlinkCooldown,
                StrongAttackHeat, StrongAttackDamage
                );
            

            string destination = Application.persistentDataPath + "/PlayerGeneralInfo.json";
            File.WriteAllText(destination, JsonUtility.ToJson(save));
        }

        public void LoadDataFromFile ()
        {
            string destination = Application.persistentDataPath + "/PlayerGeneralInfo.json";
            if (File.Exists(destination))
            {
                SaveablePlayerData save = JsonUtility.FromJson<SaveablePlayerData>(File.ReadAllText(destination));
                temperatureLevel1 = save.temperatureLevel1;
                temperatureLevel2 = save.temperatureLevel2;
                temperatureLevel3 = save.temperatureLevel3;
                temperatureCap = save.temperatureCap;
                coolingTime = save.coolingTime;

                energy = save.maxEnergy;
                energyWasteSpeed = save.energyWasteSpeed;
                idleCooling = save.idleCooling;
                walkCooling = save.walkCooling;
                runCooling = save.runCooling;
                walkSpeed = save.walkSpeed;
                runSpeed = save.runSpeed;
                jumpStrength = save.jumpStrength;
                blinkDistance = save.blinkDistance;
                blinkHeat = save.blinkHeat;
                blinkCooldown = save.blinkCooldown;
                strongAttackHeat = save.strongAttackHeat;
                strongAttackDamage = save.strongAttackDamage;
            }
        }

        public void UpdateModel (
                float temperature1, float temperature2, float temperature3, float temperatureMax, float coolingDuration,
                float energyMax, float energyConsumption,
                float idleCool, float walkCool, float runCool,
                float walkS, float runS,
                float jumpForce,
                float blinkDist, float blinkCost, float blinkCd,
                float strongAttackCost, float strongAttackDam
            )
        {
            temperatureLevel1 = temperature1;
            temperatureLevel2 = temperature2;
            temperatureLevel3 = temperature3;
            temperatureCap = temperatureMax;
            coolingTime = coolingDuration;

            energy = energyMax;
            energyWasteSpeed = energyConsumption;

            idleCooling = idleCool;
            walkCooling = walkCool;
            runCooling = runCool;
            walkSpeed = walkS;
            runSpeed = runS;

            jumpStrength = jumpForce;
            blinkDistance = blinkDist;
            blinkHeat = blinkCost;
            blinkCooldown = blinkCd;
            strongAttackHeat = strongAttackCost;
            strongAttackDamage = strongAttackDam;
        }
    }
}
