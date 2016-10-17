using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Util;
using KnowledgeAccountingSystem.DAL.Interfaces;

namespace KnowledgeAccountingSystem.BLL.Services
{
    public class TeamUpService : ITeamUpService
    {
        IUnitOfWork Database { get; }

        public TeamUpService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void CreateTeam(TeamDTO team)
        {
            Database.Teams.Create(Mapper.MapLazy(team));
            Database.Teams.Save();
        }

        public void RemoveUserTeam(UserTeamDTO userTeam)
        {
            var dbUser = Database.Users.Get(userTeam.UserId);
            dbUser.Teams.Remove(dbUser.Teams.First(x => x.Id == userTeam.TeamId));
            Database.Users.Update(dbUser);
            Database.Users.Save();
        }

        public void AddUserTeam(UserTeamDTO userTeam)
        {
            var dbUser = Database.Users.Get(userTeam.UserId);
            var dbTeam = Database.Teams.Get(userTeam.TeamId);
            dbUser.Teams.Add(dbTeam);
            Database.Users.Update(dbUser);
            Database.Users.Save();
        }

        public void RenameTeam(TeamDTO team)
        {
            var dbTeam = Database.Teams.Get(team.Id);
            if (dbTeam == null) throw new UnexpectedException("The team you're trying to rename no longer exists");
            dbTeam.Name = team.Name;
            Database.Teams.Update(dbTeam);
            Database.Teams.Save();
        }

        public void RemoveTeam(TeamDTO team)
        {
            Database.Teams.Delete(team.Id);
            Database.Teams.Save();
        }

        public void AddTeam(TeamDTO team)
        {
            Database.Teams.Create(Mapper.MapLazy(team));
            Database.Teams.Save();
        }

        /// <summary>
        /// Gets all the available teams
        /// </summary>
        /// <returns></returns>
        public List<TeamDTO> GetTeams()
        {
            var result = Database.Teams.GetAll();
            return Mapper.MapLazy(result.ToList());
        }

        /// <summary>
        /// Gets all users with categorized skills by target filter.
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        public List<CategorizedUserDTO> GetUsers(SkillSetDTO ss)
        {
            var result = from u in Database.Users.GetAll()
                         where ss.Skills.All(query => query.Value == 0 || u.Skills.Any(y => y.Skill.Id == query.Skill.Id && y.Value >= query.Value))
                         select u;

            if (!ss.IncludeTeamed)
                result = result.Where(x => x.Teams.Count == 0);

            if (result.Count() == 0)
                throw new ValidationException("Query yilded no results.", "");

            var cats = Database.Categories.GetAll().ToList();

            return Mapper.Map(result.ToList(), cats);
        }

        /// <summary>
        /// Gets all skills, grouped by categories
        /// </summary>
        /// <returns></returns>
        public List<ValuedCategoryDTO> GetSkills()
        {
            return Mapper.Map(Database.Categories.GetAll().ToList());
        }

        /// <summary>
        /// Gets all users by a specified team
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public List<CategorizedUserDTO> GetUsersByTeam(TeamDTO team)
        {
            var users = Database.Users.GetAll().Where(x => x.Teams.Any(t => t.Id == team.Id)).ToList();

            var cats = Database.Categories.GetAll().ToList();

            return Mapper.Map(users, cats);
        }

    }

    public class UnexpectedException : Exception
    {
        public UnexpectedException(string message) : base(message)
        {
        }
    }
}