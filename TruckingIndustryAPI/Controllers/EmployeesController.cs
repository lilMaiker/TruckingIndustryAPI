using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Controller;
using TruckingIndustryAPI.Extensions.Attributes;
using TruckingIndustryAPI.Features.CurrencyFeatures.Commands;
using TruckingIndustryAPI.Features.EmployeeFeatures.Commands;
using TruckingIndustryAPI.Features.EmployeeFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public EmployeesController(IMediator mediator, ILogger<EmployeesController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllEmployeeQuery()));
        }

        [HttpGet("{id}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetEmployeeByIdQuery { Id = id }));
        }

        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateEmployeeCommand createEmployeeCommand)
        {
            return HandleResult(await _mediator.Send(createEmployeeCommand));
        }

        [HttpPut]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateEmployeeCommand updateEmployeeCommand)
        {
            return HandleResult(await _mediator.Send(updateEmployeeCommand));
        }

        [HttpDelete]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] DeleteEmployeeCommand deleteEmployeeCommand)
        {
            return HandleResult(await _mediator.Send(deleteEmployeeCommand));
        }


    }
}
