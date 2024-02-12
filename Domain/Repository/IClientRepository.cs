using Domain.Dto.Client;

namespace Domain.Repository
{
    public interface IClientRepository
    {
        Task<bool> VerifyEmailInUse(string email);
        Task<ClientDto> CreateClient(ClientDto input);
        Task<ClientDto> GetClientById(long id);
        Task<bool> VerifyClientExistsById(long id);
    }
}
