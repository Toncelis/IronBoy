using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComplexMenu : MonoBehaviour
{
    public void StartLevel()
    {
        SceneManager.LoadScene("SampleLevel", LoadSceneMode.Single);
    }

    [SerializeField] GameObject exitPanel;
    public void ChangeExitPanel()
    {
        exitPanel.SetActive(!exitPanel.activeSelf);
    }
    public void DisableExitPanel()
    {
        exitPanel.SetActive(false);
    }

    public void Exit ()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }
}
