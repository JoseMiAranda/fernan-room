using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject letsStartMenu;
    public GameObject resourcesMenu;
    public GameObject mainMenu;
    public GameObject quitButton;

    void Start()
    {
        if (quitButton != null && Application.platform == RuntimePlatform.WebGLPlayer)
            quitButton.SetActive(false);
    }

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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
}
