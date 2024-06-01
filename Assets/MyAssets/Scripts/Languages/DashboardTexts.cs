using Newtonsoft.Json;

public partial class GameTexts
{
    [JsonProperty("guidances")]
    public Guidances Guidances { get; set; }

    [JsonProperty("rounds")]
    public Round[] Rounds { get; set; }
}

public partial class Guidances
{
    [JsonProperty("pressEToUseDashboard")]
    public string PressEToUseDashboard { get; set; }

    [JsonProperty("resolvePuzzle")]
    public string ResolvePuzzle { get; set; }
}

public partial class Round
{
    [JsonProperty("level")]
    public string Level { get; set; }

    [JsonProperty("proof")]
    public string Proof { get; set; }

    [JsonProperty("error")]
    public string Error { get; set; }
}