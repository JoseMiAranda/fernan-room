using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

public class DashboardTexts : MonoBehaviour
{
    [JsonProperty("guidances")]
    public Guidances Guidances { get; set; }

    [JsonProperty("rounds")]
    public Rounds Rounds { get; set; }
}

public partial class Guidances
{
    [JsonProperty("pressEToUseDashboard")]
    public string PressEToUseDashboard { get; set; }
}

public partial class Rounds
{
    [JsonProperty("one")]
    public string One { get; set; }
}