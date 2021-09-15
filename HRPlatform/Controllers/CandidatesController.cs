using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRPlatform.Interfaces;
using HRPlatform.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace HRPlatform.Controllers
{
    public class CandidatesController : ApiController
    {
        ICandidateRepository _repository { get; set; }

        public CandidatesController(ICandidateRepository repository)
        {
            _repository = repository;
        }

        [Route("api/candidates")]
        [HttpGet]
        public IQueryable<CandidateDTO> Get()
        {
            return _repository.GetAll().ProjectTo<CandidateDTO>();
        }

        public IHttpActionResult Get(int id)
        {
            var candidate = _repository.GetById(id);
            if (candidate == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<CandidateDTO>(candidate));
        }

        [Route("api/candidates")]
        [HttpPost]
        public IHttpActionResult Post(Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(candidate);
            return Ok(candidate);
        }

        public IHttpActionResult Put(int id, Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != candidate.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(candidate);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(candidate);
        }

        public IHttpActionResult Delete(int id)
        {
            var candidate = _repository.GetById(id);
            if (candidate == null)
            {
                return NotFound();
            }

            _repository.Delete(candidate);
            return Ok();
        }

        [Route("api/candidates/search")]
        public IQueryable<CandidateDTO> PostSearch(SearchCandidate searchCandidate)
        {
            return _repository.SearchCandidate(searchCandidate).ProjectTo<CandidateDTO>();
        }
    }
}
