using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private string currentMessage = "Text Message";
    public static GameManager Instance;
    private static TextMeshProUGUI roundText;
    private static TextMeshProUGUI guidanceText;

    private const string SHOW_BOARD = "Pulse E sobre la pizarra";

    private void Awake()
    {
        roundText = GameObject.FindWithTag("Round").GetComponent<TextMeshProUGUI>();
        guidanceText = GameObject.FindWithTag("Guidance").GetComponent<TextMeshProUGUI>();

        if (Instance == null)
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
        roundText.text = "Ronda 1";
        guidanceText.text = SHOW_BOARD;

        currentMessage = "Find a cube!!!!";
    }

    public void RoundTwo()
    {
        roundText.text = "Ronda 2";
        guidanceText.text = SHOW_BOARD;

        currentMessage = "Take the weapon!!!";
    }

    public string getCurrentMessage()
    {
        return currentMessage;
    }
}
