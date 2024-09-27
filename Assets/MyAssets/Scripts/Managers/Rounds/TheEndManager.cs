using UnityEngine;

public class TheEndManager : MonoBehaviour, IRoundObserver
{
    public Transform objectRespawnPoint;
    public GameObject key;
    private GameObject keyInstance;

    private void Start()
    {
        RoundManager.Instance.RegisterObserver(this);
    }

    public void OnRoundStarted(int round)
    {
        if (round == 7)
        {
            keyInstance = Instantiate(key, objectRespawnPoint.position, objectRespawnPoint.rotation);
        }
    }

    public void OnRoundCleared(int round)
    {
        if (round == 7 && keyInstance != null)
        {
            Destroy(keyInstance);
        }
    }

    private void OnDestroy()
    {
        RoundManager.Instance.UnregisterObserver(this);
    }
}
