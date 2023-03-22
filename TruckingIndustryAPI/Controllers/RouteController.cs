using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Controller;
using TruckingIndustryAPI.Extensions.Attributes;
using TruckingIndustryAPI.Features.PositionFeatures.Commands;
using TruckingIndustryAPI.Features.Routes.Commands;

using TruckingIndustryAPI.Features.Routes.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RouteController : BaseApiController
    {
        private readonly IMediator _mediator;

        public RouteController(IMediator mediator, ILogger<RouteController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить все маршруты
        /// </summary>
        /// <returns>Маршрут</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllRouteQuery()));
        }

        /// <summary>
        /// Получить маршрут по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор маршрута</param>
        /// <returns>Маршрут</returns>
        [HttpGet("GetById/{id}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetRouteByIdQuery { Id = id }));
        }

        /// <summary>
        /// Получить маршруты по идентификатору заявки
        /// </summary>
        /// <param name="id">Идентификатор заявки</param>
        /// <returns>Маршрут</returns>
        [HttpGet("GetByIdBid/{idBid}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdBid(long idBid)
        {
            return Ok(await _mediator.Send(new GetRoutesByIdBidQuery { Id = idBid }));
        }

        /// <summary>
        /// Создать маршрут
        /// </summary>
        /// <param name="createRouteCommand">Модель для создания маршрута</param>
        /// <returns>Маршрут</returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateRouteCommand createRouteCommand)
        {
            return HandleResult(await _mediator.Send(createRouteCommand));
        }

        /// <summary>
        /// Обновить маршрут
        /// </summary>
        /// <param name="updateRouteCommand">Модель для обновления маршрута</param>
        /// <returns>Маршрут</returns>
        [HttpPut]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateRouteCommand updateRouteCommand)
        {
            return HandleResult(await _mediator.Send(updateRouteCommand));
        }

        /// <summary>
        /// Обновить маршрут
        /// </summary>
        /// <param name="deleteRouteCommand">Модель для удаления маршрута</param>
        /// <returns>Маршрут</returns>
        [HttpDelete]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] DeleteRouteCommand deleteRouteCommand)
        {
            return HandleResult(await _mediator.Send(deleteRouteCommand));
        }
    }
}
