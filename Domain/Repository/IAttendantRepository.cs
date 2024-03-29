﻿using Domain.Dto.Attendant;
using Domain.Dto.Commom;

namespace Domain.Repository
{
    public interface IAttendantRepository
    {
        Task<bool> VerifyEmailInUse(string email);
        Task<AttendantDto> CreateAttendant(AttendantDto input);
        Task<AttendantDto> GetAttendantByEmail(string email);
        Task<AttendantDto> GetAttendantById(long id);
        Task<int> Count();
        Task<List<AttendantDto>> List(ListAttendantInputDto input, PaginationInputDto? pagination, SortingInputDto? sorting);
    }
}
