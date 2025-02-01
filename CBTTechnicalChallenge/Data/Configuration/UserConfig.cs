using System.Reflection.Emit;
using CBTTechnicalChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBTTechnicalChallenge.Data.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.ICNumber);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Email).IsRequired().HasMaxLength(255);
            builder.Property(e => e.PhoneNumber).IsRequired();
            builder.Property(e => e.Password).IsRequired().HasMaxLength(6);
            builder.Property(e => e.RegistrationDate).IsRequired();
            builder.Property(e => e.LanguagePreference).IsRequired().HasMaxLength(2);
            builder.Property(e => e.PrivacyPolicyAccepted).IsRequired();

            builder.HasIndex(e => e.ICNumber).IsUnique();
            builder.HasIndex(e => e.PhoneNumber).IsUnique();
            builder.HasIndex(e => e.Email).IsUnique();

            builder.HasOne(e => e.Language)
                   .WithMany(l => l.Users)
                   .HasForeignKey(e => e.LanguagePreference)
                   .HasPrincipalKey(l => l.LanguageCode)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.OtpVerifications)
                   .WithOne(o => o.User)
                   .HasForeignKey(o => new { o.UserICNumber, o.UserPhoneNumber, o.UserEmail })
                   .HasPrincipalKey(e => new { e.ICNumber, e.PhoneNumber, e.Email })
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
