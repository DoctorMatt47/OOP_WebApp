using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Lab2.Infrastructure.Persistence.Configurations;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.Property(o => o.Id).HasConversion(v => v.Value, v => AnswerId.From(v));
        builder.Property(o => o.Username).HasConversion(v => v.Value, v => Username.From(v));
        builder.Property(o => o.TestId).HasConversion(v => v.Value, v => TestId.From(v));
        builder.Property(o => o.QuestionId).HasConversion(v => v.Value, v => QuestionId.From(v));
        builder.Property(o => o.OptionId).HasConversion(v => v.Value, v => OptionId.From(v));

        builder.ToTable("Answer");
    }

    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.Property(o => o.Id).HasConversion(v => v.Value, v => OptionId.From(v));
        builder.Property(o => o.String).HasConversion(v => v.Value, v => OptionString.From(v));
        builder.Property(q => q.QuestionId).HasConversion(v => v.Value, v => QuestionId.From(v));

        builder.ToTable("Option");
    }
}