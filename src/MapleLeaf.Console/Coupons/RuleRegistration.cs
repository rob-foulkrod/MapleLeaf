using MapleLeaf.App.Coupons.Discounts;
using MapleLeaf.App.Coupons.Rules;
using MapleLeaf.App.Coupons.Specifications;
using Microsoft.Extensions.DependencyInjection;

namespace MapleLeaf.App.Coupons;

public static class RuleRegistration
{
    /// <summary>
    /// Registers the coupon engine with an initial set of rules. Adjust or externalize as needed.
    /// </summary>
    public static IServiceCollection AddCouponEngine(this IServiceCollection services)
    {
        var rules = new List<CouponRule>
        {
            new("SAVE5","$5 off orders >= $40", CouponCategory.General, stackable:false,
                new MinTotalSpec(40m), new FlatDiscount(5m)),
            new("PIZZA10","10% off 3+ pizzas", CouponCategory.Volume, stackable:true,
                new MinPizzaCountSpec(3), new PercentDiscount(0.10m)),
            new("BUNDLE3","$3 off Margherita + Pepperoni", CouponCategory.Bundle, stackable:true,
                new ContainsPizzaSpec("Margherita").And(new ContainsPizzaSpec("Pepperoni")),
                new FlatDiscount(3m))
        };

        services.AddSingleton<IEnumerable<CouponRule>>(rules);
        services.AddSingleton<ICouponEngine, AggregatedCouponEngine>();
        return services;
    }
}
