using OOP_WebApp.Lab2.Infrastructure.Persistence;

namespace OOP_WebApp.Lab2.Infrastructure.Repositories;

public abstract class RepositoryBase
{
    protected readonly OopWebAppContext Context;

    protected RepositoryBase(OopWebAppContext context) => Context = context;
}