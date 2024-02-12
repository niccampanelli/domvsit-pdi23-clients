using Domain.Dto.Attendant;

namespace Domain.Repository
{
    public interface IAttendantRepository
    {
        Task<bool> VerifyEmailInUse(string email);
        Task<AttendantDto> CreateAttendant(AttendantDto input);
        Task<AttendantDto> GetAttendantByEmail(string email);
    }
}
