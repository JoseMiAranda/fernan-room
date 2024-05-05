using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    }

    public void RoundTwo()
    {
        Debug.Log("ROUND TWO!!!");
    }
}
