namespace GameScorer.Interfaces;

/// <summary>
/// A Ruleset Interface
/// </summary>
public interface IRuleset
{
    /// <summary>
    /// Accept an array of ints and return a calculated score
    /// </summary>
    /// <param name="die">The die values</param>
    /// <returns>The calculated score</returns>
    int CalculateScore(int[] die);
}