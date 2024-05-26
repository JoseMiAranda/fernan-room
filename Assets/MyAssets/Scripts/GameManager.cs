using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // UI Texts
    private GameTexts gameTexts;
    private static TextMeshProUGUI roundText;
    private static TextMeshProUGUI guidanceText;
    private string jsonPath = @"D:\UnityVR\fernan-room\Assets\Data\Jsons\game_texts.json";
    private string proof = "Proof";

    // Font sizes
    private float currentSize;
    private float largeNoteSize = 36f; // For short proofs
    private float mediumNoteSize = 24f; // For normal proof
    private float smallNoteSize = 18; // For large proofs

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
    public GameObject foodScanner;
    private GameObject foodScannerInstance;

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
            foreach (var i in Object.FindObjectsOfType<ObjectGrabbable>())
            {
                i.resetTransform();
            }
        }
    }

    public void Introduction()
    {
        currentSize = mediumNoteSize;
        roundText.text = gameTexts.Rounds.Introduction.Level;
        guidanceText.text = gameTexts.Guidances.PressEToUseDashboard;
        proof = gameTexts.Rounds.Introduction.Proof;
    }

    public void RoundOne()
    {
        round = 1;
        carScannerInstance = Instantiate(carScanner, scannerRespawnPoint.position, scannerRespawnPoint.rotation);
        currentSize = largeNoteSize;
        roundText.text = gameTexts.Rounds.One.Level;
        guidanceText.text = gameTexts.Guidances.PressEToUseDashboard;
        proof = gameTexts.Rounds.One.Proof;
    }

    public void RoundTwo()
    {
        round = 2;
        Destroy(carScannerInstance);
        gunInstance = Instantiate(gun, gunRespawnPoint.position, gunRespawnPoint.rotation);
        currentSize = smallNoteSize;
        roundText.text = gameTexts.Rounds.Two.Level; 
        guidanceText.text = gameTexts.Guidances.PressEToUseDashboard;
        proof = gameTexts.Rounds.Two.Proof;
    }

    public void RoundThree()
    {
        round = 3;
        Destroy(gunInstance);
        foodScannerInstance = Instantiate(foodScanner, scannerRespawnPoint.position, scannerRespawnPoint.rotation);
        currentSize = mediumNoteSize;
        roundText.text = gameTexts.Rounds.Three.Level;
        guidanceText.text = gameTexts.Guidances.PressEToUseDashboard;
        proof = gameTexts.Rounds.Three.Proof;
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
}
