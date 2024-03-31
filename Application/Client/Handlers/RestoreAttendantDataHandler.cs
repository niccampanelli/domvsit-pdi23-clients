using Application.Client.Boundaries.DeleteClient;
using Application.Client.Boundaries.RestoreAttendantData;
using Application.Client.Commands;
using Application.UseCase.Attendant;
using Application.UseCase.Client;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;
using System.Text.RegularExpressions;

namespace Application.Client.Handlers
{
    public class RestoreAttendantDataHandler : IRequestHandler<RestoreAttendantDataCommand, RestoreAttendantDataOutput>
    {
        private IMediatorHandler _mediatorHandler;
        private IAttendantUseCase _attendantUseCase;

        public RestoreAttendantDataHandler(IMediatorHandler mediatorHandler, IAttendantUseCase attendantUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _attendantUseCase = attendantUseCase;
        }

        public async Task<RestoreAttendantDataOutput> Handle(RestoreAttendantDataCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;
                input.Authorization = Regex.Replace(input.Authorization, @"\bBearer\b", string.Empty).Trim();

                var extractResult = await _attendantUseCase.ExtractIdFromToken(input.Authorization);

                if (extractResult.AttendantId.HasValue == false)
                {
                    var message = "Participante não encontrado";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var attendant = await _attendantUseCase.GetAttendantById(extractResult.AttendantId.Value);

                if (attendant == null)
                {
                    var message = "Participante não encontrado";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var output = new RestoreAttendantDataOutput()
                {
                    Id = attendant.Id ?? 0L,
                    Name = attendant.Name,
                    Email = attendant.Email,
                    Role = attendant.Role,
                    ClientId = attendant.ClientId
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
