using MapleLeaf.App;

namespace MapleLeaf.App.Coupons.Specifications;

/// <summary>
/// Specification: minimum total price threshold.
/// </summary>
public sealed class MinTotalSpec(decimal min) : ISpecification<PizzaOrder>
{
    public bool IsSatisfiedBy(PizzaOrder order) => order.TotalPrice >= min;
}

/// <summary>
/// Specification: minimum number of pizzas in the order.
/// </summary>
public sealed class MinPizzaCountSpec(int count) : ISpecification<PizzaOrder>
{
    public bool IsSatisfiedBy(PizzaOrder order) => order.Pizzas.Count >= count;
}

/// <summary>
/// Specification: order must contain a pizza by (case-insensitive) name.
/// </summary>
public sealed class ContainsPizzaSpec(string name) : ISpecification<PizzaOrder>
{
    public bool IsSatisfiedBy(PizzaOrder order) =>
        order.Pizzas.Any(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
}
