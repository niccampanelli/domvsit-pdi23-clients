using Domain.Dto.Client;
using Domain.Entities.Client;

namespace Domain.Mappers.Client
{
    public static class AttendantTokenMapper
    {
        public static AttendantTokenEntity MapToEntity(this AttendantTokenDto input)
        {
            return new AttendantTokenEntity()
            {
                Id = input.Id,
                Value = input.Value,
                ClientId = input.ClientId,
                CreatedAt = input.CreatedAt,
                ExpiresAt = input.ExpiresAt
            };
        }

        public static AttendantTokenDto MapToDto(this AttendantTokenEntity input)
        {
            return new AttendantTokenDto()
            {
                Id = input.Id,
                Value = input.Value,
                ClientId = input.ClientId,
                CreatedAt = input.CreatedAt,
                ExpiresAt = input.ExpiresAt
            };
        }
    }
}
