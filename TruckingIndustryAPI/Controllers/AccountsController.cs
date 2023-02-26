using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly JwtHandler _jwtHandler;
        private readonly ILogger<AccountsController> _logger;
        private readonly IEmailSender _emailSender;

        public AccountsController(IMapper mapper, JwtHandler jwtHandler, 
            IUnitOfWork unitOfWork, ILogger<AccountsController> logger, IEmailSender emailSender)
        {
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
                     _logger.LogError("UserForRegistrationDto is null.");
                    return BadRequest("UserForRegistrationDto is null.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid model state.");
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
                    _logger.LogError($"ApplicationUser creation failed with errors: {string.Join(", ", errors)}");
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

                // Add the parameters to the client URI
                string callback = QueryHelpers.AddQueryString(uri: userForRegistration.ClientURI, parameters);

                // Create a message object with the email, subject, and message body
                var message = new Message(new string[] { user.Email }, "Email Confirmation token", callback, null);

                // Send the email
                await _emailSender.SendEmailAsync(message);

                // Add the user to the "Viewer" role
                await _unitOfWork.UserManager.AddToRoleAsync(user, "Viewer");

                // Return a 201 status code to indicate a successful creation
                _logger.LogInformation($"ApplicationUser {user.UserName} successfully registered.");
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                 _logger.LogError($"An error occurred while registering user: {ex}");
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
    }
}
