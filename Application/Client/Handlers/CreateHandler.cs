using Application.Client.Boundaries.Create;
using Application.Client.Commands;
using Application.UseCase.Client;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;

namespace Application.Client.Handlers
{
    public class CreateHandler : IRequestHandler<CreateCommand, CreateOutput>
    {
        private IMediatorHandler _mediatorHandler;
        private IClientUseCase _clientUseCase;

        public CreateHandler(IMediatorHandler mediatorHandler, IClientUseCase clientUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _clientUseCase = clientUseCase;
        }

        public async Task<CreateOutput> Handle(CreateCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {

            }

            foreach (var error in command.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, error.ErrorMessage));
            }

            return default;
        }
    }
}
