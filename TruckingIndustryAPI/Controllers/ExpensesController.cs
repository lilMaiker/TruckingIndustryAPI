using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Controller;
using TruckingIndustryAPI.Extensions.Attributes;
using TruckingIndustryAPI.Features.EmployeeFeatures.Commands;
using TruckingIndustryAPI.Features.ExpensesFeatures.Commands;

using TruckingIndustryAPI.Features.ExpensesFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpensesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ExpensesController(IMediator mediator, ILogger<ExpensesController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllExpensesQuery()));
        }

        [HttpGet("GetByIdBid/{idBid}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdBid(long idBid)
        {
            return Ok(await _mediator.Send(new GetExpensesByIdBidQuery { Id = idBid }));
        }

        [HttpGet("GetById/{id}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetExpensesByIdQuery { Id = id }));
        }

        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateExpensesCommand createExpensesCommand)
        {
            return HandleResult(await _mediator.Send(createExpensesCommand));
        }

        [HttpPut]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateExpensesCommand updateExpensesCommand)
        {
            return HandleResult(await _mediator.Send(updateExpensesCommand));
        }

        [HttpDelete]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] DeleteExpensesCommand deleteExpensesCommand)
        {
            return HandleResult(await _mediator.Send(deleteExpensesCommand));
        }
    }
}
