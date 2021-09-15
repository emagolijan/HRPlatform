namespace HRPlatform.Migrations
{
    using HRPlatform.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HRPlatform.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HRPlatform.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var skill1 = new Skill() { Id = 1, Name = "C#" };
            var skill2 = new Skill() { Id = 2, Name = "English language" };
            var skill3 = new Skill() { Id = 3, Name = "Java script" };


            var cand1 = new Candidate() { Id = 1, Name = "Johnny English", DateOfBirth = "02/02/1994", ContactNumber = "555-999", Email = "can1@gmail.com" };
            var cand2 = new Candidate() { Id = 2, Name = "Brad Pit", DateOfBirth = "03/09/1994", ContactNumber = "555-888", Email = "can2@gmail.com" };
            var cand3 = new Candidate() { Id = 3, Name = "Ema Watson", DateOfBirth = "10/03/1994", ContactNumber = "555-777", Email = "can3@gmail.com" };

            cand1.Skills.Add(skill1);
            cand1.Skills.Add(skill2);
            cand1.Skills.Add(skill3);

            cand2.Skills.Add(skill1);
           
            cand3.Skills.Add(skill2);
            cand3.Skills.Add(skill3);


            context.Candidates.AddOrUpdate(cand1);
            context.Candidates.AddOrUpdate(cand2);
            context.Candidates.AddOrUpdate(cand3);

            context.SaveChanges();
        }
    }
}
