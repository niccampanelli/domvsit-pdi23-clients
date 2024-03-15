using Domain.Dto.Client;
using Domain.Mappers.Client;
using Domain.Repository;
using Infrastructure.Setup;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AttendantTokenRepository : IAttendantTokenRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AttendantTokenRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<AttendantTokenDto> GetAttendantTokenByClientId(long clientId)
        {
            var entity = await _databaseContext.AttendantTokens.FirstOrDefaultAsync(x => x.ClientId == clientId);
            if (entity == null)
                return default;

            return entity.MapToDto();
        }

        public async Task<AttendantTokenDto> FindAttendantToken(string attendantToken)
        {
            var entity = await _databaseContext.AttendantTokens.FirstOrDefaultAsync(t => t.Value.Equals(attendantToken));
            if (entity == null)
                return default;

            return entity.MapToDto();
        }

        public async Task<AttendantTokenDto> RegisterAttendantTokenSession(AttendantTokenDto input)
        {
            var entity = input.MapToEntity();

            var alreadyStored = await _databaseContext.AttendantTokens.Where(token => token.ClientId == input.ClientId).ToListAsync();

            if (alreadyStored.Any())
            {
                _databaseContext.AttendantTokens.RemoveRange(alreadyStored);
            }

            var result = await _databaseContext.AttendantTokens.AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
            return result.Entity.MapToDto();
        }
    }
}
