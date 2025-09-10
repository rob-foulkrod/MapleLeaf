using MapleLeaf.App.Coupons.Discounts;
using MapleLeaf.App.Coupons.Specifications;
using MapleLeaf.App;

namespace MapleLeaf.App.Coupons.Rules;

public enum CouponCategory { General, Volume, Bundle }

/// <summary>
/// Represents a single coupon rule composed of a specification and a discount strategy.
/// </summary>
public sealed class CouponRule
{
    public string Code { get; }
    public string Description { get; }
    public CouponCategory Category { get; }
    public bool Stackable { get; }
    private readonly ISpecification<PizzaOrder> _spec;
    private readonly IDiscount _discount;

    public CouponRule(string code, string description, CouponCategory category, bool stackable,
        ISpecification<PizzaOrder> spec, IDiscount discount)
    {
        Code = code;
        Description = description;
        Category = category;
        Stackable = stackable;
        _spec = spec;
        _discount = discount;
    }

    public bool Applies(PizzaOrder order) => _spec.IsSatisfiedBy(order);

    public CouponResult Execute(PizzaOrder order)
    {
        if (!Applies(order)) return CouponResult.None;
        var amount = _discount.Compute(order);
        return amount > 0 ? new CouponResult(Code, Description, amount) : CouponResult.None;
    }
}
