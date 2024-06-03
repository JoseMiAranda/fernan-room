using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

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

    // Round control
    int round = 0;

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
            Introduction();
        } else
        {
            Debug.LogWarning("There are one more Game Managers!");
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q)) {
            // Reset grabbable objects 
            foreach (var i in UnityEngine.Object.FindObjectsOfType<ObjectGrabbable>())
            {
                i.resetTransform();
            }
        }
    }

    public void Introduction()
    {
        currentSize = mediumNoteSize;
        TextsRound();
    }

    public void RoundOne()
    {
        round = 1;
        carScannerInstance = Instantiate(carScanner, scannerRespawnPoint.position, scannerRespawnPoint.rotation);
        carScannerInstance.GetComponent<Scanner>().Constructor("Tezla", RoundTwo, ShowWarning);
        currentSize = largeNoteSize;
        TextsRound();
    }

    public void RoundTwo()
    {
        round = 2;
        Destroy(carScannerInstance);
        gunInstance = Instantiate(gun, gunRespawnPoint.position, gunRespawnPoint.rotation);
        currentSize = smallNoteSize;
        TextsRound();
    }

    public void RoundThree()
    {
        round = 3;
        Destroy(gunInstance);
        foodScannerInstance = Instantiate(foodScanner, scannerRespawnPoint.position, scannerRespawnPoint.rotation);
        currentSize = smallNoteSize;
        TextsRound();
    }

    public void RoundFour()
    {
        round = 4;
        PartyChurros();
        Destroy(foodScannerInstance);
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
        Debug.Log("Round Five");
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
