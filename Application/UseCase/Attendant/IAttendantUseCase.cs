using Domain.Dto.Attendant;
using Domain.Dto.Commom;

namespace Application.UseCase.Attendant
{
    public interface IAttendantUseCase
    {
        Task<bool> VerifyEmailInUse(string email);
        Task<AttendantDto> CreateAttendant(AttendantDto input);
        Task<AttendantDto> GetAttendantByEmail(string email);
        Task<AttendantDto> GetAttendantById(long id);
        Task<int> Count();
        Task<List<AttendantDto>> List(ListAttendantInputDto input, PaginationInputDto? pagination, SortingInputDto? sorting);
        Task<GenerateTokenForAttendantOutputDto> GenerateTokenForAttendant(GenerateTokenForAttendantInputDto input);
    }
}
