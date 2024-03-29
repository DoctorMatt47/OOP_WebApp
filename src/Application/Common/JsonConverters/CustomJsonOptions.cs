﻿using System.Text.Json;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Application.Common.JsonConverters;

public static class CustomJsonOptions
{
    public static JsonSerializerOptions Update(JsonSerializerOptions options)
    {
        var converters = options.Converters;
        converters.Add(new ValueObjectJsonConverter<string, OptionString>());
        converters.Add(new ValueObjectJsonConverter<string, QuestionString>());
        converters.Add(new ValueObjectJsonConverter<string, TestDescriptionString>());
        converters.Add(new ValueObjectJsonConverter<string, TestTitleString>());
        converters.Add(new IdJsonConverter<Guid, OptionId>());
        converters.Add(new IdJsonConverter<Guid, QuestionId>());
        converters.Add(new IdJsonConverter<Guid, TestId>());
        converters.Add(new IdJsonConverter<Guid, AnswerId>());
        converters.Add(new IdJsonConverter<string, Username>());
        options.PropertyNameCaseInsensitive = true;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.WriteIndented = true;
        return options;
    }
}