using CBTTechnicalChallenge.Models;

namespace CBTTechnicalChallenge.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByICNumberAsync(string icNumber);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<bool> UserExistsAsync(string icNumber);
        Task<string> CreateAccountAsync(string name, string icNumber, string phoneNumber, string email, string languageCode);
        Task<string> LoginAsync(string icNumber);
        Task<string> UpdatePrivacyPolicyAgreementAsync(string icNumber, bool privacyPolicyAccepted);
        Task<string> CreatePinAsync(string icNumber, string pin);
    }
}
