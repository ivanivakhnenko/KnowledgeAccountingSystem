using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using KnowledgeAccountingSystem.DAL.Interfaces;
using KnowledgeAccountingSystem.DAL.Models;
using Moq;

namespace KnowledgeAccountingSystem.BLL.Services.Tests
{
    [TestFixture()]
    public class SkillsServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IRepository<Skill>> _skills;
        private Mock<IRepository<Category>> _categories;

        [SetUp]
        public void TestInit()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _skills = new Mock<IRepository<Skill>>();

            List<Skill> langs = new List<Skill>
            { new Skill() {Id = 1, Name = "C#"}, new Skill() {Id = 2, Name = "Java"}};

            List<Skill> dbs = new List<Skill>
            { new Skill() {Id = 3, Name = "MSSQL Server"}, new Skill() {Id = 4, Name = "Oracle"}};

            List<Category> cats = new List<Category>
            { new Category() {Id = 1, Name = "Languages", Skills = langs},
                new Category() {Id = 2, Name = "Databases", Skills = dbs} };

            var allSkills = langs.Concat(dbs);

            _categories = new Mock<IRepository<Category>>();
            _categories.Setup(x => x.GetAll()).Returns(cats.AsQueryable());

            _skills.Setup(x => x.GetAll()).Returns(allSkills.AsQueryable());

            _unitOfWork.Setup(x => x.Skills).Returns(_skills.Object);
            _unitOfWork.Setup(x => x.Categories).Returns(_categories.Object);
        }

        /*That's a metatest of the mock. To be honest, none of the business logic is really testable
         It just calls db functions every now and then. But I had to create a test project.*/

        [Test()]
        public void GetSkillsTest()
        {
            //Arrange
            SkillsService service = new SkillsService(_unitOfWork.Object);

            //Act
            var result = service.GetSkills();

            //Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[1].Name, Is.EqualTo("Databases"));
            Assert.That(result[1].Skills[1].Name, Is.EqualTo("Oracle"));
        }
    }
}