using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // UI Texts
    private GameTexts gameTexts;
    private static TextMeshProUGUI roundText;
    private static TextMeshProUGUI guidanceText;
    private static TextMeshProUGUI warningText;
    private string jsonPath = @"D:\UnityVR\fernan-room\Assets\Data\Jsons\game_texts.json";
    private string proof = "Proof";

    // Font sizes
    private float currentSize;
    private readonly float largeNoteSize = 36f; // For short proofs
    private readonly float mediumNoteSize = 24f; // For normal proof
    private readonly float smallNoteSize = 18; // For large proofs

    // Debubing
    public int round = 0;

    // Respawn Points
    Transform scannerRespawnPoint;
    Transform gunRespawnPoint;

    // All needed objects must be referenced in the script inspector. The objects passed are prefabs. If you delete them, you destroy the prefab in your system (loss of data).
    // All prefab objects must have a copy (objectInstance).

    // Round 1
    public GameObject carScanner;
    private GameObject carScannerInstance;

    // Round 2
    public GameObject gun; 
    private GameObject gunInstance;

    // Round 3
    public GameObject churro;
    public GameObject foodScanner;
    private GameObject foodScannerInstance;

    // Round 4
    private List<GameObject> bottleScanners = new List<GameObject>();
    private readonly Dictionary<string, string> teacherBottles = new()
    {
        { "Eladio", "Granade" },
        { "Macarena", "Alcohol" },
        { "Antonio", "Lime & Limon" }
    };

    // Round 5
    private List<GameObject> objectGrabbables = new();
    private List<Material> objectGrabbableMaterials = new();
    private List<Material> customMaterials = new();
    private List<int> changedObjectGrabbables = new();

    private void Awake()
    {
        currentSize = mediumNoteSize;

        scannerRespawnPoint = GameObject.FindGameObjectWithTag("ScannerRespawn").GetComponent<Transform>();
        gunRespawnPoint = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>();

        // Take data from json. Info: https://www.newtonsoft.com/json/help/html/serializingjson.htm
        using (StreamReader streamReader = new(jsonPath))
        using (JsonTextReader jsonReader = new(streamReader))
        {
            JsonSerializer serializer = new JsonSerializer();
            gameTexts = serializer.Deserialize<GameTexts>(jsonReader);
        }

        // Obtain Text canvas
        roundText = GameObject.FindWithTag("Round").GetComponent<TextMeshProUGUI>();
        guidanceText = GameObject.FindWithTag("Guidance").GetComponent<TextMeshProUGUI>();
        warningText = GameObject.FindWithTag("Warning").GetComponent<TextMeshProUGUI>();

        if (Instance == null) // Singleton pattern
        {
            Instance = this;
        } else
        {
            Debug.LogWarning("There are one more Game Managers!");
        }
        GoToRound(round);
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            // Reset grabbable objects 
            foreach (var i in FindObjectsOfType<ObjectGrabbable>())
            {
                i.resetTransform();
            }
        }
    }

    // Debuging
    private void GoToRound(int round) 
    {
        if(round < 0 || round > 5) // Prevents errors when extracts game texts
        {
           this.round = 0;
        }
        switch (round)
        {
            case 0:
                Introduction();
                break;
            case 1:
                RoundOne(); 
                break;
            case 2: 
                RoundTwo(); 
                break;
            case 3: 
                RoundThree(); 
                break;
            case 4: 
                RoundFour(); 
                break;
            case 5: 
                RoundFive(); 
                break;
            default:
                Introduction();
                break;
                  
        }
    }

    // Rounds 
    public void Introduction()
    {
        currentSize = mediumNoteSize;
        TextsRound();
    }

    public void RoundOne()
    {
        round = 1;
        carScannerInstance = Instantiate(carScanner, scannerRespawnPoint.position, scannerRespawnPoint.rotation);
        carScannerInstance.GetComponent<Scanner>().Constructor("Tezla", ClearRoundOne, ShowWarning);
        currentSize = largeNoteSize;
        TextsRound();
    }

    public void RoundTwo()
    {
        round = 2;
        gunInstance = Instantiate(gun, gunRespawnPoint.position, gunRespawnPoint.rotation);
        currentSize = smallNoteSize;
        TextsRound();
    }

    public void RoundThree()
    {
        round = 3;
        foodScannerInstance = Instantiate(foodScanner, scannerRespawnPoint.position, scannerRespawnPoint.rotation);
        currentSize = smallNoteSize;
        TextsRound();
    }

    public void RoundFour()
    {
        round = 4;
        currentSize = mediumNoteSize;
        int count = 0;
        foreach (var teacherBootle in teacherBottles)
        {
            carScanner.GetComponent<Transform>().localScale = new Vector3(0.1f, 1f, 0.1f); //! Test
            float scannerLenght = carScanner.GetComponent<MeshRenderer>().bounds.size.x;
            Vector3 newPosition = scannerRespawnPoint.transform.position + new Vector3(count * (scannerLenght + 0.2f) - 1f, 0, 0);
            bottleScanners.Add(Instantiate(carScanner, newPosition, scannerRespawnPoint.rotation));
            bottleScanners[count].GetComponent<Scanner>().Constructor(teacherBootle.Value, CheckBootleScanners, CheckBootleScanners);
            count++;
        }
    }

    public void RoundFive()
    {
        GetGrabableObjects();
        RandomInvisibleObjects();
    }

    private void RandomInvisibleObjects()
    {
        int total = 3;
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
            if(isValid)
            {
                changedObjectGrabbables.Add(rndIndex);
                objectGrabbables[rndIndex].gameObject.layer = 8; // 8 = Proximity
                objectGrabbables[rndIndex].GetComponent<Renderer>().material = customMaterials[rndIndex];
                ProximityDetector proximityDetector = objectGrabbables[rndIndex].AddComponent<ProximityDetector>();
                proximityDetector.player = player;
            }
            if (changedObjectGrabbables.Count == total)
            {
                break;
            }
        } while (true);
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

    // Clear Rounds. It is used to preparate the scenary to call the next round in order to use debuging sucessfully
    public void ClearRoundOne()
    {
        Destroy(carScannerInstance);
        RoundTwo();
    }

    public void ClearRoundTwo()
    {
        Destroy(gunInstance);
        RoundThree();
    }

    public void ClearRoundThree()
    {
        PartyChurros();
        Destroy(foodScannerInstance);
        RoundFour();
    }

    void ClearRoundFour()
    {
        // In progress
        foreach (var bottleScanner in bottleScanners)
        {
            Destroy(bottleScanner);
        }
        RoundFive();
    }

    private void CheckBootleScanners()
    {
        foreach (var bottleScanner in bottleScanners)
        {
            if(!bottleScanner.GetComponent<Scanner>().IsFound())
            {
                return;
            }
        }
        // Round Five
        ClearRoundFour();
    }

    private void PartyChurros()
    {
        foodScannerInstance.GetComponent<FoodScanner>().Collider().enabled = false;

        Transform foodScannerTransform = foodScannerInstance.GetComponent<FoodScanner>().transform;

        Vector3 position = foodScannerTransform.position;
        position.y += 0.3f;
        foodScannerTransform.position = position;

        // Disable correct ingredients
        List<ObjectGrabbable> ingredients = foodScannerInstance.GetComponent<FoodScanner>().Ingredients();
        foreach (var ingredient in ingredients)
        {
            ingredient.gameObject.SetActive(false);
        }

        for (int i = 0; i < 11; i++)
        {
            Instantiate(churro, foodScannerTransform.position, foodScannerTransform.rotation);
        }

        foodScannerInstance.GetComponent<FoodScanner>().Explode(false);
    }

    public void ResolvePuzzle()
    {
        guidanceText.text = gameTexts.Guidances.ResolvePuzzle;
    }

    public string getProof()
    {
        return proof;
    }

    public float getSize()
    {
        return currentSize;
    }

    public float getRound()
    {
        return round;
    }

    internal void ShowWarning()
    {
        warningText.text = gameTexts.Rounds[round].Error;
    }

    private void TextsRound()
    {
        roundText.text = gameTexts.Rounds[round].Level;
        warningText.text = "";
        guidanceText.text = gameTexts.Guidances.PressEToUseDashboard;
        proof = gameTexts.Rounds[round].Proof;
    }
}
