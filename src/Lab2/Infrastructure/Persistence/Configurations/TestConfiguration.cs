using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Lab2.Infrastructure.Persistence.Configurations;

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.Property(t => t.Id).HasConversion(t => t.Value, t => TestId.From(t));
        builder.Property(t => t.Title).HasConversion(t => t.Value, t => TestTitleString.From(t));
        builder.Property(t => t.Description).HasConversion(t => t.Value, t => TestDescriptionString.From(t));
        builder.Property(t => t.Username).HasConversion(t => t.Value, t => Username.From(t));

        builder.ToTable("Test");
    }
}