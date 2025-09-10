using MapleLeaf.App;

namespace MapleLeaf.Tests;

public class OrderManagerTests
{
    [Fact]
    public void AddOrder_ShouldAddOrderToCollection()
    {
        // Arrange
        var manager = new OrderManager();
        var order = new PizzaOrder("John Doe");

        // Act
        manager.AddOrder(order);

        // Assert
        var orders = manager.GetAllOrders();
        Assert.Single(orders);
        Assert.Contains(order, orders);
    }

    [Fact]
    public void AddOrder_ShouldThrowArgumentNullException_WhenOrderIsNull()
    {
        // Arrange
        var manager = new OrderManager();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => manager.AddOrder(null));
    }

    [Fact]
    public void GetOrderById_ShouldReturnCorrectOrder_WhenOrderExists()
    {
        // Arrange
        var manager = new OrderManager();
        var order = new PizzaOrder("John Doe");
        manager.AddOrder(order);

        // Act
        var foundOrder = manager.GetOrderById(order.Id);

        // Assert
        Assert.NotNull(foundOrder);
        Assert.Equal(order, foundOrder);
    }

    [Fact]
    public void GetOrderById_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange
        var manager = new OrderManager();

        // Act
        var foundOrder = manager.GetOrderById(999);

        // Assert
        Assert.Null(foundOrder);
    }
}