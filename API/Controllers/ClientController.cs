using Application.Client.Boundaries.Create;
using Application.Client.Boundaries.DeleteClient;
using Application.Client.Boundaries.GetAttendantToken;
using Application.Client.Boundaries.GetClientByid;
using Application.Client.Boundaries.ListClient;
using Application.Client.Boundaries.UpdateClient;
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

        [HttpGet("[action]")]
        [SwaggerOperation(Summary = "Listar clientes", Description = "Lista os clientes.")]
        [SwaggerResponse(201, Description = "Sucesso", Type = typeof(PaginatedResponse<ListClientOutput>))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> List([FromQuery] ListClientInput input)
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

        [HttpPut("[action]/{id}")]
        [SwaggerOperation(Summary = "Atualizar cliente", Description = "Atualiza as informações de um cliente.")]
        [SwaggerResponse(201, Description = "Sucesso", Type = typeof(UpdateClientOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateClientControllerInput input)
        {
            var commandInput = new UpdateClientInput()
            {
                Id = id,
                Name = input.Name,
                Email = input.Email,
                Phone = input.Phone
            };

            var command = new UpdateClientCommand(commandInput);
            var result = await _mediatorHandler.SendCommand<UpdateClientCommand, UpdateClientOutput>(command);

            if (IsValidOperation())
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter cliente", Description = "Obtém o cliente pelo id")]
        [SwaggerResponse(201, Description = "Sucesso", Type = typeof(GetClientByIdOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> GetById(long id)
        {
            var input = new GetClientByIdInput()
            {
                Id = id
            };

            var command = new GetClientByIdCommand(input);
            var result = await _mediatorHandler.SendCommand<GetClientByIdCommand, GetClientByIdOutput>(command);

            if (IsValidOperation())
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpGet("{id}/[action]")]
        [SwaggerOperation(Summary = "Obter token de participante", Description = "Obtém token de participantes do cliente com o id fornecido")]
        [SwaggerResponse(201, Description = "Sucesso", Type = typeof(GetAttendantTokenOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> GetAttendantToken(long id)
        {
            var input = new GetAttendantTokenInput()
            {
                Id = id
            };

            var command = new GetAttendantTokenCommand(input);
            var result = await _mediatorHandler.SendCommand<GetAttendantTokenCommand, GetAttendantTokenOutput>(command);

            if (IsValidOperation())
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpDelete("[action]/{id}")]
        [SwaggerOperation(Summary = "Deletar cliente", Description = "Deleta o cliente com o id fornecido")]
        [SwaggerResponse(201, Description = "Sucesso", Type = typeof(DeleteClientOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> Delete(long id)
        {
            var input = new DeleteClientInput()
            {
                Id = id
            };

            var command = new DeleteClientCommand(input);
            var result = await _mediatorHandler.SendCommand<DeleteClientCommand, DeleteClientOutput>(command);

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
