using ParamsService.Domain.DTOs;

namespace ParamsService.Application.Interfaces
{
    public interface IEventParticipantService
    {
        Task<IEnumerable<ParticipantsByEvent>> GetAllEventParticipantByEvent(Guid id);
        Task<IEnumerable<EventParticipantGetDto>> FindAllAsync();
        Task<EventParticipantCreateDto> CreateAsync(EventParticipantCreateDto EventPartnerPostDTO);
        Task<bool> DeleteAsync(Guid Id, Guid id);
        Task<bool> UpdateAsync(EventParticipantUpdateDto EventParticipantPostDTO);
    }
}