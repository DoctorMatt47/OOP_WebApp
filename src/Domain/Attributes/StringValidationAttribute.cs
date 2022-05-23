using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using OOP_WebApp.Domain.Exceptions;

namespace OOP_WebApp.Domain.Attributes;

public class StringValidationAttribute
{
    private readonly Regex _regex;
    private readonly StringLengthAttribute _attr;

    public StringValidationAttribute(int min, int max, string regex)
    {
        _regex = new Regex(regex);
        _attr = new StringLengthAttribute(max)
        {
            MinimumLength = min
        };
    }
    
    public void Validate(string value)
    {
        if (_attr.IsValid(value)) throw new DomainArgumentException("Incorrect string length", value);
        if (!_regex.IsMatch(value)) throw new DomainArgumentException("String is not match regex", nameof(value));
    }
}
