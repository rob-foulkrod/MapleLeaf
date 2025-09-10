using MapleLeaf.App;
using MapleLeaf.App.Coupons;
using MapleLeaf.App.Coupons.Rules;
using MapleLeaf.App.Coupons.Specifications;
using MapleLeaf.App.Coupons.Discounts;

namespace MapleLeaf.Tests;

public class MargheritaDiscountTests
{
    private PizzaOrder Make(params (string name, decimal price)[] pizzas)
    {
        var order = new PizzaOrder("Test Customer");
        foreach (var p in pizzas)
            order.AddPizza(new Pizza(p.name, p.price));
        return order;
    }

    [Fact]
    public void Evaluate_SingleMargherita_Gets4DollarDiscount()
    {
        // Rule: $4 off any order containing a Margherita pizza (non‑stackable general category)
        var rules = new[]
        {
            new CouponRule(
                code: "MARG4",
                description: "$4 off Margherita",
                category: CouponCategory.General,
                stackable: false,
                spec: new ContainsPizzaSpec("Margherita"),
                discount: new FlatDiscount(4m))
        };

        var engine = new AggregatedCouponEngine(rules);
        var order = Make(("Margherita", 12.99m));

        var eval = engine.Evaluate(order);

        Assert.Equal(order.TotalPrice, eval.Subtotal);
        Assert.Single(eval.AppliedCoupons);
        var applied = eval.AppliedCoupons.First();
        Assert.Equal("MARG4", applied.Code);
        Assert.Equal(4m, applied.DiscountAmount);
        Assert.Equal(order.TotalPrice - 4m, eval.FinalTotal);
    }
}
