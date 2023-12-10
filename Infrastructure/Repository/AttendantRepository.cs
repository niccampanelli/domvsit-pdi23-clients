using Domain.Dto.Attendant;
using Domain.Mappers.Attendant;
using Domain.Repository;
using Infrastructure.Setup;

namespace Infrastructure.Repository
{
    public class AttendantRepository : IAttendantRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AttendantRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<AttendantDto> CreateAttendant(AttendantDto input)
        {
            var entity = input.MapToEntity();
            var result = await _databaseContext.Attendants.AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
            return result.Entity.MapToDto();
        }
    }
}
