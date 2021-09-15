using HRPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Interfaces
{
    public interface ICandidateRepository
    {
        IQueryable<Candidate> GetAll();
        Candidate GetById(int id);
        void Add(Candidate candidate);
        void Update(Candidate candidate);
        void Delete(Candidate candidate);
        IQueryable<Candidate> SearchCandidate(SearchCandidate searchCandidate);
    }
}
