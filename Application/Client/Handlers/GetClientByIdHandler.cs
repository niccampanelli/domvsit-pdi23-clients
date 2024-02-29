using Application.Client.Boundaries.GetClientByid;
using Application.Client.Commands;
using Application.UseCase.Client;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;

namespace Application.Client.Handlers
{
    public class GetClientByIdHandler : IRequestHandler<GetClientByIdCommand, GetClientByIdOutput>
    {
        private IMediatorHandler _mediatorHandler;
        private IClientUseCase _clientUseCase;

        public GetClientByIdHandler(IMediatorHandler mediatorHandler, IClientUseCase clientUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _clientUseCase = clientUseCase;
        }

        public async Task<GetClientByIdOutput> Handle(GetClientByIdCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;
                var id = input.Id;

                var client = await _clientUseCase.GetClientById(id);

                if (client == null)
                {
                    var message = "O cliente não foi encontrado";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var output = new GetClientByIdOutput()
                {
                    Id = id,
                    Name = client.Name,
                    Email = client.Email,
                    Phone = client.Phone,
                    ConsultorId = client.ConsultorId,
                    CreatedAt = client.CreatedAt
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
