using GameScorer.Interfaces;

namespace GameScorer.Rulesets;

/// <summary>
/// Returns the sum of die in the specified target set size. If there is no set of the target size, return 0
/// </summary>
/// <example>
/// A target set size of 3 and a a set of die (6, 5, 4, 4, 4) will return a score of 12 (4 + 4 + 4)
/// </example>
public class OfAKindRuleset : IRuleset
{
    private int _targetSetSize { get; set; }
    private const int _minTargetSetSize = 3;
    private const int _maxTargetSetSize = 5;

    /// <summary>
    /// The target set size
    /// </summary>
    /// <param name="targetSetSize">A value between 3 and 5 inclusive</param>
    /// <exception cref="ArgumentException">Thrown if the target size doesn't meet the requirements</exception>
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