 
using MMC.Application.IRepositories;
using ParamsService.Infrastructure.Data;
using ParamsService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MMC.Infrastructure.Repositories;

public class ParticipantRepository : Repository<Participant>, IParticipantRepository
{
    private readonly MMC_Params mMC_Params;
    public ParticipantRepository(MMC_Params db) : base(db) { 
        mMC_Params = db;
    }

    public async Task<Participant?> FindEmailAsync(string? email)
    {
        var participants = await mMC_Params.Participants.Where(em=>em.Email == email).FirstOrDefaultAsync();
        return (participants);
    }
}