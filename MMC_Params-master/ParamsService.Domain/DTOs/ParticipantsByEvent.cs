using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamsService.Domain.DTOs
{
    public class ParticipantsByEvent
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public bool ? Is_Participated { get; set; }
    }
}
