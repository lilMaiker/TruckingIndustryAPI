using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Controller;
using TruckingIndustryAPI.Extensions.Attributes;
using TruckingIndustryAPI.Features.CargoFeatures.Commands;

using TruckingIndustryAPI.Features.CargoFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CargoController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CargoController(IMediator mediator, ILogger<CargoController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Получить груз по идентификатору груза
        /// </summary>
        /// <param name="id">Идентификатор груза</param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetCargoByIdQuery { Id = id }));
        }

        /// <summary>
        /// Получить груз по идентификатору заявки
        /// </summary>
        /// <param name="idBid">Идентификатор заявки</param>
        /// <returns></returns>
        [HttpGet("GetByIdBid/{idBid}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdBid(long idBid)
        {
            return Ok(await _mediator.Send(new GetCargoByIdBidQuery { Id = idBid }));
        }

        /// <summary>
        /// Получить весь груз
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCargoQuery()));
        }

        /// <summary>
        /// Добавить новый груз в заявку
        /// </summary>
        /// <param name="createCargoCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCargoCommand createCargoCommand)
        {
            return HandleResult(await _mediator.Send(createCargoCommand));
        }

        /// <summary>
        /// Обновить груз по заявке
        /// </summary>
        /// <param name="updateCargoCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateCargoCommand updateCargoCommand)
        {
            return HandleResult(await _mediator.Send(updateCargoCommand));
        }

        /// <summary>
        /// Удалить груз из заявки
        /// </summary>
        /// <param name="deleteCargoCommand"></param>
        /// <returns></returns>
        [HttpDelete]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] DeleteCargoCommand deleteCargoCommand)
        {
            return HandleResult(await _mediator.Send(deleteCargoCommand));
        }
    }
}
