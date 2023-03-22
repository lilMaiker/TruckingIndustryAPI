using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Controller;
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
        private readonly ILogger _logger;

        public ExpensesController(IMediator mediator, ILogger<ExpensesController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllExpensesQuery()));
        }

        [HttpGet("GetByIdBid/{idBid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdBid(long idBid)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(new GetExpensesByIdBidQuery { Id = idBid }));
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(new GetExpensesByIdQuery { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateExpensesCommand createExpensesCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return HandleResult(await _mediator.Send(createExpensesCommand));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateExpensesCommand updateExpensesCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return HandleResult(await _mediator.Send(updateExpensesCommand));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] DeleteExpensesCommand deleteExpensesCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return HandleResult(await _mediator.Send(deleteExpensesCommand));
        }
    }
}
