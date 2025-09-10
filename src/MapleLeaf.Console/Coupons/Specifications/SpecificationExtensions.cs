namespace MapleLeaf.App.Coupons.Specifications;

/// <summary>
/// Extension methods enabling fluent composition of specifications. Adds compatibility where
/// default interface method implementations may not be available or desired.
/// </summary>
public static class SpecificationExtensions
{
    public static ISpecification<T> And<T>(this ISpecification<T> first, ISpecification<T> second) => new AndSpec<T>(first, second);
    public static ISpecification<T> Or<T>(this ISpecification<T> first, ISpecification<T> second) => new OrSpec<T>(first, second);
    public static ISpecification<T> Not<T>(this ISpecification<T> inner) => new NotSpec<T>(inner);
}
