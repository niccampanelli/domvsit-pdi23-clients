using Domain.Dto.Client;

namespace Domain.Repository
{
    public interface IClientRepository
    {
        Task<bool> VerifyEmailInUse(string id);
        Task<ClientDto> CreateClient(ClientDto input);
        Task<string> VerifyClientExistsById(string id);
    }
}
