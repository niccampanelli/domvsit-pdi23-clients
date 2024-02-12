using Domain.Dto.Client;
using Domain.Mappers.Client;
using Domain.Repository;
using Infrastructure.Setup;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ClientRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> VerifyEmailInUse(string email)
        {
            return await _databaseContext.Clients.AnyAsync(c => c.Email.Equals(email));
        }

        public async Task<ClientDto> CreateClient(ClientDto input)
        {
            var entity = input.MapToEntity();
            var result = await _databaseContext.Clients.AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
            return result.Entity.MapToDto();
        }

        public async Task<ClientDto> GetClientById(long id)
        {
            var entity = await _databaseContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
                return default;

            return entity.MapToDto();
        }

        public async Task<bool> VerifyClientExistsById(long id)
        {
            return await _databaseContext.Clients.AnyAsync(c => c.Id == id);
        }
    }
}
