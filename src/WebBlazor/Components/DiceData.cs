namespace WebBlazor.Components;

/// <summary>
/// Data object for dice
/// </summary>
public class DiceData
{
    /// <summary>
    /// The index of this dice in the yahtzee board
    /// </summary>
    public int Index { get; set; }
    /// <summary>
    /// The value of the dice.
    /// </summary>
    public int Value { get; set; }

    public DiceData(int index, int value)
    {
        Index = index;
        Value = value;
    }
}