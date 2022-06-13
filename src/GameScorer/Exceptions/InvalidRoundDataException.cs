namespace GameScorer.Exceptions;

/// <summary>
/// Exception thrown when incoming round data is invalid
/// </summary>
public class InvalidRoundDataException : Exception
{
    public InvalidRoundDataException(string message) : base(message)
    {
    }
}