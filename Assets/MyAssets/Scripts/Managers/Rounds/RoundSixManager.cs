using UnityEngine;

public class RoundSixManager : MonoBehaviour, IRoundObserver
{
    public Transform objectRespawnPoint;
    private readonly string keyWord = "dash";
    public GameObject computer;
    private GameObject computerInstance;
    public Canvas computerCanvas;

    private void Start()
    {
        RoundManager.Instance.RegisterObserver(this);
    }

    public void OnRoundStarted(int round)
    {
        if (round == 6)
        {
            computerInstance = Instantiate(computer, objectRespawnPoint.position, objectRespawnPoint.rotation);
            computerInstance.transform.Find("display").GetComponent<Computer>().Constructor(computerCanvas, keyWord);
        }
    }

    public void OnRoundCleared(int round)
    {
        if (round == 6 && computerInstance != null)
        {
            computerInstance.transform.Find("display").GetComponent<Computer>().UnRead();
            Destroy(computerInstance);
        }
    }

    private void OnDestroy()
    {
        RoundManager.Instance.UnregisterObserver(this);
    }
}
