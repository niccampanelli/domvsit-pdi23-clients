using Domain.Dto.Client;
using Domain.Dto.Commom;

namespace Application.UseCase.Client
{
    public interface IClientUseCase
    {
        Task<bool> VerifyEmailInUse(string email);
        Task<ClientDto> CreateClient(ClientDto input);
        Task<ClientDto> UpdateClient(long id, UpdateClientInputDto input);
        Task<bool> DeleteClient(long id);
        Task DeleteClientEvents(long clientId);
        Task<ClientDto> GetClientById(long id);
        Task<AttendantTokenDto> GetAttendantTokenByClientId(long clientId);
        Task<AttendantTokenDto> FindAttendantToken(string attendantToken);
        Task<AttendantTokenDto> GenerateAttendantToken(long validityInMinutes, long clientId);
        Task<AttendantTokenDto> RegisterAttendantTokenSession(AttendantTokenDto input);
        Task<bool> VerifyClientExistsById(long id);
        Task<int> Count();
        Task<List<ClientDto>> List(ListClientInputDto input, PaginationInputDto? pagination, SortingInputDto? sorting);
    }
}
