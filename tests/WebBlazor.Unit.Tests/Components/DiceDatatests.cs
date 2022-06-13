using FluentAssertions;
using WebBlazor.Components;
using Xunit;

namespace WebBlazor.Unit.Tests.Components;

public class DiceDatatests
{
    [Fact]
    public void Ctor_ShouldSetValues()
    {
        var diceData = new DiceData(2, 6);

        diceData.Index.Should().Be(2);
        diceData.Value.Should().Be(6);
    }
}