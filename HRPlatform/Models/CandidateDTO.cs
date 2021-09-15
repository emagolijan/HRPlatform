using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRPlatform.Models
{
    public class CandidateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public ICollection<SkillDTO> Skills { get; set; }
    }
}