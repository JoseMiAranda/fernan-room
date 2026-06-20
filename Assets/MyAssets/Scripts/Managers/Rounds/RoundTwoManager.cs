using UnityEngine;

public class RoundTwoManager : MonoBehaviour, IRoundObserver
{
    public GameObject useToolCanvas;
    public GameObject students;
    public GameObject gun;
    private GameObject gunInstance;
    public Transform objectRespawnPoint;

    private void Start()
    {
        RoundManager.Instance.RegisterObserver(this);
    }

    public void OnRoundStarted(int round)
    {
        if (round == 2)
        {
            useToolCanvas.SetActive(true);
            students.SetActive(true);
            gunInstance = Instantiate(gun, objectRespawnPoint.position, objectRespawnPoint.rotation);
        }
    }

    public void OnRoundCleared(int round)
    {
        if (round == 2 && students != null && gunInstance != null)
        {
            useToolCanvas.SetActive(false);
            Destroy(students);
            Destroy(gunInstance);
        }
    }

    private void OnDestroy()
    {
        RoundManager.Instance.UnregisterObserver(this);
    }
}
