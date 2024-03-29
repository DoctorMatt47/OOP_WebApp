﻿using Microsoft.AspNetCore.Mvc;
using OOP_WebApp.Application.Common.JsonConverters;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Lab2.WebAPI.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddJsonConverters(this IMvcBuilder builder) =>
        builder.AddJsonOptions(opts => { NewMethod(opts); });

    private static void NewMethod(JsonOptions opts)
    {
        var converters = opts.JsonSerializerOptions.Converters;
        converters.Add(new ValueObjectJsonConverter<string, OptionString>());
        converters.Add(new ValueObjectJsonConverter<string, QuestionString>());
        converters.Add(new ValueObjectJsonConverter<string, TestDescriptionString>());
        converters.Add(new ValueObjectJsonConverter<string, TestTitleString>());
        converters.Add(new IdJsonConverter<Guid, OptionId>());
        converters.Add(new IdJsonConverter<Guid, QuestionId>());
        converters.Add(new IdJsonConverter<Guid, TestId>());
        converters.Add(new IdJsonConverter<string, Username>());
    }
}