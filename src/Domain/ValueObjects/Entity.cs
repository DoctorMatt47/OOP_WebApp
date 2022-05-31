using OOP_WebApp.Domain.Interfaces;

namespace OOP_WebApp.Domain.ValueObjects;

public abstract class Entity<T> : IHasId<T>
{
    protected Entity(T id) => Id = id;

    public T Id { get; }
}