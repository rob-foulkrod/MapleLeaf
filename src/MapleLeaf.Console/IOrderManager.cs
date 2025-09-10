namespace MapleLeaf.App;

/// <summary>
/// Abstraction over order management operations to enable testing and future alternative implementations.
/// </summary>
public interface IOrderManager
{
    void AddOrder(PizzaOrder order);
    IReadOnlyList<PizzaOrder> GetAllOrders();
    PizzaOrder? GetOrderById(int id);
}
