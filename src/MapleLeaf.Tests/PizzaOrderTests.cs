using MapleLeaf.App;

namespace MapleLeaf.Tests;

public class PizzaOrderTests
{
    [Fact]
    public void PizzaOrder_Constructor_ShouldCreateOrderWithValidCustomerName()
    {
        // Arrange & Act
        var order = new PizzaOrder("John Doe");

        // Assert
        Assert.Equal("John Doe", order.CustomerName);
        Assert.True(order.Id > 0);
        Assert.True(order.OrderDate <= DateTime.Now);
        Assert.Empty(order.Pizzas);
        Assert.Equal(0m, order.TotalPrice);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void PizzaOrder_Constructor_ShouldThrowArgumentException_WhenCustomerNameIsInvalid(string invalidName)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new PizzaOrder(invalidName));
    }

    [Fact]
    public void AddPizza_ShouldAddPizzaToOrder()
    {
        // Arrange
        var order = new PizzaOrder("Jane Doe");
        var pizza = new Pizza("Margherita", 12.99m);

        // Act
        order.AddPizza(pizza);

        // Assert
        Assert.Single(order.Pizzas);
        Assert.Contains(pizza, order.Pizzas);
        Assert.Equal(12.99m, order.TotalPrice);
    }

    [Fact]
    public void AddPizza_ShouldThrowArgumentNullException_WhenPizzaIsNull()
    {
        // Arrange
        var order = new PizzaOrder("Jane Doe");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => order.AddPizza(null));
    }

    [Fact]
    public void TotalPrice_ShouldCalculateCorrectTotal_WithMultiplePizzas()
    {
        // Arrange
        var order = new PizzaOrder("John Doe");
        var pizza1 = new Pizza("Margherita", 12.99m);
        var pizza2 = new Pizza("Pepperoni", 14.99m);

        // Act
        order.AddPizza(pizza1);
        order.AddPizza(pizza2);

        // Assert
        Assert.Equal(27.98m, order.TotalPrice);
    }
}