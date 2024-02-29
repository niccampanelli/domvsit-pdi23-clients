using Application.Client.Boundaries.GetAttendantById;
using Application.Client.Commands;
using Application.UseCase.Attendant;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;

namespace Application.Client.Handlers
{
    public class GetAttendantByIdHandler : IRequestHandler<GetAttendantByIdCommand, GetAttendantByIdOutput>
    {
        private IMediatorHandler _mediatorHandler;
        private IAttendantUseCase _attendantUseCase;

        public GetAttendantByIdHandler(IMediatorHandler mediatorHandler, IAttendantUseCase attendantUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _attendantUseCase = attendantUseCase;
        }

        public async Task<GetAttendantByIdOutput> Handle(GetAttendantByIdCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;
                var id = input.Id;

                var attendant = await _attendantUseCase.GetAttendantById(id);

                if (attendant == null)
                {
                    var message = "O participante não foi encontrado";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var output = new GetAttendantByIdOutput()
                {
                    Id = id,
                    Name = attendant.Name,
                    Email = attendant.Email,
                    Role = attendant.Role,
                    ClientId = attendant.ClientId,
                    CreatedAt = attendant.CreatedAt
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
