using GameScorer.Interfaces;

namespace GameScorer.Rulesets;

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

        var scoreRegister = new Dictionary<int, int>();
        foreach (var dice in die)
        {
            if (!scoreRegister.ContainsKey(dice)) scoreRegister[dice] = 0;
            scoreRegister[dice]++;
        }

        return scoreRegister.FirstOrDefault(x => x.Key == _targetDice).Value * _targetDice;
    }
}