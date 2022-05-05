using GameScorer.Interfaces;

namespace GameScorer.Rulesets;

public class OfAKindRuleset : IRuleset
{
    private int _targetSetSize { get; set; }
    private const int _minTargetSetSize = 3;
    private const int _maxTargetSetSize = 6;

    public OfAKindRuleset(int targetSetSize)
    {
        if (targetSetSize is < 3 or > 6)
        {
            throw new ArgumentException($"Target must be between {_minTargetSetSize} and {_maxTargetSetSize}");
        }

        _targetSetSize = targetSetSize;
    }
    
    public int CalculateScore(int[] die)
    {
        var scoreRegister = die
            .GroupBy(s => s)
            .Select(s => new { 
                Dice = s.Key, 
                Count = s.Count() 
            });

        var match = scoreRegister.Where(x => x.Count >= _targetSetSize).FirstOrDefault();
        if (match is null) return 0;
        return match.Dice * _targetSetSize;
    }
}