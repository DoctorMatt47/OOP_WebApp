using System.Text.RegularExpressions;
using OOP_WebApp.Domain.Exceptions;

namespace OOP_WebApp.Domain.ValueObjects;

public record OptionString : ValueObject<string, OptionString>
{
    private static readonly Regex Regex = new(@"[A-Za-z0-9\.,;:!?()""'%\- ]{1,100}");

    protected override void Validate()
    {
        if (!Regex.IsMatch(Value)) throw new DomainArgumentException("String is not matching regex", nameof(Value));
    }
}
