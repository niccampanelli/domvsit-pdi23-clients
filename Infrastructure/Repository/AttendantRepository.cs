using Domain.Dto.Attendant;
using Domain.Dto.Commom;
using Domain.Mappers.Attendant;
using Domain.Repository;
using Infrastructure.Setup;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AttendantRepository : IAttendantRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AttendantRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> VerifyEmailInUse(string email)
        {
            return await _databaseContext.Attendants.AnyAsync(a => a.Email.Equals(email));
        }

        public async Task<AttendantDto> CreateAttendant(AttendantDto input)
        {
            var entity = input.MapToEntity();
            var result = await _databaseContext.Attendants.AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
            return result.Entity.MapToDto();
        }

        public async Task<AttendantDto> GetAttendantByEmail(string email)
        {
            var entity = await _databaseContext.Attendants.FirstOrDefaultAsync(a => a.Email.Equals(email));
            if (entity == null)
                return default;

            return entity.MapToDto();
        }

        public async Task<int> Count()
        {
            var result = await _databaseContext.Attendants.CountAsync();
            return result;
        }

        public async Task<List<AttendantDto>> List(ListInputDto input, PaginationInputDto? pagination, SortingInputDto? sorting)
        {
            var query = _databaseContext.Attendants.AsQueryable();
        
            if (sorting?.SortField != null)
            {
                switch(sorting?.SortField.ToLower().Trim())
                {
                    case "name":
                        if (sorting?.SortOrder != null && sorting.SortOrder.ToLower().Trim().Equals("desc"))
                            query = query.OrderByDescending(a => a.Name);
                        else
                            query = query.OrderBy(a => a.Name);
                        break;
                    case "email":
                        if (sorting?.SortOrder != null && sorting.SortOrder.ToLower().Trim().Equals("desc"))
                            query = query.OrderByDescending(a => a.Email);
                        else
                            query = query.OrderBy(a => a.Email);
                        break;
                    case "role":
                        if (sorting?.SortOrder != null && sorting.SortOrder.ToLower().Trim().Equals("desc"))
                            query = query.OrderByDescending(a => a.Role);
                        else
                            query = query.OrderBy(a => a.Role);
                        break;
                    case "createdAt":
                        if (sorting?.SortOrder != null && sorting.SortOrder.ToLower().Trim().Equals("desc"))
                            query = query.OrderByDescending(a => a.CreatedAt);
                        else
                            query = query.OrderBy(a => a.CreatedAt);
                        break;
                    default:
                        if (sorting?.SortOrder != null && sorting.SortOrder.ToLower().Trim().Equals("desc"))
                            query = query.OrderByDescending(a => a.CreatedAt);
                        else
                            query = query.OrderBy(a => a.CreatedAt);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(a => a.CreatedAt);
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

            if (input.Search != null)
            {
                query = query.Where(a =>
                    a.Name.ToLower().Trim().Contains(input.Search.ToLower().Trim()) ||
                    a.Email.ToLower().Trim().Contains(input.Search.ToLower().Trim()) ||
                    a.Role.ToLower().Trim().Contains(input.Search.ToLower().Trim())
                );
            }

            if (input.ClientId != null)
            {
                query = query.Where(a => a.ClientId == input.ClientId);
            }

            var result = query.Select(a => a.MapToDto());
            return await result.ToListAsync();
        }
    }
}
