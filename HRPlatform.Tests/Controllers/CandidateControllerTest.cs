using AutoMapper;
using HRPlatform.Controllers;
using HRPlatform.Interfaces;
using HRPlatform.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace HRPlatform.Tests.Controllers
{
    [TestClass]
    public class CandidateControllerTest
    {
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Candidate, CandidateDTO>();
                cfg.CreateMap<Skill, SkillDTO>();
            });
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            Mapper.Reset();
        }

        [TestMethod]
        public void GetReturnsCandidateWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<ICandidateRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(new Candidate
            {
                Id = 1,
                Name = "Marko Markovic",
                DateOfBirth = "03/08/1994",
                ContactNumber = "555-666",
                Email = "can6@gmail.com",
                Skills = { new Skill() { Id = 1, Name = "C#" }, new Skill() { Id = 2, Name = "JQuery" } }
            });

            var controller = new CandidatesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<CandidateDTO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("Marko Markovic", contentResult.Content.Name);
            Assert.AreEqual("03/08/1994", contentResult.Content.DateOfBirth);
            Assert.AreEqual("555-666", contentResult.Content.ContactNumber);
            Assert.AreEqual("can6@gmail.com", contentResult.Content.Email);
            Assert.AreEqual(2, contentResult.Content.Skills.Count);


        }

        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<ICandidateRepository>();
            var controller = new CandidatesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<ICandidateRepository>();
            var controller = new CandidatesController(mockRepository.Object);

            // Act

            IHttpActionResult actionResult = controller.Put(1, new Candidate
            {
                Id = 1,
                Name = "Marko Markovic",
                DateOfBirth = "03/08/1994",
                ContactNumber = "555-666",
                Email = "can6@gmail.com",
                Skills = { new Skill() { Id = 1, Name = "C#" }, new Skill() { Id = 2, Name = "JQuery" } }
            });

            var result = actionResult as OkNegotiatedContentResult<Candidate>;


            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Candidate>));
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Id);
            Assert.AreEqual("Marko Markovic", result.Content.Name);
            Assert.AreEqual("03/08/1994", result.Content.DateOfBirth);
            Assert.AreEqual("555-666", result.Content.ContactNumber);
            Assert.AreEqual("can6@gmail.com", result.Content.Email);
            Assert.AreEqual(2, result.Content.Skills.Count);
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<ICandidateRepository>();
            var controller = new CandidatesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(10, new Candidate
            {
                Id = 1,
                Name = "Marko Markovic",
                DateOfBirth = "03/08/1994",
                ContactNumber = "555-666",
                Email = "can6@gmail.com",
                Skills = { new Skill() { Id = 1, Name = "C#" }, new Skill() { Id = 2, Name = "JQuery" } }

            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PostReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<ICandidateRepository>();
            var controller = new CandidatesController(mockRepository.Object);
            controller.ModelState.AddModelError("Name", "Name of candidate contains more than 40 characters.");
            // Act
            IHttpActionResult actionResult = controller.Post(new Candidate
            {
                Id = 1,
                Name = "Hubert Blaine Wolfe­schlegel­stein­hausen­berger­dorff Sr",
                DateOfBirth = "03/08/1994",
                ContactNumber = "555-666",
                Email = "can6@gmail.com",
                Skills = { new Skill() { Id = 1, Name = "C#" }, new Skill() { Id = 2, Name = "JQuery" } }

            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InvalidModelStateResult));
        }


        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            // Arrange
            List<Candidate> candidates = new List<Candidate>();
            candidates.Add(new Candidate()
            {
                Id = 1,
                Name = "Marko Markovic",
                DateOfBirth = "03/08/1994",
                ContactNumber = "555-666",
                Email = "can6@gmail.com"

            });
            candidates.Add(new Candidate()
            {
                Id = 2,
                Name = "Petar Petrovic",
                DateOfBirth = "05/09/1994",
                ContactNumber = "555-888",
                Email = "can7@gmail.com"

            });
            var mockRepository = new Mock<ICandidateRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(candidates.AsQueryable());
            var controller = new CandidatesController(mockRepository.Object);

            // Act
            IQueryable<CandidateDTO> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(candidates.Count, result.ToList().Count);

            Assert.AreEqual(candidates.ElementAt(0).Id, result.ToList().ElementAt(0).Id);
            Assert.AreEqual(candidates.ElementAt(0).Name, result.ToList().ElementAt(0).Name);


            Assert.AreEqual(candidates.ElementAt(1).Id, result.ToList().ElementAt(1).Id);
            Assert.AreEqual(candidates.ElementAt(1).Name, result.ToList().ElementAt(1).Name);
        }

    }
}
