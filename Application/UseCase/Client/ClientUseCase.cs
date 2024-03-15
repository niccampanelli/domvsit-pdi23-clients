using Domain.Dto.Client;
using Domain.Dto.Commom;
using Domain.Repository;
using System.Text.RegularExpressions;

namespace Application.UseCase.Client
{
    public class ClientUseCase : IClientUseCase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAttendantTokenRepository _attendantTokenRepository;

        public ClientUseCase(IClientRepository clientRepository, IAttendantTokenRepository attendantTokenRepository)
        {
            _clientRepository = clientRepository;
            _attendantTokenRepository = attendantTokenRepository;
        }

        public async Task<bool> VerifyEmailInUse(string email)
        {
            return await _clientRepository.VerifyEmailInUse(email);
        }

        public async Task<ClientDto> CreateClient(ClientDto input)
        {
            input.CreatedAt = DateTime.UtcNow;
            return await _clientRepository.CreateClient(input);
        }

        public async Task<ClientDto> GetClientById(long id)
        {
            return await _clientRepository.GetClientById(id);
        }

        public async Task<AttendantTokenDto> GetAttendantTokenByClientId(long clientId)
        {
            return await _attendantTokenRepository.GetAttendantTokenByClientId(clientId);
        }

        public async Task<AttendantTokenDto> FindAttendantToken(string attendantToken)
        {
            return await _attendantTokenRepository.FindAttendantToken(attendantToken);
        }

        public async Task<AttendantTokenDto> GenerateAttendantToken(long validityInMinutes, long clientId)
        {
            var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
            var expirationDate = DateTime.UtcNow.AddMinutes(validityInMinutes);

            var output = new AttendantTokenDto()
            {
                ClientId = clientId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = expirationDate,
                Value = uid
            };

            return output;
        }

        public async Task<AttendantTokenDto> RegisterAttendantTokenSession(AttendantTokenDto input)
        {
            return await _attendantTokenRepository.RegisterAttendantTokenSession(input);
        }

        public async Task<bool> VerifyClientExistsById(long id)
        {
            return await _clientRepository.VerifyClientExistsById(id);
        }

        public async Task<int> Count()
        {
            return await _clientRepository.Count();
        }

        public async Task<List<ClientDto>> List(ListClientInputDto input, PaginationInputDto? pagination, SortingInputDto? sorting)
        {
            return await _clientRepository.List(input, pagination, sorting);
        }
    }
}
