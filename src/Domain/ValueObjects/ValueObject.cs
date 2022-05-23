namespace OOP_WebApp.Domain.ValueObjects;

public abstract record ValueObject<TValue, TThis> where TThis : ValueObject<TValue, TThis>, new()
{
    public TValue Value { get; protected init; } = default!;

    protected abstract void Validate();

    public static TThis From(TValue value)
    {
        var tThis = new TThis { Value = value };

        tThis.Validate();

        return tThis;
    }
}
