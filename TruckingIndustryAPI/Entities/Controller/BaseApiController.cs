using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Command;

namespace TruckingIndustryAPI.Entities.Controller
{
    public class BaseApiController : ControllerBase
    {
        private readonly ILogger<BaseApiController> _logger;

        public BaseApiController(ILogger<BaseApiController> logger)
        {
            _logger = logger;
        }

        //This code is a method that handles the result of a command. It checks if the result was successful or not. If the result was not successful, it checks if the error
        // message contains the phrase "Not Found". If it does, it logs the error and returns a NotFound result. Otherwise, it logs the error and returns a BadRequest result
        //. The code does not check for the case when result.Error is null.
        /// <summary>
        /// Handles the result of a command.
        /// </summary>
        /// <param name="result">The result of the command.</param>
        /// <returns>
        /// An IActionResult based on the result of the command.
        /// </returns>
        protected IActionResult HandleResult(ICommandResult result)
        {
            if (!result.Success)
            {
                if (result.Error.Contains("Not Found"))
                {
                    _logger.LogInformation($"{result.Error}. {result.Data}");
                    return NotFound(result);
                }
                else
                {
                    _logger.LogError(message: result.Error);
                    return BadRequest(new Command.BadRequestResult { Error = result.Error });
                }
            }

            return Ok(result);
        }

        //Bug: The code does not check for the case when result.Error is null.
    }
}
