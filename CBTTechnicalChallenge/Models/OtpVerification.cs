using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBTTechnicalChallenge.Models
{
    public class OtpVerification
    {
        [Key]
        public int OtpID { get; set; }
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "OTPCode must be 4 digits.")]
        public string OTPCode { get; set; }
        [Required]
        public DateTime GeneratedAt { get; set; }
        [Required]
        public DateTime ExpiresAt { get; set; }
        // Foreign keys
        public string UserICNumber { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserEmail { get; set; }
        // Navigation property
        public User User { get; set; }
    }
}
