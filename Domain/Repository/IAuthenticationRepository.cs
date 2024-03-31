using Domain.Dto.Attendant;

namespace Domain.Repository
{
    public interface IAuthenticationRepository
    {
        Task<GenerateTokenForAttendantOutputDto> GenerateTokenForAttendant(GenerateTokenForAttendantInputDto input);
        Task<ExtractIdFromTokenOutputDto> ExtractIdFromToken(string token);
    }
}
