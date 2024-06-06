using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool dev = false;

    private bool canMove = true;

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
    Transform objectRespawnPoint;
    Transform containerRespawnPoint;

    // All needed objects must be referenced in the script inspector. The objects passed are prefabs. If you delete them, you destroy the prefab in your system (loss of data).
    // All prefab objects must have a copy (objectInstance).

    // Round 1
    public GameObject carScanner;
    private GameObject carScannerInstance;

    // Round 2
    public GameObject students;
    public GameObject gun; 
    private GameObject gunInstance;

    // Round 3
    List<string> secretIngredients = new() { "knife", "mushroom", "fish" };
    public GameObject churro;
    public GameObject foodScanner;
    private GameObject foodScannerInstance;

    // Round 4
    private readonly List<GameObject> bottleScanners = new List<GameObject>();
    private readonly Dictionary<string, string> teacherBottles = new()
    {
        { "Eladio", "Granade" },
        { "Macarena", "Alcohol" },
        { "Antonio", "Lime & Limon" }
    };

    // Round 5
    private const int totalInvisibleObjects = 3;
    public GameObject invisivilityScanner;
    private GameObject invisivilityScannerInstance;
    public GameObject magicMirror;
    private GameObject magicMirrorInstance;
    private readonly List<GameObject> objectGrabbables = new();
    private readonly List<Material> objectGrabbableMaterials = new();
    private readonly List<Material> customMaterials = new();
    private readonly List<int> changedObjectGrabbables = new();

    // Round 6
    private readonly string keyWord = "dash";
    public GameObject computer;
    private GameObject computerInstance;
    public Canvas computerCanvas;
    private void Awake()
    {
        currentSize = mediumNoteSize;

        objectRespawnPoint = GameObject.FindGameObjectWithTag("ScannerRespawn").GetComponent<Transform>();
        containerRespawnPoint = GameObject.FindGameObjectWithTag("ContainerRespawn").GetComponent<Transform>();

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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Use only mouse in game screen
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
        if(dev)
        {
            if (Input.GetKeyUp(KeyCode.F1))
            {
                NextRound();
            }
        }
    }

    public void NextRound()
    {
        ClearRound(round);
        round++;
        GoToRound(round);
    }

    private void ClearRound(int round)
    {
        switch (round)
        {
            case 1:
                ClearRoundOne();
                break;
            case 2:
                ClearRoundTwo();
                break;
            case 3:
                ClearRoundThree();
                break;
            case 4:
                ClearRoundFour();
                break;
            case 5:
                ClearRoundFive();
                break;
            default:
                Debug.LogWarning("You have no round to clear");
                break;

        }
    }

    // Debuging
    private void GoToRound(int round) 
    {
        switch (round)
        {
            case 0:
                TextsRound();
                Introduction();
                break;
            case 1:
                TextsRound();
                RoundOne(); 
                break;
            case 2:
                TextsRound();
                RoundTwo(); 
                break;
            case 3:
                TextsRound();
                RoundThree(); 
                break;
            case 4:
                TextsRound();
                RoundFour(); 
                break;
            case 5:
                TextsRound();
                RoundFive(); 
                break;
            case 6:
                TextsRound();
                RoundSix();
                break;
            default:
                this.round = 0; // Prevents errors when extracts game texts
                TextsRound();
                Introduction();
                break;
                  
        }
    }

    // Rounds 
    public void Introduction()
    {
        currentSize = mediumNoteSize;
    }

    public void RoundOne()
    {
        carScannerInstance = Instantiate(carScanner, objectRespawnPoint.position, objectRespawnPoint.rotation);
        carScannerInstance.GetComponent<Scanner>().Constructor("Tezla", NextRound, ShowWarning);
        currentSize = largeNoteSize;
    }

    public void RoundTwo()
    {
        students.SetActive(true);
        gunInstance = Instantiate(gun, objectRespawnPoint.position, objectRespawnPoint.rotation);
        currentSize = smallNoteSize;
    }

    public void RoundThree()
    {
        foodScannerInstance = Instantiate(foodScanner, objectRespawnPoint.position, objectRespawnPoint.rotation);
        foodScannerInstance.GetComponent<FoodScanner>().Constrcutor(secretIngredients);
        currentSize = smallNoteSize;
    }

    public void RoundFour()
    {
        currentSize = mediumNoteSize;
        int count = 0;
        foreach (var teacherBootle in teacherBottles)
        {
            carScanner.GetComponent<Transform>().localScale = new Vector3(0.1f, 1f, 0.1f); //! Test
            float scannerLenght = carScanner.GetComponent<MeshRenderer>().bounds.size.x;
            Vector3 newPosition = objectRespawnPoint.transform.position + new Vector3(count * (scannerLenght + 0.2f) - 1f, 0, 0); // Position between other bottle scanners
            bottleScanners.Add(Instantiate(carScanner, newPosition, objectRespawnPoint.rotation));
            bottleScanners[count].GetComponent<Scanner>().Constructor(teacherBootle.Value, CheckBootleScanners, CheckBootleScanners);
            count++;
        }
    }

    public void RoundFive()
    {
        magicMirrorInstance = Instantiate(magicMirror, objectRespawnPoint.position, objectRespawnPoint.rotation);
        invisivilityScannerInstance = Instantiate(invisivilityScanner, containerRespawnPoint.position, containerRespawnPoint.rotation);
        invisivilityScannerInstance.transform.Find("Collider").GetComponent<Container>().TotalInvisibleObjects(totalInvisibleObjects);
        currentSize = smallNoteSize;
        GetGrabableObjects();
        RandomInvisibleObjects();
    }

    public void RoundSix()
    {
        currentSize = smallNoteSize;
        computerInstance = Instantiate(computer, objectRespawnPoint.position, objectRespawnPoint.rotation);
        computerInstance.transform.Find("display").GetComponent<Computer>().Constructor(computerCanvas, keyWord);
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
            if(isValid)
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
    }

    public void ClearRoundTwo()
    {
        Destroy(students);
        Destroy(gunInstance);
    }

    public void ClearRoundThree()
    {
        PartyChurros();
        Destroy(foodScannerInstance);
    }

    void ClearRoundFour()
    {
        // In progress
        foreach (var bottleScanner in bottleScanners)
        {
            Destroy(bottleScanner);
        }
    }

    public void ClearRoundFive()
    {
        foreach (int index in changedObjectGrabbables)
        {
            objectGrabbables[index].SetActive(true);
            objectGrabbables[index].layer = 0; // 0 = Default
            objectGrabbables[index].GetComponent<Renderer>().material = objectGrabbableMaterials[index];
        }
        Destroy(invisivilityScannerInstance);
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
        NextRound();
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

    public bool CanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    internal void ShowWarning()
    {
        warningText.text = gameTexts.Rounds[round].Error;
    }

    private void TextsRound()
    {
        roundText.text = GetRoundText(round);
        warningText.text = "";
        guidanceText.text = gameTexts.Guidances.PressEToUseDashboard;
        proof = gameTexts.Rounds[round].Proof;
    }

    public string GetRoundText(int round)
    {
        return gameTexts.Rounds[round].Level;
    }
}
