using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.WebUtilities;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.DTO.Request;
using TruckingIndustryAPI.Entities.DTO.Response;
using TruckingIndustryAPI.Entities.Models.Identity;
using TruckingIndustryAPI.Helpers;
using TruckingIndustryAPI.Services.Email;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtHandlerService _jwtHandler;
        private readonly IEmailSenderService _emailSender;

        public AccountsController(IMapper mapper, JwtHandlerService jwtHandler, 
            IUnitOfWork unitOfWork, IEmailSenderService emailSender)
        {
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _unitOfWork = unitOfWork;
            //_logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Registers a new ApplicationUser
        /// </summary>
        /// <param name="userForRegistration">The user information for registration</param>
        /// <returns>Task of IActionResult</returns>
        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            try
            {
                // Check if the userForRegistration DTO is null or if the model state is invalid.
                // If either of these conditions are met, return a bad request.
                if (userForRegistration == null)
                {
                     //_logger.LogError("UserForRegistrationDto is null.");
                    return BadRequest("UserForRegistrationDto is null.");
                }

                if (!ModelState.IsValid)
                {
                    //_logger.LogError("Invalid model state.");
                    return BadRequest("Invalid model state.");
                }

                // Map the userForRegistration DTO to a ApplicationUser object using the mapper.
                var user = _mapper.Map<ApplicationUser>(userForRegistration);

                // Use the user manager to create the user and store the result.
                var result = await _unitOfWork.UserManager.CreateAsync(user, userForRegistration.Password);

                // Check if the user creation was not successful. If it wasn't, extract the error messages and return a bad request with the error messages.
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    //_logger.LogError($"ApplicationUser creation failed with errors: {string.Join(", ", errors)}");
                    return BadRequest(new RegistrationResponseDto { Errors = errors });
                }

                // Generate an email confirmation token for the user
                var token = await _unitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(user);

                // Create a dictionary containing the token and email
                var parameters = new Dictionary<string, string>
                {
                    {"email", user.Email },
                    {"token", token }
                };

                var url = Url.Action("EmailConfirmation", "Accounts", null, Request.Scheme, Request.Host.ToString());

                // Add the parameters to the client URI
                string callback = QueryHelpers.AddQueryString(uri: url, queryString: parameters);

                // Create a message object with the email, subject, and message body
                var message = new Message(new string[] { user.Email }, "Email Confirmation token", $"Доброго времени суток, Вот ваша ссылка для подтверждения: {callback}", null);

                // Send the email
                await _emailSender.SendEmailAsync(message);

                // Add the user to the "Viewer" role
                await _unitOfWork.UserManager.AddToRoleAsync(user, "Viewer");

                // Return a 201 status code to indicate a successful creation
                //_logger.LogInformation($"ApplicationUser {user.UserName} successfully registered.");
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                 //_logger.LogError($"An error occurred while registering user: {ex}");
                return StatusCode(500, "An error occurred while registering user.");
            }
        }

        [HttpGet("EmailConfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Invalid Email Confirmation Request");

            var confirmResult = await _unitOfWork.UserManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                return BadRequest("Invalid Email Confirmation Request");

            return Ok("Электронная почта успешно подтверждена.");
        }

        /// <summary>
        /// Handles external authentication process.
        /// </summary>
        /// <param name="externalAuth">External authentication information.</param>
        /// <returns>Auth response containing token and success status.</returns>
        [HttpPost("ExternalLogin")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthDto externalAuth)
        {
            // Verify authentication token from external source
            var payload = await _jwtHandler.VerifyGoogleToken(externalAuth);
            if (payload == null) return BadRequest("Invalid external authentication token.");

            // Create user login information
            var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);

            // Try to find user by login
            var user = await _unitOfWork.UserManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                // If user is not found by login, try to find by email
                user = await _unitOfWork.UserManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    // If user is not found by email, create a new user
                    user = new ApplicationUser { Email = payload.Email, UserName = payload.Email };
                    var createResult = await _unitOfWork.UserManager.CreateAsync(user);
                    if (!createResult.Succeeded) return BadRequest("Failed to create a new user.");

                    // Add the "Viewer" role to the user
                    await _unitOfWork.UserManager.AddToRoleAsync(user, "Viewer");

                    // Add external login information to the user
                    var addLoginResult = await _unitOfWork.UserManager.AddLoginAsync(user, info);
                    if (!addLoginResult.Succeeded) return BadRequest("Failed to add external login information to the user.");

                    // TODO: Prepare and send an email for email confirmation
                }
                else
                {
                    // If user is found by email, add external login information to the user
                    var addLoginResult = await _unitOfWork.UserManager.AddLoginAsync(user, info);
                    if (!addLoginResult.Succeeded) return BadRequest("Failed to add external login information to the user.");
                }
            }

            // Check if the user account is locked out
            if (_unitOfWork.UserManager.SupportsUserLockout && await _unitOfWork.UserManager.IsLockedOutAsync(user)) return BadRequest("User account is locked out.");

            // Generate a JWT token
            var token = await _jwtHandler.GenerateToken(user);

            var rolesUser = await _unitOfWork.UserManager.GetRolesAsync(user);

            //Send response for front-end
            return Ok(new AuthResponseDto { Token = token, IsAuthSuccessful = true, Role = rolesUser });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await _unitOfWork.UserManager.FindByNameAsync(userForAuthentication.Email);
            if (user == null)
                return BadRequest("Invalid Request");

            if (!await _unitOfWork.UserManager.IsEmailConfirmedAsync(user))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Email is not confirmed" });

            if (!await _unitOfWork.UserManager.CheckPasswordAsync(user, userForAuthentication.Password))
            {
                await _unitOfWork.UserManager.AccessFailedAsync(user);

                if (await _unitOfWork.UserManager.IsLockedOutAsync(user))
                {
                    var content = $@"Your account is locked out. To reset the password click this link.";
                    var message = new Message(new string[] { userForAuthentication.Email },
                        "Locked out account information", content, null);

                    await _emailSender.SendEmailAsync(message);

                    return Unauthorized(new AuthResponseDto { ErrorMessage = "The account is locked out" });
                }

                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            }

            var rolesUser = await _unitOfWork.UserManager.GetRolesAsync(user);

            /*if (await _unitOfWork.UserManager.GetTwoFactorEnabledAsync(user))
                return await GenerateOTPFor2StepVerification(user);*/

            var token = await _jwtHandler.GenerateToken(user);

            await _unitOfWork.UserManager.ResetAccessFailedCountAsync(user);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token, Role = rolesUser });
        }

        [HttpPost("TwoStepVerification")]
        public async Task<IActionResult> TwoStepVerification([FromBody] TwoFactorDto twoFactorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _unitOfWork.UserManager.FindByEmailAsync(twoFactorDto.Email);
            if (user is null)
                return BadRequest("Invalid Request");

            var validVerification = await _unitOfWork.UserManager.VerifyTwoFactorTokenAsync(user, twoFactorDto.Provider, twoFactorDto.Token);
            if (!validVerification)
                return BadRequest("Invalid Token Verification");

            var token = await _jwtHandler.GenerateToken(user);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _unitOfWork.UserManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
                return BadRequest("Invalid Request");

            var token = await _unitOfWork.UserManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                {"token", token },
                {"email", forgotPasswordDto.Email }
            };

            var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientURI, param);
            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);

            await _emailSender.SendEmailAsync(message);

            return Ok();
        }

        [HttpPost("DeleteAccount")]
        public async Task<IActionResult> DeleteUser(string UserName)
        {
            var user = await _unitOfWork.UserManager.FindByNameAsync(userName: UserName);
            if (user == null)
                return BadRequest("Invalid Request");

            var isDeleteUser = await _unitOfWork.UserManager.DeleteAsync(user);
            if (!isDeleteUser.Succeeded)
            {
                List<IdentityError> errorList = isDeleteUser.Errors.ToList();
                var errors = string.Join(", ", errorList.Select(e => e.Description));
                return BadRequest("Invalid Request");
            }

            return Ok(new { Status = "Success", Message = "Удаление пользователя прошло успешно!" });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _unitOfWork.UserManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return BadRequest("Invalid Request");

            var resetPassResult = await _unitOfWork.UserManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);

                return BadRequest(new { Errors = errors });
            }

            await _unitOfWork.UserManager.SetLockoutEndDateAsync(user, new DateTime(2000, 1, 1));

            return Ok();
        }
    }
}
