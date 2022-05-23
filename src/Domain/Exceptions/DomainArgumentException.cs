namespace OOP_WebApp.Domain.Exceptions;

public class DomainArgumentException : ArgumentException
{
    public DomainArgumentException(string msg, string paramName) : base(msg, paramName)
    {
    }
    
    public DomainArgumentException(string msg, string paramName, Exception innerException) :
        base(msg, paramName, innerException)
    {
    }
}
