using Domain.Dto.Client;

namespace Domain.Repository
{
    public interface IClientRepository
    {
        Task<bool> VerifyEmailInUse(string email);
        Task<ClientDto> CreateClient(ClientDto input);
        Task<bool> VerifyClientExistsById(long id);
    }
}
