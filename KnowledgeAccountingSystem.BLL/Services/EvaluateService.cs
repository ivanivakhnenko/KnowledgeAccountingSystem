using System.Collections.Generic;
using System.Linq;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Util;
using KnowledgeAccountingSystem.DAL.Interfaces;
using KnowledgeAccountingSystem.DAL.Models;

namespace KnowledgeAccountingSystem.BLL.Services
{
    public class EvaluateService : IEvaluateService
    {
        IUnitOfWork Database { get; }

        public EvaluateService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// Resets values of user's skills in the database
        /// </summary>
        /// <param name="user"></param>
        public void SetUserSkills(UserDTO user)
        {
            User dbUser = Database.Users.Get(user.Id);

            foreach (var s in user.Skills)
            {
                var analog = dbUser.Skills.Find(x => x.Skill.Name == s.Skill.Name);
                if (analog == null)
                {
                    foreach (var cat in Database.Categories.GetAll().ToList())  //for each db category
                    {
                        foreach (var sk in cat.Skills)                          //for each skill in db category
                        {
                            if (sk.Id == s.Skill.Id)
                            {
                                dbUser.Skills.Add(new SkillValue { Skill = sk, Value = s.Value });
                            }   //if user had no skill like this (id), we add it
                        }
                    }

                }
                else
                {               //if user had this skill before, we update it's value
                    analog.Value = s.Value;
                }
            }

            Database.Users.Update(dbUser);
            Database.Users.Save();
        }

        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserDTO GetUser(int? id)
        {
            if (id == null)
                throw new ValidationException("User id not set", "");

            var user = Database.Users.Get(id.Value);

            if (user == null)
                throw new ValidationException("User not found", "");

            var result = Mapper.Map(user);

            return result;
        }

        /// <summary>
        /// Gets user with categorized skills by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategorizedUserDTO GetCategorizedUser(int? id)
        {
            if (id == null)
                throw new ValidationException("User id not set", "");

            var user = Database.Users.Get(id.Value);

            if (user == null)
                throw new ValidationException("User not found", "");

            var cats = Database.Categories.GetAll();

            CategorizedUserDTO result = Mapper.Map(user, cats.ToList());

            return result;
        }

        /// <summary>
        /// Registers new user by its login parameters from Identity db
        /// </summary>
        /// <param name="identityId"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        public void AddUser(string identityId, string name, string email)
        {
            User newUser = new User
            {
                IdentityId = identityId,
                Name = name,
                Skills = new List<SkillValue>(),
                Teams = new List<Team>(),
                Email = email
            };


            Database.Users.Create(newUser);
            Database.Users.Save();
        }

        /// <summary>
        /// Gets user with structured skills by its Identity id (string)
        /// </summary>
        /// <param name="id">Identity id</param>
        /// <returns></returns>
        public CategorizedUserDTO GetCategorizedUser(string id)
        {
            var user = Database.Users.GetAll().First(x => x.IdentityId == id);

            var cats = Database.Categories.GetAll();

            CategorizedUserDTO result = Mapper.Map(user, cats.ToList());

            return result;
        }
    }
}