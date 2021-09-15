using HRPlatform.Interfaces;
using HRPlatform.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HRPlatform.Repository
{
    public class SkillRepository : IDisposable, ISkillRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Skill skill)
        {
            db.Skills.Add(skill);
            db.SaveChanges();
        }

        public void Delete(Skill skill)
        {
            db.Skills.Remove(skill);
            db.SaveChanges();
        }

        public IQueryable<Skill> GetAll()
        {
            return db.Skills;
        }

        public Skill GetById(int id)
        {
            return db.Skills.Include(x=> x.Candidates).FirstOrDefault(x => x.Id == id);
        }

        public void Update(Skill skill)
        {
            db.Entry(skill).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}