using Application.Client.Boundaries.Create;
using Application.Client.Boundaries.ListClient;
using Application.Client.Commands;
using Application.Commom.Boundaries;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [SwaggerTag("Rotas para gerenciar clientes da plataforma")]
    public class ClientController : BaseController
    {
        private IMediatorHandler _mediatorHandler;

        public ClientController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("[action]")]
        [SwaggerOperation(Summary = "Cadastrar cliente", Description = "Cadastra um novo cliente.")]
        [SwaggerResponse(201, Description = "Sucesso", Type = typeof(CreateOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> Create([FromBody] CreateControllerInput input)
        {
            var userIdHeader = Request.Headers["User-Id"].FirstOrDefault();

            if (userIdHeader == null)
            {
                return BadRequest(new string[]
                {
                    "O id do consultor precisa ser informado"
                });
            }

            var commandInput = new CreateInput()
            {
                Name = input.Name,
                Email = input.Email,
                Phone = input.Phone,
                ConsultorId = long.Parse(userIdHeader)
            };

            var command = new CreateCommand(commandInput);
            var result = await _mediatorHandler.SendCommand<CreateCommand, CreateOutput>(command);

            if (IsValidOperation())
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpPost("[action]")]
        [SwaggerOperation(Summary = "Listar clientes", Description = "Lista os clientes.")]
        [SwaggerResponse(201, Description = "Sucesso", Type = typeof(PaginatedResponse<ListClientOutput>))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> List([FromBody] ListClientInput input)
        {
            var command = new ListClientCommand(input);
            var result = await _mediatorHandler.SendCommand<ListClientCommand, PaginatedResponse<ListClientOutput>>(command);

            if (IsValidOperation())
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }
    }
}
