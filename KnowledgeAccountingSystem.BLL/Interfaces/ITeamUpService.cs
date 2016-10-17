using System.Collections.Generic;
using KnowledgeAccountingSystem.BLL.DTO;

namespace KnowledgeAccountingSystem.BLL.Services
{
    public interface ITeamUpService
    {
        /// <summary>
        /// Create new team in the database
        /// </summary>
        /// <param name="team"></param>
        void CreateTeam(TeamDTO team);

        /// <summary>
        /// Get all skills, grouped by categories
        /// </summary>
        /// <returns></returns>
        List<ValuedCategoryDTO> GetSkills();

        /// <summary>
        /// Get all teams
        /// </summary>
        /// <returns></returns>
        List<TeamDTO> GetTeams();

        /// <summary>
        /// Get users by minimal skillset
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        List<CategorizedUserDTO> GetUsers(SkillSetDTO ss);

        /// <summary>
        /// Get all users in the specified team
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        List<CategorizedUserDTO> GetUsersByTeam(TeamDTO team);

        void RenameTeam(TeamDTO mapLazy);
        void RemoveTeam(TeamDTO mapLazy);
        void AddTeam(TeamDTO mapLazy);
        void RemoveUserTeam(UserTeamDTO map);
        void AddUserTeam(UserTeamDTO map);
    }
}