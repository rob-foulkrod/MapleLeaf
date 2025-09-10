namespace MapleLeaf.App.Coupons.Specifications;

/// <summary>
/// Generic specification interface supporting basic boolean combinators.
/// </summary>
/// <typeparam name="T">Target type under evaluation.</typeparam>
public interface ISpecification<T>
{
    bool IsSatisfiedBy(T target);

    ISpecification<T> And(ISpecification<T> other) => new AndSpec<T>(this, other);
    ISpecification<T> Or(ISpecification<T> other) => new OrSpec<T>(this, other);
    ISpecification<T> Not() => new NotSpec<T>(this);
}

internal sealed class AndSpec<T>(ISpecification<T> a, ISpecification<T> b) : ISpecification<T>
{
    public bool IsSatisfiedBy(T target) => a.IsSatisfiedBy(target) && b.IsSatisfiedBy(target);
}

internal sealed class OrSpec<T>(ISpecification<T> a, ISpecification<T> b) : ISpecification<T>
{
    public bool IsSatisfiedBy(T target) => a.IsSatisfiedBy(target) || b.IsSatisfiedBy(target);
}

internal sealed class NotSpec<T>(ISpecification<T> inner) : ISpecification<T>
{
    public bool IsSatisfiedBy(T target) => !inner.IsSatisfiedBy(target);
}
