using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRPlatform.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name of candidate is required.")]
        [StringLength(40, ErrorMessage = "Max allowed characters are 40.")]
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Skill> Skills { get; set; }

        public Candidate()
        {
            Skills = new List<Skill>();
        }
    }
}