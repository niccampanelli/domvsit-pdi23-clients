using Domain.Dto.Attendant;
using Domain.Dto.Commom;

namespace Domain.Repository
{
    public interface IAttendantRepository
    {
        Task<bool> VerifyEmailInUse(string email);
        Task<AttendantDto> CreateAttendant(AttendantDto input);
        Task<AttendantDto> GetAttendantByEmail(string email);
        Task<int> Count();
        Task<List<AttendantDto>> List(ListInputDto input, PaginationInputDto? pagination, SortingInputDto? sorting);
    }
}
