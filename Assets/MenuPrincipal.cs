using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject StartMenuPanel;
    [SerializeField] private GameObject ControllersPanel;
    [SerializeField] private GameObject AboutPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(levelName);
    }

    public void OpenControllers()
    {
        StartMenuPanel.SetActive(false);
        ControllersPanel.SetActive(true);  
    }

    public void CloseControllers()
    {
        ControllersPanel?.SetActive(false);
        StartMenuPanel?.SetActive(true);
    }

    public void OpenAbout()
    {
        StartMenuPanel.SetActive(false);
        AboutPanel.SetActive(true);
    }

    public void CloseAbout()
    {
        AboutPanel.SetActive(false);
        StartMenuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        System.Diagnostics.Debug.Write("Saindo do Jogo ...");
        Application.Quit();
    }
}
