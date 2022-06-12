using GameScorer.Interfaces;

namespace GameScorer.Config;

/// <summary>
/// Configuration object for a game scorer.
/// </summary>
public class GameScorerOptions
{
    /// <summary>
    /// An optional list of rules to be used for this game round
    /// </summary>
    public Dictionary<string, IRuleset>? Rulesets { get; set; }
    /// <summary>
    /// An optional parser to be used for this game round.
    /// </summary>
    public IRoundParser? RoundParser { get; set; }
}