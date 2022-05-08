using System.Text.RegularExpressions;
using GameScorer.Exceptions;
using GameScorer.Interfaces;

namespace GameScorer.Parsers;

public class RegexParser : IRoundParser
{
    
    
    
    public Round Parse(string inputstring)
    {

        try
        {
            Regex rx = new Regex(@"\(([^()]*)\)");
            var match = rx.Match(inputstring);

            if (!match.Success) throw new InvalidRoundDataException("No die data found in input string");

            var dieString = match.Groups[1];
            var ruleset = inputstring.Replace(match.Value, "").Trim();
            
            if(string.IsNullOrEmpty(ruleset)) throw new InvalidRoundDataException($"Unable to parse ruleset from input string");
            
            int[] die = dieString.ToString().Split(',').Select(x => Convert.ToInt32(x.Trim())).ToArray();

            if (die.Length != 5)
                throw new InvalidRoundDataException($"Unexpected die count of {die.Length}, expecting 5");
            
            var round = new Round()
            {
                Die = die,
                Ruleset = ruleset
            };

            return round;
        }
        catch (Exception ex)
        {
            throw new InvalidRoundDataException($"Error parsing input string: {ex.Message}");
        }

        
    }
}