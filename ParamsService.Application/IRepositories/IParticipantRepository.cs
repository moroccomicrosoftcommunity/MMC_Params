﻿
using ParamsService.Domain.Entities;

namespace MMC.Application.IRepositories;

public interface IParticipantRepository : IRepository<Participant>
{
    Task<Participant?> FindEmailAsync(string? email);
}