using MapleLeaf.App;
using MapleLeaf.App.Coupons;
using MapleLeaf.App.Coupons.Rules;
using MapleLeaf.App.Coupons.Discounts;
using MapleLeaf.App.Coupons.Specifications;

namespace MapleLeaf.Tests;

public class CouponEngineTests
{
    private PizzaOrder Make(params (string name, decimal price)[] pizzas)
    {
        var order = new PizzaOrder("Test");
        foreach (var p in pizzas)
            order.AddPizza(new Pizza(p.name, p.price));
        return order;
    }

    [Fact]
    public void Evaluate_NoRulesApplied_ReturnsSubtotal()
    {
        var engine = new AggregatedCouponEngine(Array.Empty<CouponRule>());
        var order = Make(("M", 10m));
        var eval = engine.Evaluate(order);
        Assert.Equal(order.TotalPrice, eval.Subtotal);
        Assert.Empty(eval.AppliedCoupons);
        Assert.Equal(order.TotalPrice, eval.FinalTotal);
    }

    [Fact]
    public void Evaluate_PicksBestPerCategoryAndStacks()
    {
        var rules = new[]
        {
            new CouponRule("SAVE5","$5 off >=40", CouponCategory.General,false,new MinTotalSpec(40m), new FlatDiscount(5m)),
            new CouponRule("VOL10","10% off 3+ pizzas", CouponCategory.Volume,true,new MinPizzaCountSpec(3), new PercentDiscount(0.10m)),
            new CouponRule("BUNDLE3","$3 off bundle", CouponCategory.Bundle,true,new ContainsPizzaSpec("Margherita").And(new ContainsPizzaSpec("Pepperoni")), new FlatDiscount(3m))
        };
        var engine = new AggregatedCouponEngine(rules);
        var order = Make(("Margherita",12.99m),("Pepperoni",14.99m),("Veg",13.99m)); // total 41.97
        var eval = engine.Evaluate(order);
        Assert.True(eval.TotalDiscount > 0);
        // General is non-stackable, but allowed as first plus other stackables by our engine logic.
        Assert.Contains(eval.AppliedCoupons, c => c.Code == "SAVE5");
        Assert.Contains(eval.AppliedCoupons, c => c.Code == "VOL10");
        Assert.Contains(eval.AppliedCoupons, c => c.Code == "BUNDLE3");
    }
}
