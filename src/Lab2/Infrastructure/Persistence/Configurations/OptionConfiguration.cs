using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace Lab2.Infrastructure.Persistence.Configurations;

public class OptionConfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.Property(o => o.Id).HasConversion(v => v.Value, v => OptionId.From(v));
        builder.Property(o => o.String).HasConversion(v => v.Value, v => OptionString.From(v));
        builder.Property(q => q.QuestionId).HasConversion(v => v.Value, v => QuestionId.From(v));
    }
}