using MediatR;

using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.EmployeeFeatures.Commands;
using TruckingIndustryAPI.Features.EmployeeFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllEmployeeQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetEmployeeByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeCommand createEmployeeCommand)
        {
            return Ok(await _mediator.Send(createEmployeeCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateEmployeeCommand updateEmployeeCommand)
        {
            return Ok(await _mediator.Send(updateEmployeeCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteEmployeeCommand deleteEmployeeCommand)
        {
            return Ok(await _mediator.Send(deleteEmployeeCommand));
        }


    }
}
