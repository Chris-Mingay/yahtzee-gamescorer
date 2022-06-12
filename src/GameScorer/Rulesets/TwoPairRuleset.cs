using GameScorer.Interfaces;

namespace GameScorer.Rulesets;

/// <summary>
/// Look for two pairs and return the total of the matching die.
/// NOTE: A full house (a 3 of a kind set and a pair) is excluded from this check and will return a score of 0
/// NOTE: A 4 of a kind set is excluded from this check and will return a score of 0 
/// </summary>
/// <example>
/// A set of (3, 3, 5, 5, 6) will return a score of 16 (3 + 3 + 5 + 5)</example>
public class TwoPairRuleset : IRuleset
{
    public int CalculateScore(int[] die)
    {
        var scoreRegister = die
            .GroupBy(s => s)
            .Select(s => new { 
                Dice = s.Key, 
                Count = s.Count() 
            });

        var matches = scoreRegister.Where(x => x.Count == 2);

        if (matches.Count() != 2) return 0;
        
        return matches.Sum(x => x.Dice * x.Count);
    }
}