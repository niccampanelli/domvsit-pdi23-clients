using Domain.Dto.Attendant;

namespace Application.UseCase.Attendant
{
    public interface IAttendantUseCase
    {
        Task<bool> VerifyEmailInUse(string email);
        Task<AttendantDto> CreateAttendant(AttendantDto input);
        Task<AttendantDto> GetAttendantByEmail(string email);
    }
}
