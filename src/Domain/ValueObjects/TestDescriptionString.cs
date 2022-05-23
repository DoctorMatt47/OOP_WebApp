using System.Text.RegularExpressions;
using OOP_WebApp.Domain.Exceptions;

namespace OOP_WebApp.Domain.ValueObjects;

public record TestDescriptionString : ValueObject<string, TestDescriptionString>
{
    private static readonly Regex Regex = new(@"[A-Za-z0-9\.,;:!?()""'%\- ]{0,1000}");

    protected override void Validate()
    {
        if (!Regex.IsMatch(Value)) throw new DomainArgumentException("String is not matching regex", nameof(Value));
    }
}
