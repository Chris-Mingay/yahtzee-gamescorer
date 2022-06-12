using GameScorer.Interfaces;

namespace GameScorer.Rulesets;

/// <summary>
/// Look for a full house (3 times one dice and 2 times another) and return the total dice value. If the set isn't a full house return 0
/// </summary>
/// <example>
/// (1, 1, 2, 2, 2) is a full house and will return a score of 8 ( 1 + 1 + 2 + 2 + 2 )
/// (1, 2, 3, 4, 5) is not a full house and will return a score of 0
/// </example>
public class FullHouseRuleset : IRuleset
{
    public int CalculateScore(int[] die)
    {
        var scoreRegister = die
            .GroupBy(s => s)
            .Select(s => new { 
                Dice = s.Key, 
                Count = s.Count() 
            });

        var matches = scoreRegister.Where(x => x.Count is 2 or 3);

        if (matches.Count() != 2) return 0;
        
        return matches.Sum(x => x.Dice * x.Count);
    }
}