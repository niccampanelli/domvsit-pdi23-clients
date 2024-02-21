using Application.Client.Boundaries.ListClient;
using Application.Client.Commands;
using Application.Commom.Boundaries;
using Application.UseCase.Attendant;
using Application.UseCase.Client;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Dto.Client;
using Domain.Dto.Commom;
using MediatR;

namespace Application.Client.Handlers
{
    public class ListClientHandler : IRequestHandler<ListClientCommand, PaginatedResponse<ListClientOutput>>
    {
        private IMediatorHandler _mediatorHandler;
        private IClientUseCase _clientUseCase;

        public ListClientHandler(IMediatorHandler mediatorHandler, IClientUseCase clientUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _clientUseCase = clientUseCase;
        }

        public async Task<PaginatedResponse<ListClientOutput>> Handle(ListClientCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;

                var clientCount = await _clientUseCase.Count();

                var listInput = new ListClientInputDto()
                {
                    Search = input.Search,
                    ConsultorId = input.ConsultorId
                };

                var paginationInput = new PaginationInputDto()
                {
                    Limit = input.Limit,
                    Page = input.Page
                };

                var sortingInput = new SortingInputDto()
                {
                    SortField = input.SortField,
                    SortOrder = input.SortOrder
                };

                var clientList = await _clientUseCase.List(listInput, paginationInput, sortingInput);

                var clientListCount = clientList.Count();

                var output = new PaginatedResponse<ListClientOutput>()
                {
                    CurrentPage = input.Page ?? 1,
                    ItemsCount = clientListCount,
                    Data = clientList.Select(c => new ListClientOutput()
                    {
                        Id = c.Id ?? 0L,
                        Name = c.Name,
                        Email = c.Email,
                        Phone = c.Phone,
                        ConsultorId = c.ConsultorId,
                        CreatedAt = c.CreatedAt
                    }).ToList(),
                    Total = clientCount
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
