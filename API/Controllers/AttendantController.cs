using Application.Client.Boundaries.Authenticate;
using Application.Client.Boundaries.GetAttendantById;
using Application.Client.Boundaries.JoinAsAttendant;
using Application.Client.Boundaries.ListAttendant;
using Application.Client.Boundaries.RestoreAttendantData;
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
    [SwaggerTag("Rotas para gerenciar participantes dos clientes")]
    public class AttendantController : BaseController
    {
        private IMediatorHandler _mediatorHandler;

        public AttendantController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Autenticar participante", Description = "Autentica o participante e devolve o token para acessar a plataforma.")]
        [SwaggerResponse(201, Description = "Sucesso", Type = typeof(AuthenticateOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateInput input)
        {
            var command = new AuthenticateCommand(input);
            var result = await _mediatorHandler.SendCommand<AuthenticateCommand, AuthenticateOutput>(command);

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
        [SwaggerOperation(Summary = "Restaurar dados do participante", Description = "Obtém os dados do participante logado com base no token")]
        [SwaggerResponse(200, Description = "Sucesso", Type = typeof(RestoreAttendantDataOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> RestoreAttendantData([FromHeader(Name = "Authorization")] string authorization)
        {
            var input = new RestoreAttendantDataInput()
            {
                Authorization = authorization
            };

            var command = new RestoreAttendantDataCommand(input);
            var result = await _mediatorHandler.SendCommand<RestoreAttendantDataCommand, RestoreAttendantDataOutput>(command);

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
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Ingressar como participante", Description = "Cadastra um novo participante em um cliente.")]
        [SwaggerResponse(201, Description = "Sucesso", Type = typeof(JoinAsAttendantOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> JoinAsAttendant([FromBody] JoinAsAttendantInput input)
        {
            var command = new JoinAsAttendantCommand(input);
            var result = await _mediatorHandler.SendCommand<JoinAsAttendantCommand, JoinAsAttendantOutput>(command);

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
        [SwaggerOperation(Summary = "Listar participantes", Description = "Lista os partipantes.")]
        [SwaggerResponse(201, Description = "Sucesso", Type = typeof(PaginatedResponse<ListAttendantOutput>))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> List([FromQuery] ListAttendantInput input)
        {
            var command = new ListAttendantCommand(input);
            var result = await _mediatorHandler.SendCommand<ListAttendantCommand, PaginatedResponse<ListAttendantOutput>>(command);

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
        [SwaggerOperation(Summary = "Obter participante", Description = "Obtém um participante pelo id")]
        [SwaggerResponse(201, Description = "Sucesso", Type = typeof(GetAttendantByIdOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> GetById(long id)
        {
            var input = new GetAttendantByIdInput()
            {
                Id = id
            };

            var command = new GetAttendantByIdCommand(input);
            var result = await _mediatorHandler.SendCommand<GetAttendantByIdCommand, GetAttendantByIdOutput>(command);

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
