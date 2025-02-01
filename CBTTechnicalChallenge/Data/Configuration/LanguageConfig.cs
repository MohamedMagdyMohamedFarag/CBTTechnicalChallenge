using CBTTechnicalChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CBTTechnicalChallenge.Data.Configuration
{
    public class LanguageConfig : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(e => e.LanguageCode);
            builder.Property(e => e.LanguageCode).IsRequired().HasMaxLength(2);
            builder.Property(e => e.LanguageName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.IsDefault).IsRequired();
            builder.Property(e => e.IsActive).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.UpdatedDate);

            builder.HasMany(e => e.Users)
                   .WithOne(u => u.Language)
                   .HasForeignKey(u => u.LanguagePreference)
                   .HasPrincipalKey(e => e.LanguageCode)
                   .OnDelete(DeleteBehavior.Restrict);

            // Seed initial data
            builder.HasData(
                new Language
                {
                    LanguageCode = "EN",
                    LanguageName = "English",
                    IsDefault = true,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                },
                new Language
                {
                    LanguageCode = "MY",
                    LanguageName = "Malay",
                    IsDefault = false,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                }
            );
        }
    }
}
