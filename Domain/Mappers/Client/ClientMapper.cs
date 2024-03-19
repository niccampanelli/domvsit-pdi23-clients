using Domain.Dto.Client;
using Domain.Entities.Client;

namespace Domain.Mappers.Client
{
    public static class ClientMapper
    {
        public static ClientEntity MapToEntity(this ClientDto input)
        {
            return new ClientEntity()
            {
                Id = input.Id,
                Name = input.Name,
                Email = input.Email,
                Phone = input.Phone,
                ConsultorId = input.ConsultorId,
                CreatedAt = input.CreatedAt
            };
        }

        public static ClientDto MapToDto(this ClientEntity input)
        {
            return new ClientDto()
            {
                Id = input.Id,
                Name = input.Name,
                Email = input.Email,
                Phone = input.Phone,
                ConsultorId = input.ConsultorId,
                CreatedAt = input.CreatedAt
            };
        }

        public static ClientEntity UpdateEntity(this ClientEntity entity, UpdateClientInputDto input)
        {
            entity.Name = input.Name ?? entity.Name;
            entity.Email = input.Email ?? entity.Email;
            entity.Phone = input.Phone ?? entity.Phone;

            return entity;
        }
    }
}
