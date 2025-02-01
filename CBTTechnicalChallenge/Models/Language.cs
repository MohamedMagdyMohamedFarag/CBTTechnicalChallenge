using System.ComponentModel.DataAnnotations;

namespace CBTTechnicalChallenge.Models
{
    public class Language
    {
        [Key]
        [Required]
        [MaxLength(2)]
        public string LanguageCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string LanguageName { get; set; }
        public bool IsDefault { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        // Navigation property
        public ICollection<User> Users { get; set; }
    }
}
