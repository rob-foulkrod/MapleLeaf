namespace MapleLeaf.App.Coupons;

/// <summary>
/// Result of a single coupon application.
/// </summary>
public sealed class CouponResult
{
    public static CouponResult None { get; } = new("NONE", "No discount", 0m);

    public string Code { get; }
    public string Description { get; }
    public decimal DiscountAmount { get; }

    public CouponResult(string code, string description, decimal discountAmount)
    {
        Code = code;
        Description = description;
        DiscountAmount = discountAmount < 0 ? 0 : discountAmount;
    }
}

/// <summary>
/// Aggregated evaluation output including subtotal and applied coupon breakdown.
/// </summary>
public sealed class CouponEvaluation
{
    public decimal Subtotal { get; }
    public decimal TotalDiscount { get; }
    public decimal FinalTotal => Subtotal - TotalDiscount;
    public IReadOnlyList<CouponResult> AppliedCoupons { get; }

    public CouponEvaluation(decimal subtotal, IEnumerable<CouponResult> results)
    {
        Subtotal = subtotal;
        var list = results.Where(r => r.DiscountAmount > 0).ToList();
        AppliedCoupons = list;
        TotalDiscount = list.Sum(r => r.DiscountAmount);
    }
}
