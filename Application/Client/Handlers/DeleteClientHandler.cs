using Application.Client.Boundaries.DeleteClient;
using Application.Client.Commands;
using Application.UseCase.Client;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;

namespace Application.Client.Handlers
{
    public class DeleteClientHandler : IRequestHandler<DeleteClientCommand, DeleteClientOutput>
    {
        private IMediatorHandler _mediatorHandler;
        private IClientUseCase _clientUseCase;

        public DeleteClientHandler(IMediatorHandler mediatorHandler, IClientUseCase clientUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _clientUseCase = clientUseCase;
        }

        public async Task<DeleteClientOutput> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;
                var id = input.Id;

                var deleted = await _clientUseCase.DeleteClient(id);

                if (deleted == false)
                {
                    var message = "O cliente não foi deletado";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                await _clientUseCase.DeleteClientEvents(id);

                return default;
            }

            foreach (var error in command.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, error.ErrorMessage));
            }

            return default;
        }
    }
}
