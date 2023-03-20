using MediatR;

using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.ExpensesFeatures.Commands;

using TruckingIndustryAPI.Features.ExpensesFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpensesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetByIdBid/{idBid}")]
        public async Task<IActionResult> GetByIdBid(long idBid)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(new GetExpensesByIdBidQuery { Id = idBid }));
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(new GetExpensesByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExpensesCommand createExpensesCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(createExpensesCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateExpensesCommand updateExpensesCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(updateExpensesCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteExpensesCommand deleteExpensesCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(deleteExpensesCommand));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllExpensesQuery()));
        }
    }
}
