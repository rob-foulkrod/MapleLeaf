using MapleLeaf.App;

namespace MapleLeaf.App.Coupons.Discounts;

/// <summary>
/// Discount strategy abstraction.
/// </summary>
public interface IDiscount
{
    decimal Compute(PizzaOrder order);
}

/// <summary>
/// Flat (fixed amount) discount.
/// </summary>
public sealed class FlatDiscount(decimal amount) : IDiscount
{
    public decimal Compute(PizzaOrder order) => amount;
}

/// <summary>
/// Percentage-based discount against order subtotal.
/// </summary>
public sealed class PercentDiscount(decimal percent) : IDiscount
{
    public decimal Compute(PizzaOrder order) => Math.Round(order.TotalPrice * percent, 2);
}
