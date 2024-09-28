using System.Collections.Generic;
using UnityEngine;

public class RoundFiveManager : MonoBehaviour, IRoundObserver
{
    public Transform objectRespawnPoint;
    public Transform containerRespawnPoint;
    private const int totalInvisibleObjects = 3;
    public GameObject invisivilityScanner;
    private GameObject invisivilityScannerInstance;
    public GameObject magicMirror;
    private GameObject magicMirrorInstance;
    private readonly List<GameObject> objectGrabbables = new();
    private readonly List<Material> objectGrabbableMaterials = new();
    private readonly List<Material> customMaterials = new();
    private readonly List<int> changedObjectGrabbables = new();

    private void Start()
    {
        RoundManager.Instance.RegisterObserver(this);
    }

    public void OnRoundStarted(int round)
    {
        if (round == 5)
        {
            magicMirrorInstance = Instantiate(magicMirror, objectRespawnPoint.position, objectRespawnPoint.rotation);
            invisivilityScannerInstance = Instantiate(invisivilityScanner, containerRespawnPoint.position, containerRespawnPoint.rotation);
            invisivilityScannerInstance.transform.Find("Collider").GetComponent<Container>().TotalInvisibleObjects(totalInvisibleObjects);
            GetGrabableObjects();
            RandomInvisibleObjects();
            TextManager.Instance.ShowWarning("0 / " + totalInvisibleObjects);
        }
    }

    public void OnRoundCleared(int round)
    {
        if (round == 5 && magicMirrorInstance != null && invisivilityScannerInstance != null)
        {
            foreach (int index in changedObjectGrabbables)
            {
                objectGrabbables[index].SetActive(true);
                objectGrabbables[index].layer = 0; // 0 = Default
                objectGrabbables[index].GetComponent<Renderer>().material = objectGrabbableMaterials[index];
            }
            Destroy(invisivilityScannerInstance);
            Destroy(magicMirrorInstance);
        }
    }

    private void ShowWarning()
    {
        Debug.Log("Warning for Round 1!");
    }

    private void OnDestroy()
    {
        RoundManager.Instance.UnregisterObserver(this);
    }

    private void GetGrabableObjects()
    {
        foreach (var objectGrabbable in FindObjectsOfType<ObjectGrabbable>())
        {
            Renderer renderer = objectGrabbable.gameObject.GetComponent<Renderer>();
            if (renderer != null) // Ensures getting objects whose renderer we can extract their materials
            {
                objectGrabbables.Add(objectGrabbable.gameObject);
                Material customMaterial = new Material(Shader.Find("Custom/Proximity"));
                objectGrabbableMaterials.Add(renderer.material);
                customMaterial.SetTexture("_MainTex", renderer.material.mainTexture);
                customMaterials.Add(customMaterial);
            }
        }
    }

    private void RandomInvisibleObjects()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        System.Random rnd = new();
        do
        {
            bool isValid = true;
            int rndIndex = rnd.Next(0, objectGrabbables.Count - 1);
            for (int i = 0; i < changedObjectGrabbables.Count; i++)
            {
                if (changedObjectGrabbables[i] == rndIndex)
                {
                    isValid = false;
                    break;
                }
            }
            if (isValid)
            {
                changedObjectGrabbables.Add(rndIndex);
                objectGrabbables[rndIndex].gameObject.layer = 8; // 8 = Proximity, Main camera must have Proximity layer disabled
                objectGrabbables[rndIndex].GetComponent<Renderer>().material = customMaterials[rndIndex];
                ProximityDetector proximityDetector = objectGrabbables[rndIndex].AddComponent<ProximityDetector>();
                proximityDetector.player = player;
            }
            if (changedObjectGrabbables.Count == totalInvisibleObjects)
            {
                break;
            }
        } while (true);
    }
}
