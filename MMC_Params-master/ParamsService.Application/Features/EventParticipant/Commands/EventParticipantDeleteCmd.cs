using MediatR;

namespace MMC.Application.Features.EventParticipant.Commands;

public record EventParticipantDeleteCmd(Guid Id, Guid id) : IRequest<bool>;