using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject letsStartMenu;
    public GameObject resourcesMenu;
    public GameObject mainMenu;

    public void OpenLestsStartMenu() 
    {
        mainMenu.SetActive(false);
        letsStartMenu.SetActive(true);
    }

    public void OpenResourcesMenu() 
    {
        mainMenu.SetActive(false);
        resourcesMenu.SetActive(true);
    }

    public void OpenMainMenu()
    {
        resourcesMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
}
