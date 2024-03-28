using Domain.Dto.Client;
using Domain.Dto.Commom;
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

        public async Task<ClientDto> UpdateClient(long id, UpdateClientInputDto input)
        {
            var entity = await _databaseContext.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (entity != null)
            {
                var updatedEntity = entity.UpdateEntity(input);
                await _databaseContext.SaveChangesAsync();
                return entity.MapToDto();
            }

            return default;
        }

        public async Task<bool> DeleteClient(long id)
        {
            var entity = await _databaseContext.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (entity == null)
            {
                return false;
            }

            _databaseContext.Clients.Remove(entity);
            await _databaseContext.SaveChangesAsync();
            return true;
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

        public async Task<int> Count()
        {
            var result = await _databaseContext.Clients.CountAsync();
            return result;
        }

        public async Task<List<ClientDto>> List(ListClientInputDto input, PaginationInputDto? pagination, SortingInputDto? sorting)
        {
            var query = _databaseContext.Clients.AsQueryable();

            if (input.Search != null)
            {
                query = query.Where(c =>
                    c.Name.ToLower().Trim().Contains(input.Search.ToLower().Trim()) ||
                    c.Email.ToLower().Trim().Contains(input.Search.ToLower().Trim()) ||
                    c.Phone.ToLower().Trim().Contains(input.Search.ToLower().Trim())
                );
            }

            if (input.ConsultorId != null)
            {
                query = query.Where(c => c.ConsultorId == input.ConsultorId);
            }

            if (sorting?.SortField != null)
            {
                switch (sorting?.SortField.ToLower().Trim())
                {
                    case "name":
                        if (sorting?.SortOrder != null && sorting.SortOrder.ToLower().Trim().Equals("desc"))
                            query = query.OrderByDescending(c => c.Name);
                        else
                            query = query.OrderBy(c => c.Name);
                        break;
                    case "email":
                        if (sorting?.SortOrder != null && sorting.SortOrder.ToLower().Trim().Equals("desc"))
                            query = query.OrderByDescending(c => c.Email);
                        else
                            query = query.OrderBy(c => c.Email);
                        break;
                    case "phone":
                        if (sorting?.SortOrder != null && sorting.SortOrder.ToLower().Trim().Equals("desc"))
                            query = query.OrderByDescending(c => c.Phone);
                        else
                            query = query.OrderBy(c => c.Phone);
                        break;
                    case "createdAt":
                        if (sorting?.SortOrder != null && sorting.SortOrder.ToLower().Trim().Equals("desc"))
                            query = query.OrderByDescending(c => c.CreatedAt);
                        else
                            query = query.OrderBy(c => c.CreatedAt);
                        break;
                    default:
                        if (sorting?.SortOrder != null && sorting.SortOrder.ToLower().Trim().Equals("desc"))
                            query = query.OrderByDescending(c => c.CreatedAt);
                        else
                            query = query.OrderBy(c => c.CreatedAt);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(c => c.CreatedAt);
            }

            if (pagination != null)
            {
                if (pagination?.Page != null && pagination.Page != 0 && pagination?.Limit != null && pagination.Limit != 0)
                {
                    var skip = ((pagination.Page - 1) * pagination.Limit) ?? 0;
                    var take = pagination.Limit ?? 10;

                    query = query.Skip(skip).Take(take);
                }
            }

            var result = query.Select(a => a.MapToDto());
            return await result.ToListAsync();
        }
    }
}
