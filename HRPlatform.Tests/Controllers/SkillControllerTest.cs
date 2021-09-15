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
    public class SkillControllerTest
    {
        [TestMethod]
        public void GetReturnsSkillWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<ISkillRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(new Skill
            {
                Id = 1,
                Name = "C#",
                Candidates = { new Candidate() { Id = 1, Name = "Ema" }, new Candidate() {Id = 2, Name = "Milos" } }
            });

            var controller = new SkillsController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<SkillDTO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("C#", contentResult.Content.Name);
            Assert.AreEqual(2, contentResult.Content.Candidates.Count);
        }
        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<ISkillRepository>();
            var controller = new SkillsController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<ISkillRepository>();
            var controller = new SkillsController(mockRepository.Object);

            // Act

            IHttpActionResult actionResult = controller.Put(1, new Skill
            {
                Id = 1,
                Name = "F#",
                Candidates = { new Candidate() { Id = 1, Name = "Ema" }, new Candidate() { Id = 2, Name = "Milos" } }
            });

            var result = actionResult as OkNegotiatedContentResult<Skill>;


            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Skill>));
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Id);
            Assert.AreEqual("F#", result.Content.Name);
            Assert.AreEqual(2, result.Content.Candidates.Count);
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<ISkillRepository>();
            var controller = new SkillsController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(10, new Skill
            {
                Id = 1,
                Name = "English",
               
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PostReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<ISkillRepository>();
            var controller = new SkillsController(mockRepository.Object);
            controller.ModelState.AddModelError("Name", "Name of skill contains more than 20 characters.");
            // Act
            IHttpActionResult actionResult = controller.Post(new Skill
            {
                Id = 1,
                Name = "Proficiency level of English language",
               
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InvalidModelStateResult));
        }


        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            // Arrange
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill()
            {
                Id = 1,
                Name = "German language",
                
            });
            skills.Add(new Skill()
            {
                Id = 2,
                Name = "Russian language",

            });
            var mockRepository = new Mock<ISkillRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(skills.AsQueryable());
            var controller = new SkillsController (mockRepository.Object);

            // Act
            IQueryable<SkillDTO> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(skills.Count, result.ToList().Count);

            Assert.AreEqual(skills.ElementAt(0).Id, result.ToList().ElementAt(0).Id);
            Assert.AreEqual(skills.ElementAt(0).Name, result.ToList().ElementAt(0).Name);


            Assert.AreEqual(skills.ElementAt(1).Id, result.ToList().ElementAt(1).Id);
            Assert.AreEqual(skills.ElementAt(1).Name, result.ToList().ElementAt(1).Name);
        }



    }
}
