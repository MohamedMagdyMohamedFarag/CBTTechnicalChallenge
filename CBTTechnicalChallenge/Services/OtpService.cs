using CBTTechnicalChallenge.Data;
using CBTTechnicalChallenge.Enums;
using CBTTechnicalChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace CBTTechnicalChallenge.Services
{
    public class OtpService : IOtpService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ILogger<OtpService> _logger;

        public OtpService(ApplicationDbContext context, IUserService userService, ILogger<OtpService> logger)
        {
            _context = context;
            _userService = userService;
            _logger = logger;
        }

        public async Task<string> SendOtpAsync(string icNumber)
        {
            try
            {
                _logger.LogInformation("Generating OTP for ICNumber: {ICNumber}", icNumber);

                var user = await _userService.GetUserByICNumberAsync(icNumber);
                if (user == null)
                {
                    _logger.LogWarning("User not found for ICNumber: {ICNumber}", icNumber);
                    return "User not found.";
                }

                var otp = new OtpVerification
                {
                    UserICNumber = icNumber,
                    OTPCode = GenerateOtp(),
                    GeneratedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                    UserPhoneNumber = user.PhoneNumber,
                    UserEmail = user.Email
                };

                // Save OTP to database
                await SaveOtpAsync(otp);

                // Logic to send OTP to phone or email (implementation not shown)
                // SendOtpToContact(otp);

                _logger.LogInformation("OTP sent successfully for ICNumber: {ICNumber}", icNumber);
                return "OTP sent successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending OTP for ICNumber: {ICNumber}", icNumber);
                return "Error sending OTP.";
            }
        }

        public async Task<string> VerifyOtpAsync(string contact, string otp, ContactType contactType)
        {
            try
            {
                _logger.LogInformation("Verifying OTP for Contact: {Contact}, ContactType: {ContactType}", contact, contactType);

                OtpVerification? otpVerification = null;
                if (contactType == ContactType.Phone)
                {
                    otpVerification = await _context.OtpVerifications
                        .FirstOrDefaultAsync(o => o.UserPhoneNumber == contact && o.OTPCode == otp && o.ExpiresAt > DateTime.UtcNow);
                }
                else if (contactType == ContactType.Email)
                {
                    otpVerification = await _context.OtpVerifications
                        .FirstOrDefaultAsync(o => o.UserEmail == contact && o.OTPCode == otp && o.ExpiresAt > DateTime.UtcNow);
                }

                if (otpVerification != null)
                {
                    _logger.LogInformation("OTP verified successfully for Contact: {Contact}, ContactType: {ContactType}", contact, contactType);
                    return "OTP verified successfully.";
                }

                _logger.LogWarning("Invalid or expired OTP for Contact: {Contact}, ContactType: {ContactType}", contact, contactType);
                return "Invalid or expired OTP.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying OTP for Contact: {Contact}, ContactType: {ContactType}", contact, contactType);
                return "Error verifying OTP.";
            }
        }

        private string GenerateOtp()
        {
            // Generate a 4-digit OTP
            var random = new Random();
            return random.Next(1000, 9999).ToString();
        }

        private async Task SaveOtpAsync(OtpVerification otp)
        {
            _context.OtpVerifications.Add(otp);
            await _context.SaveChangesAsync();
        }
    }
}
