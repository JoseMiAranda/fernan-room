using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private string currentMessage = "Text Message";
    public static GameManager Instance;
    private static TextMeshProUGUI roundText;
    private static TextMeshProUGUI guidanceText;
    private string jsonPath = @"D:\UnityVR\fernan-room\Assets\Data\Jsons\board_texts.json";
    private DashboardTexts dashboardTexts;
    private void Awake()
    {
        // Take data from json. Info: https://www.newtonsoft.com/json/help/html/serializingjson.htm
        using (StreamReader streamReader = new(jsonPath))
        using (JsonTextReader jsonReader = new(streamReader))
        {
            JsonSerializer serializer = new JsonSerializer();
            dashboardTexts = serializer.Deserialize<DashboardTexts>(jsonReader);
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
        roundText.text = "Ronda 1";
        guidanceText.text = dashboardTexts.Guidances.PressEToUseDashboard;

        currentMessage = dashboardTexts.Rounds.One;
    }

    public void RoundTwo()
    {
        roundText.text = "Ronda 2";
        guidanceText.text = dashboardTexts.Guidances.PressEToUseDashboard;

        currentMessage = "Take the weapon!!!";
    }

    public string getCurrentMessage()
    {
        return currentMessage;
    }
}
