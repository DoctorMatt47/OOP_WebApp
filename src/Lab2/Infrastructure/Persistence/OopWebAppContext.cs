using Lab2.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using OOP_WebApp.Domain.Entities;

namespace Lab2.Infrastructure.Persistence;

public class OopWebAppContext : DbContext
{
    public OopWebAppContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Option> Options { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Test> Tests { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OptionConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new TestConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}