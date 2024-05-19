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
    private float normalNoteSize = 36f; // For short proofs
    private float smallNoteSize = 18; // For large proofs

    private void Awake()
    {
        currentSize = normalNoteSize;

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
            RoundOne();
        } else
        {
            Debug.LogWarning("There are more Game Managers!");
        }
    }

    public void RoundOne()
    {
        roundText.text = gameTexts.Rounds.One.Level;
        guidanceText.text = gameTexts.Guidances.PressEToUseDashboard;
        proof = gameTexts.Rounds.One.Proof;
    }

    public void RoundTwo()
    {
        currentSize = smallNoteSize;
        roundText.text = gameTexts.Rounds.Two.Level; 
        guidanceText.text = gameTexts.Guidances.PressEToUseDashboard;
        proof = gameTexts.Rounds.Two.Proof;
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
}
