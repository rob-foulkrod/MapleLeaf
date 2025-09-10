namespace MapleLeaf.App;

public class OrderManager
{
    private readonly List<PizzaOrder> _orders = new();

    public void AddOrder(PizzaOrder order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));

        _orders.Add(order);
    }

    public IReadOnlyList<PizzaOrder> GetAllOrders()
    {
        return _orders.AsReadOnly();
    }

    public PizzaOrder? GetOrderById(int id)
    {
        return _orders.FirstOrDefault(o => o.Id == id);
    }
}