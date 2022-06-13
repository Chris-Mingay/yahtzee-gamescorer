namespace WebBlazor.Components;

public class DiceData
{
    public int Index { get; set; }
    public int Value { get; set; }

    public DiceData(int index, int value)
    {
        Index = index;
        Value = value;
    }
}