using CBTTechnicalChallenge.Enums;
using CBTTechnicalChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace CBTTechnicalChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OtpController : ControllerBase
    {
        private readonly IOtpService _otpService;

        public OtpController(IOtpService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("send-otp")]
        public async Task<ActionResult> SendOtp(string icNumber)
        {
            if (ModelState.IsValid)
            {
                var result = await _otpService.SendOtpAsync(icNumber);
                if (result == "OTP sent successfully.")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("verify-otp")]
        public async Task<ActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _otpService.VerifyOtpAsync(request.Contact, request.Otp, request.ContactType);
                if (result == "OTP verified successfully.")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }
    }

    public class VerifyOtpRequest
    {
        public string Contact { get; set; }
        public string Otp { get; set; }
        public ContactType ContactType { get; set; }
    }
}
