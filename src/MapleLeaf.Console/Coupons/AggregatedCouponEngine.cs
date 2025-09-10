using MapleLeaf.App.Coupons.Rules;
using MapleLeaf.App;

namespace MapleLeaf.App.Coupons;

public interface ICouponEngine
{
    CouponEvaluation Evaluate(PizzaOrder order);
}

/// <summary>
/// Coupon engine that evaluates all rules, keeps the best per category, then applies stack rules.
/// </summary>
public sealed class AggregatedCouponEngine : ICouponEngine
{
    private readonly IReadOnlyList<CouponRule> _rules;

    public AggregatedCouponEngine(IEnumerable<CouponRule> rules)
    {
        _rules = rules.ToList();
    }

    public CouponEvaluation Evaluate(PizzaOrder order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));

        var subtotal = order.TotalPrice;
        var applicable = _rules
            .Where(r => r.Applies(order))
            .Select(r => r.Execute(order))
            .Where(r => r.DiscountAmount > 0)
            .ToList();

        if (!applicable.Any())
            return new CouponEvaluation(subtotal, applicable);

        // Best per category
        var bestPerCategory = applicable
            .GroupBy(r => _rules.First(rr => rr.Code == r.Code).Category)
            .Select(g => g.OrderByDescending(r => r.DiscountAmount).First())
            .OrderByDescending(r => r.DiscountAmount)
            .ToList();

        var applied = new List<CouponResult>();
        foreach (var res in bestPerCategory)
        {
            var rule = _rules.First(rr => rr.Code == res.Code);
            if (rule.Stackable || !applied.Any())
                applied.Add(res);
        }

        return new CouponEvaluation(subtotal, applied);
    }
}

/// <summary>
/// Null-object pattern implementation used if engine not registered (backwards compatibility).
/// </summary>
public sealed class NullCouponEngine : ICouponEngine
{
    public static ICouponEngine Instance { get; } = new NullCouponEngine();
    private NullCouponEngine() { }
    public CouponEvaluation Evaluate(PizzaOrder order) => new(order.TotalPrice, Array.Empty<CouponResult>());
}
