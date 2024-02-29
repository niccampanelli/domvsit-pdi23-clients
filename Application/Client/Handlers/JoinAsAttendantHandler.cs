using Application.Client.Boundaries.JoinAsAttendant;
using Application.Client.Commands;
using Application.UseCase.Attendant;
using Application.UseCase.Client;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Dto.Attendant;
using MediatR;

namespace Application.Client.Handlers
{
    public class JoinAsAttendantHandler : IRequestHandler<JoinAsAttendantCommand, JoinAsAttendantOutput>
    {
        private IMediatorHandler _mediatorHandler;
        private IAttendantUseCase _attendantUseCase;
        private IClientUseCase _clientUseCase;

        public JoinAsAttendantHandler(IMediatorHandler mediatorHandler, IAttendantUseCase attendantUseCase, IClientUseCase clientUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _attendantUseCase = attendantUseCase;
            _clientUseCase = clientUseCase;
        }

        public async Task<JoinAsAttendantOutput> Handle(JoinAsAttendantCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;
                input.Email = input.Email.ToLower().Trim();

                var attendantToken = await _clientUseCase.FindAttendantToken(input.AttendantToken);
                var clientId = attendantToken?.ClientId ?? 0L;

                if (clientId == 0L)
                {
                    var message = "Nenhum cliente encontrado com o token de participante informado";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                if (await _attendantUseCase.VerifyEmailInUse(input.Email) == true)
                {
                    var message = "Já existe um participante com o email informado";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var client = await _clientUseCase.GetClientById(clientId);

                if (client == null)
                {
                    var message = "O cliente associado ao token informado não foi encontrado e pode não existir mais";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var inputEmailDomain = input.Email.Split("@")[1];
                var clientEmailDomain = client.Email.Split("@")[1];

                var isEmailInDomain = inputEmailDomain.Equals(clientEmailDomain);

                var createInput = new AttendantDto()
                {
                    Name = input.Name,
                    Email = input.Email,
                    Role = input.Role,
                    ClientId = clientId
                };

                var createResult = await _attendantUseCase.CreateAttendant(createInput);

                var output = new JoinAsAttendantOutput()
                {
                    Id = createResult.Id ?? 0L,
                    IsEmailInDomain = isEmailInDomain
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
