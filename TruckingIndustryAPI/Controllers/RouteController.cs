using MediatR;

using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.Routes.Commands;

using TruckingIndustryAPI.Features.Routes.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RouteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetRouteByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRouteCommand createRouteCommand)
        {
            return Ok(await _mediator.Send(createRouteCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRouteCommand updateRouteCommand)
        {
            return Ok(await _mediator.Send(updateRouteCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteRouteCommand deleteRouteCommand)
        {
            return Ok(await _mediator.Send(deleteRouteCommand));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllRouteQuery()));
        }
    }
}
