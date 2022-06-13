using GameScorer.Config;
using GameScorer.Exceptions;
using GameScorer.Interfaces;
using GameScorer.Parsers;
using GameScorer.Rulesets;

namespace GameScorer;

/// <summary>
/// A Yahtzee game scorer.
/// </summary>
public class Scorer
{
    public IRoundParser Parser { get; private set; }

    private Dictionary<string, IRuleset> _rulesets { get; set; }
    public int TotalScore { get; private set; }
    public int RulesetCount => _rulesets?.Count() ?? 0;

    /// <summary>
    /// Generate a scorer with the default ruleset and parser
    /// </summary>
    public Scorer()
    {
        Parser = new RoundParser();
        SetDefaultRules();
    }
    
    /// <summary>
    /// Generate a scorer with the provided GameOptions
    /// </summary>
    /// <param name="options">An options set to be used, any properties not defined will revert to the defaults.</param>
    public Scorer(GameScorerOptions options)
    {
        Parser = options.RoundParser ?? new RoundParser();
        if (options.Rulesets is not null)
        {
            _rulesets = options.Rulesets;
        }
        else
        {
            SetDefaultRules();
        }
    }
    
    private void SetDefaultRules()
    {
        _rulesets = new Dictionary<string, IRuleset>();
        _rulesets.Add("ones", new CountNumberRuleset(1));
        _rulesets.Add("twos", new CountNumberRuleset(2));
        _rulesets.Add("threes", new CountNumberRuleset(3));
        _rulesets.Add("fours", new CountNumberRuleset(4));
        _rulesets.Add("fives", new CountNumberRuleset(5));
        _rulesets.Add("sixes", new CountNumberRuleset(6));
        _rulesets.Add("threeofakind", new OfAKindRuleset(3));
        _rulesets.Add("fourofakind", new OfAKindRuleset(4));
        _rulesets.Add("fiveofakind", new OfAKindRuleset(5));
        _rulesets.Add("twopairs", new TwoPairRuleset());
        _rulesets.Add("fullhouse", new FullHouseRuleset());
    }

    /// <summary>
    /// Clear the current ruleset configuration
    /// </summary>
    public void ClearRulesets()
    {
        _rulesets.Clear();
    }

    /// <summary>
    /// Add a ruleset to this GameScorer
    /// </summary>
    /// <param name="rulesetName">A unique ruleset name</param>
    /// <param name="ruleset">An IRuleset implementation</param>
    /// <exception cref="ArgumentNullException">Thrown if neither arguement is provided</exception>
    public void AddRuleset(string rulesetName, IRuleset ruleset)
    {
        if (string.IsNullOrEmpty(rulesetName)) throw new ArgumentNullException(nameof(rulesetName));

        _rulesets[rulesetName] = ruleset ?? throw new ArgumentNullException(nameof(ruleset));
    }

    /// <summary>
    /// Removes a ruleset from the GameScorer by name
    /// </summary>
    /// <param name="rulesetName">The name of the ruleset to be removed</param>
    /// <exception cref="ArgumentNullException">Thrown if rulesetName not provided</exception>
    /// <exception cref="RulesetNotFoundException">Thrown if rulesetName not present in ruleset</exception>
    public void RemoveRuleset(string rulesetName)
    {
        if (string.IsNullOrEmpty(rulesetName)) throw new ArgumentNullException(nameof(rulesetName));
        if (!_rulesets.ContainsKey(rulesetName)) throw new RulesetNotFoundException($"Ruleset: {rulesetName} does not exist");

        _rulesets.Remove(rulesetName);
    }

    /// <summary>
    /// Get the names of each ruleset
    /// </summary>
    /// <returns>A list of ruleset names</returns>
    public List<string> GetRulesets()
    {
        return _rulesets.Keys.ToList();
    }

    /// <summary>
    /// Returns a ruleset from the provided name
    /// </summary>
    /// <param name="rulesetName">The name of the ruleset to return</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Thrown if rulesetName is not provided</exception>
    /// <exception cref="RulesetNotFoundException">Thrown if rulesetName not present in ruleset</exception>
    public IRuleset GetRuleset(string rulesetName)
    {
        if (string.IsNullOrEmpty(rulesetName)) throw new ArgumentNullException(nameof(rulesetName));
        if (!_rulesets.ContainsKey(rulesetName)) throw new RulesetNotFoundException($"Ruleset: {rulesetName} does not exist");

        return _rulesets[rulesetName];
    }

    /// <summary>
    /// Play a round using the provided input string
    /// </summary>
    /// <param name="inputString">The round in string format that must honour the requirement of the Parser</param>
    /// <returns>The parsed round object</returns>
    /// <exception cref="RulesetNotFoundException">Thrown if the the specified ruleset is not known to this GameScorer instance</exception>
    public Round PlayRound(string inputString)
    {
        var round = ParseRound(inputString);
        TotalScore += round.Score;
        return round;
    }

    /// <summary>
    /// Parse input data into a round object
    /// </summary>
    /// <param name="inputString">The input string</param>
    /// <returns>The parsed round object</returns>
    public Round ParseRound(string inputString)
    {
        var round = Parser.Parse(inputString);
        if (!_rulesets.ContainsKey(round.Ruleset)) throw new RulesetNotFoundException($"Ruleset: {round.Ruleset} not found");
        round.Score = _rulesets[round.Ruleset].CalculateScore(round.Die);
        return round;
    }
}