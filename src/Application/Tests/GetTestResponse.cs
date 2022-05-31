﻿using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace Application.Tests;

public record GetTestResponse(
    TestId Id,
    TestTitleString Title,
    TestDescriptionString Description,
    UserId UserId,
    IEnumerable<GetQuestionResponse> Questions);