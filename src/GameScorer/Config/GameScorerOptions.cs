using GameScorer.Interfaces;

namespace GameScorer.Config;

public class GameScorerOptions
{
    public Dictionary<string, IRuleset> Rulesets { get; set; }
    public IRoundParser RoundParser { get; set; }
}