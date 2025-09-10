using MapleLeaf.App;

namespace MapleLeaf.Tests;

public class PizzaTests
{
    [Fact]
    public void Pizza_Constructor_ShouldCreatePizzaWithValidNameAndPrice()
    {
        // Arrange & Act
        var pizza = new Pizza("Margherita", 12.99m);

        // Assert
        Assert.Equal("Margherita", pizza.Name);
        Assert.Equal(12.99m, pizza.Price);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Pizza_Constructor_ShouldThrowArgumentException_WhenNameIsInvalid(string invalidName)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Pizza(invalidName, 12.99m));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10.50)]
    public void Pizza_Constructor_ShouldThrowArgumentException_WhenPriceIsInvalid(decimal invalidPrice)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Pizza("Margherita", invalidPrice));
    }
}