namespace OOP_WebApp.Domain.ValueObjects;

public abstract record Id<TValue, TThis> where TThis : Id<TValue, TThis>, new()
{
    public TValue Value { get; private init; } = default!;

    public static TThis From(TValue value) => new() {Value = value};
}