using Domain.Dto.Client;
using Domain.Mappers.Client;
using Domain.Repository;
using Infrastructure.Setup;

namespace Infrastructure.Repository
{
    public class AttendantTokenRepository : IAttendantTokenRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AttendantTokenRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<AttendantTokenDto> RegisterAttendantTokenSession(AttendantTokenDto input)
        {
            var entity = input.MapToEntity();
            var result = await _databaseContext.AttendantTokens.AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
            return result.Entity.MapToDto();
        }
    }
}
