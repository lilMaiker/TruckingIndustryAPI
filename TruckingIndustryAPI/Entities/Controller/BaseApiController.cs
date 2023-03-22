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

        protected IActionResult HandleResult(ICommandResult result)
        {
            if (!result.Success && result.Error.Contains("Not Found"))
            {
                return NotFound(result.Data);
            }

            if (!result.Success)
            {
                _logger.LogError(result.Error);
                return BadRequest(new Command.BadRequestResult { Error = result.Error });
            }

            return Ok(result);
        }
    }
}
