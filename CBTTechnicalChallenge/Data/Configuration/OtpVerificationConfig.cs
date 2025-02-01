using System.Reflection.Emit;
using CBTTechnicalChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBTTechnicalChallenge.Data.Configuration
{
    public class OtpVerificationConfig : IEntityTypeConfiguration<OtpVerification>
    {
        public void Configure(EntityTypeBuilder<OtpVerification> builder)
        {
            builder.HasKey(e => e.OtpID);
            builder.Property(e => e.OTPCode).IsRequired().HasMaxLength(4); // OTPCode is 4 digits
            builder.Property(e => e.GeneratedAt).IsRequired().HasColumnType("datetime");
            builder.Property(e => e.ExpiresAt).IsRequired().HasColumnType("datetime");

            // Defining the composite foreign key relationship with User
            builder.HasOne(o => o.User)
                .WithMany(u => u.OtpVerifications)
                .HasForeignKey(o => new { o.UserICNumber, o.UserPhoneNumber, o.UserEmail })
                .HasPrincipalKey(u => new { u.ICNumber, u.PhoneNumber, u.Email })
                .OnDelete(DeleteBehavior.Cascade); // Enabling cascading delete for OtpVerifications
        }
    }
}
