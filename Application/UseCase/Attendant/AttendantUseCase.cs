using Domain.Dto.Attendant;
using Domain.Repository;

namespace Application.UseCase.Attendant
{
    public class AttendantUseCase : IAttendantUseCase
    {
        private readonly IAttendantRepository _attendantRepository;

        public AttendantUseCase(IAttendantRepository attendantRepository)
        {
            _attendantRepository = attendantRepository;
        }

        public async Task<bool> VerifyEmailInUse(string email)
        {
            return await _attendantRepository.VerifyEmailInUse(email);
        }

        public async Task<AttendantDto> CreateAttendant(AttendantDto input)
        {
            input.CreatedAt = DateTime.UtcNow;
            return await _attendantRepository.CreateAttendant(input);
        }

        public async Task<AttendantDto> GetAttendantByEmail(string email)
        {
            return await _attendantRepository.GetAttendantByEmail(email);
        }
    }
}
