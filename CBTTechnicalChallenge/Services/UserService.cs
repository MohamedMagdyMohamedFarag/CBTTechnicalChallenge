using CBTTechnicalChallenge.Data;
using CBTTechnicalChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace CBTTechnicalChallenge.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User?> GetUserByICNumberAsync(string icNumber)
        {
            try
            {
                return await _context.Users.FindAsync(icNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the user with ICNumber {ICNumber}.", icNumber);
                throw new ApplicationException("An error occurred while retrieving the user.", ex);
            }
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the user with ICNumber {ICNumber}.", user.ICNumber);
                throw new ApplicationException("An error occurred while adding the user.", ex);
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user with ICNumber {ICNumber}.", user.ICNumber);
                throw new ApplicationException("An error occurred while updating the user.", ex);
            }
        }

        public async Task<bool> UserExistsAsync(string icNumber)
        {
            try
            {
                return await _context.Users.AnyAsync(u => u.ICNumber == icNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking if the user with ICNumber {ICNumber} exists.", icNumber);
                throw new ApplicationException("An error occurred while checking if the user exists.", ex);
            }
        }

        public async Task<string> CreateAccountAsync(string name, string icNumber, string phoneNumber, string email, string languageCode)
        {
            try
            {
                if (await UserExistsAsync(icNumber))
                {
                    return "ICNumber already registered. Please try to login.";
                }

                var user = new User
                {
                    Name = name,
                    ICNumber = icNumber,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    RegistrationDate = DateTime.UtcNow,
                    LanguagePreference = languageCode,
                    PrivacyPolicyAccepted = false, // Default value
                    Password = string.Empty // Default value
                };

                await AddUserAsync(user);
                return "User account created successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the account for ICNumber {ICNumber}.", icNumber);
                return $"An error occurred while creating the account: {ex.Message}";
            }
        }

        public async Task<string> LoginAsync(string icNumber)
        {
            try
            {
                var user = await GetUserByICNumberAsync(icNumber);
                if (user != null)
                {
                    return "Login successful.";
                }
                return "Invalid login attempt.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in with ICNumber {ICNumber}.", icNumber);
                return $"An error occurred while logging in: {ex.Message}";
            }
        }

        public async Task<string> UpdatePrivacyPolicyAgreementAsync(string icNumber, bool privacyPolicyAccepted)
        {
            try
            {
                var user = await GetUserByICNumberAsync(icNumber);
                if (user != null)
                {
                    user.PrivacyPolicyAccepted = privacyPolicyAccepted;
                    await UpdateUserAsync(user);
                    return "Privacy policy agreement updated successfully.";
                }
                return "User not found.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the privacy policy agreement for ICNumber {ICNumber}.", icNumber);
                return $"An error occurred while updating the privacy policy agreement: {ex.Message}";
            }
        }

        public async Task<string> CreatePinAsync(string icNumber, string pin)
        {
            try
            {
                var user = await GetUserByICNumberAsync(icNumber);
                if (user != null)
                {
                    user.Password = pin;
                    await UpdateUserAsync(user);
                    return "PIN created successfully.";
                }
                return "User not found.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the PIN for ICNumber {ICNumber}.", icNumber);
                return $"An error occurred while creating the PIN: {ex.Message}";
            }
        }
    }
}
