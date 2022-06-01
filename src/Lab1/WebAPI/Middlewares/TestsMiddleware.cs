﻿using System.Text;
using System.Text.Json;
using Application.Tests;
using Lab1.WebAPI.JsonConverters;
using OOP_WebApp.Domain.Entities;

namespace Lab1.WebAPI.Middlewares;

public class TestsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITestService _tests;

    public TestsMiddleware(RequestDelegate next, ITestService tests)
    {
        _next = next;
        _tests = tests;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLowerInvariant();

        if (path is null || !path.StartsWith("/api/tests"))
        {
            await _next.Invoke(context);
            return;
        }

        var task = context.Request.Method switch
        {
            "GET" => GetTests(context),
            "POST" => CreateTest(context),
            _ => MethodNotAllowed(context)
        };
        await task;
    }

    private async Task GetTests(HttpContext context)
    {
        var username = context.User.Identity?.Name;
        if (username is null)
        {
            context.Response.StatusCode = 401;
            return;
        }

        var id = context.Request.Query["id"].ToString();
        if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var guid))
        {
            context.Response.StatusCode = 400;
            return;
        }

        var test = await _tests.Get(TestId.From(guid), context.RequestAborted);
        var testJson = JsonSerializer.Serialize(test, CustomJsonOptions.Get());
        var testBytes = Encoding.UTF8.GetBytes(testJson);

        context.Response.StatusCode = 200;
        context.Response.ContentType = "application/json; charset=utf-8";
        await context.Response.Body.WriteAsync(testBytes, 0, testBytes.Length);
    }

    private async Task CreateTest(HttpContext context)
    {
        var username = context.User.Identity?.Name;
        if (username is null)
        {
            context.Response.StatusCode = 401;
            return;
        }

        var requestBody = await context.Request.BodyReader.ReadAsync();
        var testJson = Encoding.UTF8.GetString(requestBody.Buffer);
        var test = JsonSerializer.Deserialize<CreateTestRequest>(testJson, CustomJsonOptions.Get());

        if (test is null)
        {
            context.Response.StatusCode = 400;
            return;
        }

        var testId = await _tests.Create(test, Username.From(username), context.RequestAborted);
        var testIdJson = JsonSerializer.Serialize(testId, CustomJsonOptions.Get());
        var testIdBytes = Encoding.UTF8.GetBytes(testIdJson);

        context.Response.StatusCode = 200;
        context.Response.ContentType = "application/json; charset=utf-8";
        await context.Response.Body.WriteAsync(testIdBytes, 0, testIdBytes.Length);
    }

    private static Task MethodNotAllowed(HttpContext context)
    {
        context.Response.StatusCode = 405;
        return Task.CompletedTask;
    }
}