namespace GameScorer.Exceptions;

/// <summary>
/// Exception thrown when the requested ruleset does not exist
/// </summary>
public class RulesetNotFoundException : Exception
{
    public RulesetNotFoundException(string message) : base(message)
    {
        
    }
}