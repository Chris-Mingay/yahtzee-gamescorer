namespace GameScorer;

/// <summary>
/// Yahtzee round object
/// </summary>
public class Round
{
    public int[] Die { get; set; } = Array.Empty<int>();
    public string Ruleset { get; set; }
    public int Score { get; set; }

}