﻿using Application.Common.Interfaces;
using Lab2.Infrastructure.Persistence;
using OOP_WebApp.Domain.Entities;

namespace Lab2.Infrastructure.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(OopWebAppContext context) : base(context)
    {
    }

    public async Task<User?> Get(Username id, CancellationToken cancellationToken) => await Context.Users.FindAsync(id);

    public Task Create(User user, CancellationToken cancellationToken)
    {
        Context.Users.Add(user);
        return Task.CompletedTask;
    }
}