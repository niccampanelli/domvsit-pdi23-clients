﻿using Domain.Dto.Client;

namespace Domain.Repository
{
    public interface IAttendantTokenRepository
    {
        Task<AttendantTokenDto> GetAttendantTokenByClientId(long clientId);
        Task<AttendantTokenDto> FindAttendantToken(string attendantToken);
        Task<AttendantTokenDto> RegisterAttendantTokenSession(AttendantTokenDto input);
    }
}
