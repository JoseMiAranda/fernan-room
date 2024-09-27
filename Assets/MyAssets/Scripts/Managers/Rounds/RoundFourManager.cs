using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundFourManager : MonoBehaviour, IRoundObserver
{
    public GameObject drikScanner;
    public Transform objectRespawnPoint;

    private readonly List<GameObject> bottleScanners = new List<GameObject>();
    private readonly Dictionary<string, string> teacherBottles = new()
         {
             { "Eladio", "Granade" },
             { "Macarena", "Alcohol" },
             { "Antonio", "Lime & Limon" }
         };

    private void Start()
    {
        RoundManager.Instance.RegisterObserver(this);
    }

    public void OnRoundStarted(int round)
    {
        if (round == 4)
        {
            int count = 0;
            foreach (var teacherBootle in teacherBottles)
            {
                drikScanner.GetComponent<Transform>().localScale = new Vector3(0.1f, 1f, 0.1f);
                float scannerLenght = drikScanner.GetComponent<MeshRenderer>().bounds.size.x;
                Vector3 newPosition = objectRespawnPoint.transform.position + new Vector3(count * (scannerLenght + 0.2f) - 1f, 0, 0); // Position between other bottle scanners
                bottleScanners.Add(Instantiate(drikScanner, newPosition, objectRespawnPoint.rotation));
                Scanner bootleScanner = bottleScanners[count].GetComponent<Scanner>();
                bootleScanner.Tags = new List<string> { teacherBootle.Value };
                bootleScanner.OnSuccess = CheckBootleScanners;
                bootleScanner.OnFailure = CheckBootleScanners;
                count++;
            }
        }
    }

    internal void CheckBootleScanners()
    {
        if (bottleScanners.Any(scanner => scanner.GetComponent<Scanner>().Count() == 0))
        {
            TextManager.Instance.ClearWarning();
            return;
        }
        else if (bottleScanners.All(scanner => scanner.GetComponent<Scanner>().IsFound() && scanner.GetComponent<Scanner>().Count() == 1))
        {
            GameManager.Instance.NextRound();
        }
        else
        {
            TextManager.Instance.ShowWarning(4);
        }
    }

    public void OnRoundCleared(int round)
    {
        if (round == 4 && bottleScanners.Count == 3)
        {
            foreach (var bottleScanner in bottleScanners)
            {
                Destroy(bottleScanner);
            }
        }
    }

    private void OnDestroy()
    {
        RoundManager.Instance.UnregisterObserver(this);
    }
}
