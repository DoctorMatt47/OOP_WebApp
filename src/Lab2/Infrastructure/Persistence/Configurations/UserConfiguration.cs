using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOP_WebApp.Domain.Entities;

namespace Lab2.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id).HasConversion(u => u.Value, u => Username.From(u)).HasColumnName("Username");

        builder.ToTable("User");
    }
}
