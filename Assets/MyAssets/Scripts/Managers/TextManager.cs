using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public enum Guidance { board, resolve }
public class TextManager
{
    private static TextManager instance;
    private const string GameTextsPath = "Jsons/game_texts.json";

    private GameTexts gameTexts;
    private static TextMeshProUGUI roundText;
    private static TextMeshProUGUI guidanceText;
    private static TextMeshProUGUI warningText;

    private readonly float largeNoteSize = 36f;
    private readonly float mediumNoteSize = 24f;
    private readonly float smallNoteSize = 18;

    public static bool IsReady { get; private set; }

    public static TextManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TextManager();
            }
            return instance;
        }
    }

    private TextManager() { }

    public static IEnumerator Initialize()
    {
        if (IsReady)
        {
            yield break;
        }

        if (instance == null)
        {
            instance = new TextManager();
        }

        string json = null;
        string sourcePath = Path.Combine(Application.streamingAssetsPath, GameTextsPath);

#if UNITY_WEBGL && !UNITY_EDITOR
        string url = sourcePath.Replace("\\", "/");
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to load game texts from {url}: {request.error}");
                yield break;
            }

            json = request.downloadHandler.text;
        }
#else
        try
        {
            json = File.ReadAllText(sourcePath);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load game texts from {sourcePath}: {e.Message}");
            yield break;
        }

        yield return null;
#endif

        try
        {
            instance.gameTexts = JsonConvert.DeserializeObject<GameTexts>(json);
            if (instance.gameTexts == null)
            {
                Debug.LogError("Failed to deserialize game_texts.json");
                yield break;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to parse game_texts.json: {e.Message}");
            yield break;
        }

        if (!BindUiTexts())
        {
            yield break;
        }

        IsReady = true;
    }

    private static bool BindUiTexts()
    {
        GameObject roundObject = GameObject.FindWithTag("Round");
        GameObject guidanceObject = GameObject.FindWithTag("Guidance");
        GameObject warningObject = GameObject.FindWithTag("Warning");

        if (roundObject == null || guidanceObject == null || warningObject == null)
        {
            Debug.LogError("Failed to find Round, Guidance, or Warning UI objects by tag.");
            return false;
        }

        roundText = roundObject.GetComponent<TextMeshProUGUI>();
        guidanceText = guidanceObject.GetComponent<TextMeshProUGUI>();
        warningText = warningObject.GetComponent<TextMeshProUGUI>();

        if (roundText == null || guidanceText == null || warningText == null)
        {
            Debug.LogError("Round, Guidance, or Warning UI objects are missing TextMeshProUGUI components.");
            return false;
        }

        return true;
    }

    public String Proof(int round)
    {
        if (!IsReady)
        {
            Debug.LogError("TextManager is not ready. Call Initialize() before accessing proof text.");
            return string.Empty;
        }

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
            6 => smallNoteSize,
            _ => mediumNoteSize,
        };
    }

    public void ShowGuidance(Guidance guidance)
    {
        if (!IsReady)
        {
            Debug.LogError("TextManager is not ready. Call Initialize() before showing guidance.");
            return;
        }

        guidanceText.text = guidance is Guidance.board ? gameTexts.Guidances.PressEToUseDashboard : gameTexts.Guidances.ResolvePuzzle;
    }

    public void ShowWarning(int round)
    {
        if (!IsReady)
        {
            Debug.LogError("TextManager is not ready. Call Initialize() before showing warnings.");
            return;
        }

        warningText.text = gameTexts.Rounds[round].Error;
    }

    public void ShowWarning(string text)
    {
        if (!IsReady)
        {
            Debug.LogError("TextManager is not ready. Call Initialize() before showing warnings.");
            return;
        }

        warningText.text = text;
    }

    public void ClearWarning()
    {
        if (!IsReady)
        {
            return;
        }

        warningText.text = "";
    }

    public void ShowTextsRound(int round)
    {
        if (!IsReady)
        {
            Debug.LogError("TextManager is not ready. Call Initialize() before showing round text.");
            return;
        }

        roundText.text = gameTexts.Rounds[round].Level;
        warningText.text = "";
        ShowGuidance(Guidance.board);
    }
}
