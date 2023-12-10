using Domain.Dto.Attendant;
using Domain.Entities.Attendant;

namespace Domain.Mappers.Attendant
{
    public static class AttendantMapper
    {
        public static AttendantEntity MapToEntity(this AttendantDto input)
        {
            return new AttendantEntity()
            {
                Id = input.Id,
                Name = input.Name,
                Role = input.Role,
                Email = input.Email,
                ClientId = input.ClientId,
                CreatedAt = input.CreatedAt
            };
        }

        public static AttendantDto MapToDto(this AttendantEntity input)
        {
            return new AttendantDto()
            {
                Id = input.Id,
                Name = input.Name,
                Role = input.Role,
                Email = input.Email,
                ClientId = input.ClientId,
                CreatedAt = input.CreatedAt
            };
        }
    }
}
