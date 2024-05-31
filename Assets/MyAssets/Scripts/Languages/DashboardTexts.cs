using Newtonsoft.Json;

public class GameTexts
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

    [JsonProperty("resolvePuzzle")]
    public string ResolvePuzzle { get; set; }
}

public class Rounds
{
    [JsonProperty("introduction")]
    public Round Introduction { get; set; }

    [JsonProperty("one")]
    public Round One { get; set; }

    [JsonProperty("two")]
    public Round Two { get; set; }

    [JsonProperty("three")]
    public Round Three { get; set; }

    [JsonProperty("four")]
    public Round Four { get; set; }
}

public class Round
{
    [JsonProperty("level")]
    public string Level { get; set; }

    [JsonProperty("proof")]
    public string Proof { get; set; }
}