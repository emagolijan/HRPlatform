using HRPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Interfaces
{
    public interface ISkillRepository
    {
        IQueryable<Skill> GetAll();
        Skill GetById(int id);
        void Add(Skill skill);
        void Update(Skill skill);
        void Delete(Skill skill);
    }
}
