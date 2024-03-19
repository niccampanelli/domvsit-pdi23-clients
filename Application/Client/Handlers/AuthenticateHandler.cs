using Application.Client.Boundaries.Authenticate;
using Application.Client.Commands;
using Application.UseCase.Attendant;
using Application.UseCase.Client;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Dto.Attendant;
using MediatR;

namespace Application.Client.Handlers
{
    public class AuthenticateHandler : IRequestHandler<AuthenticateCommand, AuthenticateOutput>
    {
        private IMediatorHandler _mediatorHandler;
        private IAttendantUseCase _attendantUseCase;
        private IClientUseCase _clientUseCase;

        public AuthenticateHandler(IMediatorHandler mediatorHandler, IAttendantUseCase attendantUseCase, IClientUseCase clientUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _attendantUseCase = attendantUseCase;
            _clientUseCase = clientUseCase;
        }

        public async Task<AuthenticateOutput> Handle(AuthenticateCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;
                input.Email = input.Email.ToLower().Trim();

                var attendant = await _attendantUseCase.GetAttendantByEmail(input.Email);

                if (attendant == null)
                {
                    var message = "Nenhum participante cadastrado com este email";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var attendantToken = await _clientUseCase.FindAttendantToken(input.AttendantToken);
                var clientId = attendantToken?.ClientId ?? 0L;

                if (clientId == 0L)
                {
                    var message = "Nenhum cliente encontrado com o token de participante informado";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                if (clientId != attendant.ClientId)
                {
                    var message = "Acesso do participante no cliente não autorizado";
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

                var generateTokenInput = new GenerateTokenForAttendantInputDto()
                {
                    AttendantId = attendant.Id ?? 0L
                };

                var generatedTokens = await _attendantUseCase.GenerateTokenForAttendant(generateTokenInput);

                if (generatedTokens == null || generatedTokens.Token == null)
                {
                    var message = "Não foi possível gerar os tokens de acesso";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var output = new AuthenticateOutput()
                {
                    Id = attendant.Id ?? 0L,
                    Name = attendant.Name,
                    Email = attendant.Email,
                    Role = attendant.Role,
                    ClientId = clientId,
                    Token = generatedTokens.Token,
                    RefreshToken = generatedTokens.RefreshToken,
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
