using System.Collections.Generic;
using UnityEngine;

public class RoundOneManager : MonoBehaviour, IRoundObserver
{
    public Material material;
    public GameObject carScanner;
    private GameObject carScannerInstance;
    public Transform objectRespawnPoint;

    private void Start()
    {
        RoundManager.Instance.RegisterObserver(this);
    }

    public void OnRoundStarted(int round)
    {
        if (round == 1)
        {
            carScannerInstance = Instantiate(carScanner, objectRespawnPoint.position, objectRespawnPoint.rotation);
            Renderer renderer = carScannerInstance.GetComponent<Renderer>();

            if (renderer != null && material != null)
            {
                renderer.material = material;
            }

            Scanner scanner = carScannerInstance.GetComponent<Scanner>();
            scanner.Tags = new List<string> { "Tezla" };
            scanner.OnSuccess = () =>
            {
                GameManager.Instance.NextRound();
            };
            scanner.OnFailure = () =>
            {
                TextManager.Instance.ShowWarning(1);
            };
        }
    }

    public void OnRoundCleared(int round)
    {
        if (round == 1 && carScannerInstance != null)
        {
            Destroy(carScannerInstance);
        }
    }

    private void OnDestroy()
    {
        RoundManager.Instance.UnregisterObserver(this);
    }
}
