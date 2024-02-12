using Domain.Dto.Client;

namespace Application.UseCase.Client
{
    public interface IClientUseCase
    {
        Task<bool> VerifyEmailInUse(string email);
        Task<ClientDto> CreateClient(ClientDto input);
        Task<ClientDto> GetClientById(long id);
        Task<AttendantTokenDto> FindAttendantToken(string attendantToken);
        Task<AttendantTokenDto> GenerateAttendantToken(long validityInMinutes, long clientId);
        Task<AttendantTokenDto> RegisterAttendantTokenSession(AttendantTokenDto input);
        Task<bool> VerifyClientExistsById(long id);
    }
}
