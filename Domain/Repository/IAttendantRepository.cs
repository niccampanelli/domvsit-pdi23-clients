﻿using Domain.Dto.Attendant;

namespace Domain.Repository
{
    public interface IAttendantRepository
    {
        Task<AttendantDto> CreateAttendant(AttendantDto input);
    }
}
