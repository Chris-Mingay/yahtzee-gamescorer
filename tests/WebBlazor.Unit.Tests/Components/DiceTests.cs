using Bunit;
using FluentAssertions;
using WebBlazor.Components;
using Xunit;

namespace WebBlazor.Unit.Tests.Components;

public class DiceTests
{
    [Fact]
    public void ClickUp_Should_IncrementDiceValue()
    {
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Dice>(parameters => parameters
            .Add(p => p.Value, 3)
        );
        
        cut.Find(".up-button").Click();

        cut.FindAll(".dot").Count.Should().Be(4);
    }
    
    [Fact]
    public void ClickDown_Should_DecrementDiceValue()
    {
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Dice>(parameters => parameters
            .Add(p => p.Value, 3)
        );
        
        cut.Find(".down-button").Click();

        cut.FindAll(".dot").Count.Should().Be(2);
    }
    
    [Fact]
    public void ClickUp_Should_WrapDiceValue()
    {
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Dice>(parameters => parameters
            .Add(p => p.Value, 6)
        );
        
        cut.Find(".up-button").Click();

        cut.FindAll(".dot").Count.Should().Be(1);
    }
    
    [Fact]
    public void ClickDown_Should_WrapValue()
    {
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Dice>(parameters => parameters
            .Add(p => p.Value, 1)
        );
        
        cut.Find(".down-button").Click();

        cut.FindAll(".dot").Count.Should().Be(6);
    }
    
    [Fact]
    public void ClickDice_Can_ChangeValue()
    {
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Dice>(parameters => parameters
            .Add(p => p.Value, 1)
        );
        
        cut.Find(".dice-button").Click();

        cut.FindAll(".dot").Count.Should().BeInRange(1, 6);
    }
}