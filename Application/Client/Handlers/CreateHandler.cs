using Application.Client.Boundaries.Create;
using Application.Client.Commands;
using Application.UseCase.Attendant;
using Application.UseCase.Client;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Dto.Attendant;
using Domain.Dto.Client;
using Domain.Option;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Client.Handlers
{
    public class CreateHandler : IRequestHandler<CreateCommand, CreateOutput>
    {
        private IMediatorHandler _mediatorHandler;
        private IAttendantUseCase _attendantUseCase;
        private IClientUseCase _clientUseCase;
        private IOptions<Secrets> _secrets;

        public CreateHandler(IMediatorHandler mediatorHandler, IAttendantUseCase attendantUseCase, IClientUseCase clientUseCase, IOptions<Secrets> secrets)
        {
            _mediatorHandler = mediatorHandler;
            _attendantUseCase = attendantUseCase;
            _clientUseCase = clientUseCase;
            _secrets = secrets;
        }

        public async Task<CreateOutput> Handle(CreateCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;

                input.Email = input.Email.ToLower().Trim();

                if (await _clientUseCase.VerifyEmailInUse(input.Email))
                {
                    var message = "O email do cliente já está em uso";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var createClientInput = new ClientDto()
                {
                    Name = input.Name,
                    Email = input.Email,
                    Phone = input.Phone
                };

                var createResult = await _clientUseCase.CreateClient(createClientInput);

                var attendantTokenValidity = _secrets.Value.AttendantTokenDefaultValidityInMinutes;
                var generateAttendantTokenResult = await _clientUseCase.GenerateAttendantToken(attendantTokenValidity, createResult.Id);

                var registerAttendantTokenSessionResult = await _clientUseCase.RegisterAttendantTokenSession(generateAttendantTokenResult);

                var createAttendantInput = new AttendantDto()
                {
                    Name = $"Administrador {input.Name}",
                    Email = input.Email,
                    ClientId = createResult.Id,
                    Role = "Administrador"
                };

                var createAttendantResult = await _attendantUseCase.CreateAttendant(createAttendantInput);

                var output = new CreateOutput()
                {
                    CreatedId = createResult.Id,
                    CreatedAttendantId = createAttendantResult.Id,
                    AttendantToken = new CreateAttendantTokenOutput()
                    {
                        Id = registerAttendantTokenSessionResult.Id,
                        Value = registerAttendantTokenSessionResult.Value,
                        CreatedAt = registerAttendantTokenSessionResult.CreatedAt,
                        ExpiresAt = registerAttendantTokenSessionResult.ExpiresAt
                    }
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
