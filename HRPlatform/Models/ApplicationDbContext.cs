using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HRPlatform.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public ApplicationDbContext(): base("HRPlatform")
        {
        }
    }
}