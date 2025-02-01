using CBTTechnicalChallenge.Models;
using CBTTechnicalChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace CBTTechnicalChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create-account")]
        public async Task<ActionResult> CreateAccount(string name, string icNumber, string phoneNumber, string email, string languageCode = "EN")
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateAccountAsync(name, icNumber, phoneNumber, email, languageCode);
                if (result == "User account created successfully.")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(string icNumber)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(icNumber);
                if (result == "Login successful.")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("privacy-policy-agreement")]
        public async Task<ActionResult> PrivacyPolicyAgreement(string icNumber, bool privacyPolicyAccepted)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdatePrivacyPolicyAgreementAsync(icNumber, privacyPolicyAccepted);
                if (result == "Privacy policy agreement updated successfully.")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("create-pin")]
        public async Task<ActionResult> CreatePin(string icNumber, string pin)
        {
            if (ModelState.IsValid)
            {
                if (pin.Length != 6 || !pin.All(char.IsDigit))
                {
                    return BadRequest("PIN must be exactly 6 digits.");
                }

                var result = await _userService.CreatePinAsync(icNumber, pin);
                if (result == "PIN created successfully.")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }
    }
}
