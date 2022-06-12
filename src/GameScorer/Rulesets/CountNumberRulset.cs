using GameScorer.Interfaces;

namespace GameScorer.Rulesets;

/// <summary>
/// Count how many of the provided target dice exist in the roll multiplied by the target dice value.
/// </summary>
/// <example>
/// A target dice of 6 calculated against a set of die (1,5,6,2,6) will return a score of 12 (6 * 2).
/// </example>
public class CountNumberRuleset : IRuleset
{
    private int _targetDice { get; set; }

    public CountNumberRuleset(int targetDice)
    {
        if (targetDice is < 1 or > 6)
        {
            throw new ArgumentException($"Target must be between {1} and {6}");
        }
        
        _targetDice = targetDice;
    }
    
    public int CalculateScore(int[] die)
    {
        if (die is null) throw new ArgumentNullException(nameof(die));

        var scoreRegister = die.GroupBy(x => x)
            .Select(s => new
            {
                Dice = s.Key,
                Count = s.Count()
            });

        var target = scoreRegister.FirstOrDefault(x => x.Dice == _targetDice);

        if (target is null) return 0;
        
        return target.Count * _targetDice;
    }
}