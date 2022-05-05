namespace GameScorer.Exceptions;

public class InvalidRoundDataException : Exception
{
    public InvalidRoundDataException(string message) : base(message)
    {
    }
}