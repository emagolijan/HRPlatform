using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRPlatform.Models
{
    public class SkillDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
    }
}