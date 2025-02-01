using CBTTechnicalChallenge.Enums;

namespace CBTTechnicalChallenge.Services
{
    public interface IOtpService
    {
        Task<string> SendOtpAsync(string icNumber);
        Task<string> VerifyOtpAsync(string contact, string otp, ContactType contactType);
    }
}
