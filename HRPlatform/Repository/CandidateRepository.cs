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
    public class CandidateRepository : IDisposable, ICandidateRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Candidate candidate)
        {
            Candidate candDb = new Candidate();
            candDb.Name = candidate.Name;
            candDb.Email = candidate.Email;
            candDb.ContactNumber = candidate.ContactNumber;
            candDb.DateOfBirth = candidate.DateOfBirth;
            foreach (Skill skill in candidate.Skills)
            {
                Skill skilldb = db.Skills.FirstOrDefault(x => x.Id == skill.Id);
                candDb.Skills.Add(skilldb);
            }
            db.Candidates.Add(candDb);
            db.SaveChanges();
        }

        public void Delete(Candidate candidate)
        {
            db.Candidates.Remove(candidate);
            db.SaveChanges();
        }

        public IQueryable<Candidate> GetAll()
        {
            return db.Candidates;
        }

        public Candidate GetById(int id)
        {
            return db.Candidates.Include(x => x.Skills).FirstOrDefault(x => x.Id == id);
        }

        public void Update(Candidate candidate)
        {
            Candidate candDb = db.Candidates.Include(x => x.Skills).FirstOrDefault(x => x.Id == candidate.Id);
            candDb.Name = candidate.Name;
            candDb.Email = candidate.Email;
            candDb.ContactNumber = candidate.ContactNumber;
            candDb.DateOfBirth = candidate.DateOfBirth;
            candDb.Skills.Clear();
            foreach (Skill skill in candidate.Skills)
            {
                Skill skilldb = db.Skills.FirstOrDefault(x => x.Id == skill.Id);
                candDb.Skills.Add(skilldb);
            }
            //db.Entry(candidate).State = EntityState.Modified;
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

        public IQueryable<Candidate> SearchCandidate(SearchCandidate searchCandidate)
        {
            List<Candidate> candidatesByName = new List<Candidate>();
            List<Candidate> candidatesBySkill = new List<Candidate>();
            List<Candidate> candidatesAllSearched = new List<Candidate>();

            if (!string.IsNullOrEmpty(searchCandidate.Name))
            {
                candidatesByName.AddRange(db.Candidates.Include(s => s.Skills).Where(x => x.Name.Contains(searchCandidate.Name)).ToList());
            }
            if (!string.IsNullOrEmpty(searchCandidate.SkillName))
            {
                Skill skill = db.Skills.Include(c => c.Candidates).Where(x=> x.Name == searchCandidate.SkillName).ToList().FirstOrDefault();
                if (skill != null)
                {
                    candidatesBySkill.AddRange(skill.Candidates);
                }
            }

            if (candidatesByName.Count > 0 && candidatesBySkill.Count > 0)
            {
                candidatesAllSearched = candidatesByName.Intersect(candidatesBySkill).ToList();
            }
            else
            {
                candidatesByName.AddRange(candidatesBySkill);
                candidatesAllSearched = candidatesByName.GroupBy(x => x.Id).Select(grp => grp.First()).ToList();
            }
            //spoj sve liste (po imenu i po skilovima)
            //izvuci jedinstvene po id ju

            return candidatesAllSearched.AsQueryable();
        }
    }
}