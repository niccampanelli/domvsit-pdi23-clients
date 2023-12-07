using Domain.Dto.Client;

namespace Domain.Repository
{
    public interface IAttendantTokenRepository
    {
        Task<AttendantTokenDto> RegisterAttendantTokenSession(AttendantTokenDto input);
    }
}
