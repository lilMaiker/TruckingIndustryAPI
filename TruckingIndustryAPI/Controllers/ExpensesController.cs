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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetExpensesByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExpensesCommand createExpensesCommand)
        {
            return Ok(await _mediator.Send(createExpensesCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateExpensesCommand updateExpensesCommand)
        {
            return Ok(await _mediator.Send(updateExpensesCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteExpensesCommand deleteExpensesCommand)
        {
            return Ok(await _mediator.Send(deleteExpensesCommand));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllExpensesQuery()));
        }
    }
}
