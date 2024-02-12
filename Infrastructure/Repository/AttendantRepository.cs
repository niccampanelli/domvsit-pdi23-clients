using Domain.Dto.Attendant;
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
    }
}
