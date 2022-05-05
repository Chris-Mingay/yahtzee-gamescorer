using GameScorer.Interfaces;

namespace GameScorer.Rulesets;

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