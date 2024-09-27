using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private static RoundManager instance;
    private readonly List<IRoundObserver> observers = new();

    public static RoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RoundManager>();
            }
            return instance;
        }
    }

    public void RegisterObserver(IRoundObserver observer)
    {
        if (!observers.Contains(observer))
        {
            Debug.Log("Added observer");
            observers.Add(observer);
        }
    }

    public void UnregisterObserver(IRoundObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void StartRound(int round)
    {
        TextManager.Instance.ShowTextsRound(round);
        AudioManager.Instance.PlayMusicRound(round);

        Debug.Log("Started round: " + round);
        foreach (var observer in observers)
        {
            observer.OnRoundStarted(round);
        }
    }

    public void ClearRound(int round)
    {
        foreach (var observer in observers)
        {
            observer.OnRoundCleared(round);
        }
        if (round == 7)
        {
            GameManager.Instance.Exit();
        }
    }
}
