namespace SpeakerService.Api.model
{
    public class Images
    {
        public required IFormFile File { get; set; }
        public required Guid EventId { get; set; }
    }
}
