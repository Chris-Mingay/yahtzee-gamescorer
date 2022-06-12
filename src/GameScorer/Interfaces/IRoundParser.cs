namespace GameScorer.Interfaces;

public interface IRoundParser
{
    /// <summary>
    /// Generate a Round from an input string
    /// </summary>
    /// <param name="inputstring">The input data to be parsed</param>
    /// <returns>A Round object</returns>
    Round Parse(string inputstring);
}