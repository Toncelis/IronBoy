using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp_AllUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject MenuPanel;

    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject TestChamber;

    [Space(4)]
    [Header("SO")]
    [SerializeField] Game.PlayerV2.PlayerModel playerModel;
    [SerializeField] Game.Enemy.EnemyModel enemyModel;

    [Space(4)]
    [Header("Settings")]
    [SerializeField] InputField tempLvl1;
    [SerializeField] InputField tempLvl2;
    [SerializeField] InputField tempLvl3;
    [SerializeField] InputField tempCap;
    [SerializeField] InputField coolingTime;
    [SerializeField] InputField energy;
    [SerializeField] InputField energyWaste;
    [SerializeField] InputField idleCooling;
    [SerializeField] InputField walkCooling;
    [SerializeField] InputField runCooling;
    [SerializeField] InputField walkSpeed;
    [SerializeField] InputField runSpeed;
    [SerializeField] InputField jumpForce;
    [SerializeField] InputField blinkCost;
    [SerializeField] InputField strongAttackCost;
    [SerializeField] InputField strongAttackDamage;
    [SerializeField] InputField blinkDist;
    [SerializeField] InputField blinkCooldown;

    [SerializeField] InputField enemyHp;
    [SerializeField] InputField enemyDamage;
    [SerializeField] InputField enemyWalkS;
    [SerializeField] InputField enemyRunS;

    private void OnEnable()
    {
        //playerModel.LoadDataFromFile();
        enemyModel.LoadDataFromFile();
    }

    public void StartButtonPressed ()
    {
        //playerModel.SaveDataToFile();
        enemyModel.SaveDataToFile();
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleLevel", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
    public void ExitButtonPressed ()
    {
        //playerModel.SaveDataToFile();
        enemyModel.SaveDataToFile();
        Application.Quit();
    }

    public void MenuButtonPressed ()
    {
        MenuPanel.SetActive(!MenuPanel.activeSelf);

        SettingsPanel.SetActive(false);
        TestChamber.SetActive(false);
    }

    public void SettingsButtonPressed ()
    {
        MenuPanel.SetActive(false);

        if (SettingsPanel.activeSelf)
        {
            SettingsPanel.SetActive(false);
            TestChamber.SetActive(false);
            return;
        }

        SettingsPanel.SetActive(true);
        TestChamber.SetActive(true);
        SetupSettings();
    }

    private void SetupSettings ()
    {
        tempLvl1.text = $"{playerModel.TemperatureLevel1}";
        tempLvl2.text = $"{playerModel.TemperatureLevel2}";
        tempLvl3.text = $"{playerModel.TemperatureLevel3}";
        tempCap.text = $"{playerModel.TemperatureCap}";
        coolingTime.text = $"{playerModel.CoolingTime}";
        energy.text = $"{playerModel.Energy}";
        energyWaste.text = $"{playerModel.EnergyWasteSpeed}";
        idleCooling.text = $"{playerModel.IdleCooling}";
        walkCooling.text = $"{playerModel.WalkCooling}";
        runCooling.text = $"{playerModel.RunCooling}";
        walkSpeed.text = $"{playerModel.WalkSpeed}";
        runSpeed.text = $"{playerModel.RunSpeed}";
        jumpForce.text = $"{playerModel.JumpStrength}";
        blinkCost.text = $"{playerModel.BlinkHeat}";
        blinkCooldown.text = $"{playerModel.BlinkCooldown}";
        blinkDist.text = $"{playerModel.BlinkDistance}";
        strongAttackCost.text = $"{playerModel.StrongAttackHeat}";
        strongAttackDamage.text = $"{playerModel.StrongAttackDamage}";

        enemyHp.text = $"{enemyModel.Hp}";
        enemyDamage.text = $"{enemyModel.Damage}";
        enemyWalkS.text = $"{enemyModel.WalkSpeed}";
        enemyRunS.text = $"{enemyModel.RunSpeed}";
    }

    public void SaveSettings ()
    {
        /*
        playerModel.UpdateModel(
            float.Parse(tempLvl1.text), float.Parse(tempLvl2.text),
            float.Parse(tempLvl3.text), float.Parse(tempCap.text),
            float.Parse(coolingTime.text),
            float.Parse(energy.text), float.Parse(energyWaste.text),
            float.Parse(idleCooling.text), float.Parse(walkCooling.text),
            float.Parse(runCooling.text), float.Parse(walkSpeed.text),
            float.Parse(runSpeed.text), float.Parse(jumpForce.text),
            float.Parse(blinkDist.text), float.Parse(blinkCost.text),
            float.Parse(blinkCooldown.text), float.Parse(strongAttackCost.text),
            float.Parse(strongAttackDamage.text));
        */
        enemyModel.UpdateModel(
            float.Parse(enemyHp.text), float.Parse(enemyDamage.text),
            float.Parse(enemyWalkS.text), float.Parse(enemyRunS.text));
    }
}
