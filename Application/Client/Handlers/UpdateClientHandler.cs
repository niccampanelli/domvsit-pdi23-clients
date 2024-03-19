using Application.Client.Boundaries.UpdateClient;
using Application.Client.Commands;
using Application.UseCase.Client;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Dto.Client;
using MediatR;

namespace Application.Client.Handlers
{
    public class UpdateClientHandler : IRequestHandler<UpdateClientCommand, UpdateClientOutput>
    {
        private IMediatorHandler _mediatorHandler;
        private IClientUseCase _clientUseCase;

        public UpdateClientHandler(IMediatorHandler mediatorHandler, IClientUseCase clientUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _clientUseCase = clientUseCase;
        }

        public async Task<UpdateClientOutput> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;

                var updateInput = new UpdateClientInputDto()
                {
                    Name = input.Name,
                    Email = input.Email,
                    Phone = input.Phone
                };

                var result = await _clientUseCase.UpdateClient(input.Id, updateInput);

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
