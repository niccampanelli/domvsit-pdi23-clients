﻿using Domain.Dto.Client;
using Domain.Dto.Commom;

namespace Domain.Repository
{
    public interface IClientRepository
    {
        Task<bool> VerifyEmailInUse(string email);
        Task<ClientDto> CreateClient(ClientDto input);
        Task<ClientDto> GetClientById(long id);
        Task<bool> VerifyClientExistsById(long id);
        Task<int> Count();
        Task<List<ClientDto>> List(ListClientInputDto input, PaginationInputDto? pagination, SortingInputDto? sorting);
    }
}
