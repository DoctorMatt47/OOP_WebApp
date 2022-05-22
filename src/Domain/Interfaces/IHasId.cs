namespace OOP_WebApp.Domain.Interfaces;

public interface IHasId<out T>
{
    T Id { get; }
}
