using GameScorer.Config;
using GameScorer.Exceptions;
using GameScorer.Interfaces;
using GameScorer.Parsers;
using GameScorer.Rulesets;

namespace GameScorer;

public class Scorer
{
    public IRoundParser Parser { get; private set; }

    private Dictionary<string, IRuleset> _rulesets { get; set; }
    public int TotalScore { get; private set; }
    public int RulesetCount => _rulesets.Count();

    public Scorer()
    {
        Parser = new RoundParser();
        SetDefaultRules();
    }
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

    public void ClearRulesets()
    {
        _rulesets.Clear();
    }

    public void AddRuleset(string rulesetName, IRuleset ruleset)
    {
        if (string.IsNullOrEmpty(rulesetName)) throw new ArgumentNullException(nameof(rulesetName));

        _rulesets[rulesetName] = ruleset ?? throw new ArgumentNullException(nameof(ruleset));
    }

    public void RemoveRuleset(string rulesetName)
    {
        if (string.IsNullOrEmpty(rulesetName)) throw new ArgumentNullException(nameof(rulesetName));
        if (!_rulesets.ContainsKey(rulesetName)) throw new RulesetNotFoundException($"Ruleset: {rulesetName} does not exist");

        _rulesets.Remove(rulesetName);
    }

    public IRuleset GetRuleset(string rulesetName)
    {
        if (string.IsNullOrEmpty(rulesetName)) throw new ArgumentNullException(nameof(rulesetName));
        if (!_rulesets.ContainsKey(rulesetName)) throw new RulesetNotFoundException($"Ruleset: {rulesetName} does not exist");

        return _rulesets[rulesetName];
    }

    public void PlayRound(string inputString)
    {
        var round = Parser.Parse(inputString);
        if (!_rulesets.ContainsKey(round.Ruleset)) throw new RulesetNotFoundException($"Ruleset: {round.Ruleset} not found");
        TotalScore += _rulesets[round.Ruleset].CalculateScore(round.Die);
    }
}