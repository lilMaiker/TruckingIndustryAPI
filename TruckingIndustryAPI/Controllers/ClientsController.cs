using MediatR;

using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.ClientFeatures.Commands;
using TruckingIndustryAPI.Features.ClientFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetClientByIdQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllClientQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientCommand createClientCommand)
        {
            return Ok(await _mediator.Send(createClientCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateClientCommand updateClientCommand)
        {
            return Ok(await _mediator.Send(updateClientCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteClientCommand deleteClientCommand)
        {
            return Ok(await _mediator.Send(deleteClientCommand));
        }
    }
}
