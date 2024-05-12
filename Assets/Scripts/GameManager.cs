using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private string currentMessage = "Text Message";
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            RoundOne();
        } else
        {
            Debug.LogWarning("There are more Game Managers!");
        }
    }

    public void RoundOne()
    {
        Debug.Log("ROUND ONE!!!");
        currentMessage = "Find a cube!!!!";
    }

    public void RoundTwo()
    {
        Debug.Log("ROUND TWO!!!");
        currentMessage = "Take the weapon!!!";
    }

    public string getCurrentMessage()
    {
        return currentMessage;
    }
}
