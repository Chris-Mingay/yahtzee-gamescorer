using GameScorer.Exceptions;
using GameScorer.Interfaces;

namespace GameScorer.Parsers;

public class RoundParser : IRoundParser
{
    public Round Parse(string inputstring)
    {
        if (string.IsNullOrEmpty(inputstring)) throw new InvalidRoundDataException("inputString is required");
        
        if(inputstring.Trim().Last() == ')') throw new InvalidRoundDataException("inputString must contain ruleset");

        var tmp = inputstring.Split(')')[0].Trim('(').Split(',');
        var output = new List<int>();
        foreach (var diceString in tmp)
        {
            if (Int32.TryParse(diceString.Trim(), out int diceAsInt))
            {
                output.Add(diceAsInt);
            }
            else
            {
                throw new InvalidRoundDataException($"Invalid input dice '{diceString}'");
            }
        }
        
        var ruleset = inputstring.Split(' ').Last();

        if (output.Count != 5)
            throw new InvalidRoundDataException($"Expecting {5} die from inputString but only found {output.Count}");

        if(string.IsNullOrEmpty(ruleset)) 
            throw new InvalidRoundDataException($"Ruleset not found in inputString");

        return new Round()
        {
            Ruleset = ruleset,
            Die = output.ToArray()
        };

    }
}