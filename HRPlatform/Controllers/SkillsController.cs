using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRPlatform.Interfaces;
using HRPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRPlatform.Controllers
{
    public class SkillsController : ApiController
    {
        ISkillRepository _repository { get; set; }

        public SkillsController(ISkillRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<SkillDTO> Get()
        {
            return _repository.GetAll().ProjectTo<SkillDTO>();
        }

        public IHttpActionResult Get(int id)
        {
            var skill = _repository.GetById(id);
            if (skill == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<SkillDTO>(skill));
        }

        public IHttpActionResult Post(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(skill);
            return CreatedAtRoute("DefaultApi", new { id = skill.Id }, skill);
        }

        public IHttpActionResult Put(int id, Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skill.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(skill);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(skill);
        }

        public IHttpActionResult Delete(int id)
        {
            var skill = _repository.GetById(id);
            if (skill == null)
            {
                return NotFound();
            }

            _repository.Delete(skill);
            return Ok();
        }
    }
}
