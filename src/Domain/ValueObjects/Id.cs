namespace OOP_WebApp.Domain.ValueObjects;

public abstract record Id<TValue, TThis> : ValueObject<TValue, TThis> where TThis : ValueObject<TValue, TThis>, new();