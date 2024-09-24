using Newtonsoft.Json;
using System;
using System.IO;
using TMPro;
using UnityEngine;

public class TextManager
{
    // UI Texts
    private GameTexts gameTexts;
    private static TextMeshProUGUI roundText;
    private static TextMeshProUGUI guidanceText;
    private static TextMeshProUGUI warningText;

    // Font sizes
    private readonly float largeNoteSize = 36f; // For short proofs
    private readonly float mediumNoteSize = 24f; // For normal proof
    private readonly float smallNoteSize = 18; // For large proofs

    public TextManager()
    {
        // Take data from json. Info: https://www.newtonsoft.com/json/help/html/serializingjson.htm
        using (StreamReader streamReader = new(Path.Combine(Application.streamingAssetsPath, "Jsons/game_texts.json")))
        using (JsonTextReader jsonReader = new(streamReader))
        {
            JsonSerializer serializer = new JsonSerializer();
            gameTexts = serializer.Deserialize<GameTexts>(jsonReader);
        }

        // Obtain Text canvas
        roundText = GameObject.FindWithTag("Round").GetComponent<TextMeshProUGUI>();
        guidanceText = GameObject.FindWithTag("Guidance").GetComponent<TextMeshProUGUI>();
        warningText = GameObject.FindWithTag("Warning").GetComponent<TextMeshProUGUI>();
    }

    public void ResolvePuzzle()
    {
        guidanceText.text = gameTexts.Guidances.ResolvePuzzle;
    }

    public String Proof(int round)
    {
        return gameTexts.Rounds[round].Proof;
    }

    public float Size(int round)
    {
        return round switch
        {
            0 => mediumNoteSize,
            1 => largeNoteSize,
            2 => smallNoteSize,
            3 => smallNoteSize,
            4 => mediumNoteSize,
            5 => smallNoteSize,
            6 => mediumNoteSize,
            _ => mediumNoteSize,
        };
    }

    public void ShowWarning(int round)
    {
        warningText.text = gameTexts.Rounds[round].Error;
    }

    public void ShowWarning(string warning)
    {
        warningText.text = warning;
    }

    public void ShowTextsRound(int round)
    {
        roundText.text = gameTexts.Rounds[round].Level;
        warningText.text = "";
        guidanceText.text = gameTexts.Guidances.PressEToUseDashboard;
    }
}
