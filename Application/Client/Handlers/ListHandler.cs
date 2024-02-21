using Application.Client.Boundaries.List;
using Application.Client.Commands;
using Application.Commom.Boundaries;
using Application.UseCase.Attendant;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Dto.Attendant;
using Domain.Dto.Commom;
using MediatR;

namespace Application.Client.Handlers
{
    public class ListHandler : IRequestHandler<ListCommand, PaginatedResponse<ListOutput>>
    {
        private IMediatorHandler _mediatorHandler;
        private IAttendantUseCase _attendantUseCase;

        public ListHandler(IMediatorHandler mediatorHandler, IAttendantUseCase attendantUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _attendantUseCase = attendantUseCase;
        }

        public async Task<PaginatedResponse<ListOutput>> Handle(ListCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;

                var attendantCount = await _attendantUseCase.Count();

                var listInput = new ListInputDto()
                {
                    Search = input.Search,
                    ClientId = input.ClientId,
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

                var attendantList = await _attendantUseCase.List(listInput, paginationInput, sortingInput);

                var attendantListCount = attendantList.Count();

                var output = new PaginatedResponse<ListOutput>()
                {
                    CurrentPage = input.Page ?? 1,
                    ItemsCount = attendantListCount,
                    Data = attendantList.Select(a => new ListOutput()
                    {
                        Id = a.Id ?? 0L,
                        Name = a.Name,
                        Email = a.Email,
                        Role = a.Role,
                        ClientId = a.ClientId,
                        CreatedAt = a.CreatedAt
                    }).ToList(),
                    Total = attendantCount
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
