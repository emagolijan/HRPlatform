using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRPlatform.Models
{
    public class Skill
    {
        public int Id { get; set; }

        [StringLength(20, ErrorMessage = "Max allowed characters are 20.")]
        public string Name { get; set; }
        public ICollection<Candidate> Candidates { get; set; }

        public Skill()
        {
            Candidates = new List<Candidate>();
        }
    }
}