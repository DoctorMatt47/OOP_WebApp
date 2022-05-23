using OOP_WebApp.Domain.Interfaces;

namespace OOP_WebApp.Domain.ValueObjects;

public abstract class Entity<T> : IHasId<T>
{
    public T Id { get; protected init; }
}
