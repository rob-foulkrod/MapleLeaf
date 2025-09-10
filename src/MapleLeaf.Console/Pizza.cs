namespace MapleLeaf.App;

public class Pizza
{
    public string Name { get; }
    public decimal Price { get; }

    public Pizza(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Pizza name cannot be empty", nameof(name));
        
        if (price <= 0)
            throw new ArgumentException("Pizza price must be greater than zero", nameof(price));

        Name = name;
        Price = price;
    }
}