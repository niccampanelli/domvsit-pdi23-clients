using Application.Client.Boundaries.GetAttendantToken;
using Application.Client.Commands;
using Application.UseCase.Client;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Option;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Client.Handlers
{
    public class GetAttendantTokenHandler : IRequestHandler<GetAttendantTokenCommand, GetAttendantTokenOutput>
    {
        private IMediatorHandler _mediatorHandler;
        private IClientUseCase _clientUseCase;
        private IOptions<Secrets> _secrets;

        public GetAttendantTokenHandler(IMediatorHandler mediatorHandler, IClientUseCase clientUseCase, IOptions<Secrets> secrets)
        {
            _mediatorHandler = mediatorHandler;
            _clientUseCase = clientUseCase;
            _secrets = secrets;
        }

        public async Task<GetAttendantTokenOutput> Handle(GetAttendantTokenCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;
                var clientId = input.Id;

                var attendantToken = await _clientUseCase.GetAttendantTokenByClientId(clientId);

                if (attendantToken == null)
                {
                    var message = "O token de participante não foi encontrado";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                DateTime? expiredAt = null;

                if (attendantToken.ExpiresAt <=  DateTime.UtcNow)
                {
                    expiredAt = attendantToken.ExpiresAt;


                    var attendantTokenValidity = _secrets.Value.AttendantTokenDefaultValidityInMinutes;
                    var generateAttendantTokenResult = await _clientUseCase.GenerateAttendantToken(attendantTokenValidity, clientId);

                    attendantToken = await _clientUseCase.RegisterAttendantTokenSession(generateAttendantTokenResult);
                }

                var output = new GetAttendantTokenOutput()
                {
                    Id = attendantToken.Id,
                    Value = attendantToken.Value,
                    CreatedAt = attendantToken.CreatedAt,
                    ExpiresAt = attendantToken.ExpiresAt,
                    ExpiredAt = expiredAt,
                };

                return output;
            }

            foreach (var error in command.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, error.ErrorMessage));
            }

            return default;
        }
    }
}
