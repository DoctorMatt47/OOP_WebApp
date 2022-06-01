using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Lab2.Infrastructure.Persistence.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.Property(q => q.Id).HasConversion(v => v.Value, v => QuestionId.From(v));
        builder.Property(q => q.String).HasConversion(v => v.Value, v => QuestionString.From(v));
        builder.Property(q => q.TestId).HasConversion(v => v.Value, v => TestId.From(v));

        builder.ToTable("Question");
    }
}