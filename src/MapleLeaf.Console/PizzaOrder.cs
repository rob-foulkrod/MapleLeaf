namespace MapleLeaf.App;

public class PizzaOrder
{
    private static int _nextId = 1;
    private readonly List<Pizza> _pizzas = new();

    public int Id { get; }
    public string CustomerName { get; }
    public DateTime OrderDate { get; }
    public IReadOnlyList<Pizza> Pizzas => _pizzas.AsReadOnly();
    public decimal TotalPrice => _pizzas.Sum(p => p.Price);

    public PizzaOrder(string customerName)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name cannot be empty", nameof(customerName));

        Id = _nextId++;
        CustomerName = customerName;
        OrderDate = DateTime.Now;
    }

    public void AddPizza(Pizza pizza)
    {
        if (pizza == null)
            throw new ArgumentNullException(nameof(pizza));

        _pizzas.Add(pizza);
    }
}