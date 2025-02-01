
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CBTTechnicalChallenge.Models
{
    public class User
    {
        [Key]
        public string ICNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Password must be 6 digits.")]
        public string Password { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        [Required]
        [MaxLength(2)]
        [ForeignKey("Language")]
        public string LanguagePreference { get; set; }
        [Required]
        public bool PrivacyPolicyAccepted { get; set; }
        // Navigation property
        public Language Language { get; set; }
        public ICollection<OtpVerification> OtpVerifications { get; set; }
    }
}
