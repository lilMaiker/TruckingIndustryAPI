using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.Routes.Commands;

using TruckingIndustryAPI.Features.Routes.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RouteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RouteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить маршрут по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор маршрута</param>
        /// <returns>Маршрут</returns>
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(new GetRouteByIdQuery { Id = id }));
        }

        /// <summary>
        /// Получить маршруты по идентификатору заявки
        /// </summary>
        /// <param name="id">Идентификатор заявки</param>
        /// <returns>Маршрут</returns>
        [HttpGet("GetByIdBid/{idBid}")]
        public async Task<IActionResult> GetByIdBid(long idBid)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(new GetRoutesByIdBidQuery { Id = idBid }));
        }

        /// <summary>
        /// Создать маршрут
        /// </summary>
        /// <param name="createRouteCommand">Модель для создания маршрута</param>
        /// <returns>Маршрут</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRouteCommand createRouteCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(createRouteCommand));
        }

        /// <summary>
        /// Обновить маршрут
        /// </summary>
        /// <param name="updateRouteCommand">Модель для обновления маршрута</param>
        /// <returns>Маршрут</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRouteCommand updateRouteCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(updateRouteCommand));
        }

        /// <summary>
        /// Обновить маршрут
        /// </summary>
        /// <param name="deleteRouteCommand">Модель для удаления маршрута</param>
        /// <returns>Маршрут</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteRouteCommand deleteRouteCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(deleteRouteCommand));
        }

        /// <summary>
        /// Получить все маршруты
        /// </summary>
        /// <returns>Маршрут</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllRouteQuery()));
        }
    }
}
