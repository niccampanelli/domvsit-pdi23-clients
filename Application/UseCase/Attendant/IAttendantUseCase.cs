using Domain.Dto.Attendant;

namespace Application.UseCase.Attendant
{
    public interface IAttendantUseCase
    {
        Task<AttendantDto> CreateAttendant(AttendantDto input);
    }
}
