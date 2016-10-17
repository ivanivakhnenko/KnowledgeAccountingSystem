using System.Collections.Generic;
using System.Linq;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.DAL.Models;
using KnowledgeAccountingSystem.DAL.Models.Identity;

namespace KnowledgeAccountingSystem.BLL.Util
{
    internal static class Mapper
    {
        public static UserDTO Map(User user)
        {
            UserDTO result = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Skills = Map(user.Skills),
                IdentityId = user.IdentityId,
                Email = user.Email
            };

            return result;
        }

        public static CategorizedUserDTO Map(User user, List<Category> cats)
        {
            CategorizedUserDTO result = new CategorizedUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                IdentityId = user.IdentityId,
                Categories = new List<ValuedCategoryDTO>(),
                Teams = MapLazy(user.Teams),
                Email = user.Email
            };

            foreach (var cat in cats)
            {
                result.Categories.Add(Map(cat));
                foreach (var s in result.Categories.Last().Skills)
                {
                    foreach (var us in user.Skills.Where(us => us.Skill.Id == s.Skill.Id))
                    {
                        s.Value = us.Value;
                        s.Id = us.Id;
                        s.Skill.Name = us.Skill.Name;
                    }
                }
            }

            return result;
        }

        private static ValuedCategoryDTO Map(Category cat)
        {
            ValuedCategoryDTO result = new ValuedCategoryDTO
            {
                Id = cat.Id,
                Name = cat.Name,
                Skills = new List<SkillValueDTO>()
            };

            foreach (var skill in cat.Skills)
            {
                result.Skills.Add(new SkillValueDTO { Skill = Map(skill) });
            }

            return result;
        }

        private static List<SkillValueDTO> Map(List<SkillValue> skills)
        {
            return skills.Select(Map).ToList();
        }

        public static List<UserDTO> Map(List<User> users)
        {
            return users.Select(Map).ToList();
        }

        public static SkillValueDTO Map(SkillValue skillValue)
        {
            SkillValueDTO result = new SkillValueDTO { Id = skillValue.Id, Skill = Map(skillValue.Skill), Value = skillValue.Value };
            return result;
        }

        public static SkillDTO Map(Skill sid)
        {
            SkillDTO result = new SkillDTO
            {
                Id = sid.Id,
                Name = sid.Name,
                Category = MapLazy(sid.Category)
            };
            return result;
        }

        private static CategoryDTO MapLazy(Category category)
        {
            return new CategoryDTO() { Id = category.Id, Name = category.Name };
        }

        public static List<SkillDTO> Map(List<Skill> sids)
        {
            return sids.Select(Map).ToList();
        }

        public static List<SkillValue> Map(List<SkillValueDTO> skills)
        {
            return skills.Select(Map).ToList();
        }

        public static Skill Map(SkillDTO skill)
        {
            return new Skill
            {
                Id = skill.Id,
                Name = skill.Name,
                Category = skill.Category == null ? null : MapLazy(skill.Category)
            };
        }

        private static Category MapLazy(CategoryDTO category)
        {
            return new Category()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static SkillValue Map(SkillValueDTO skillsValue)
        {
            return new SkillValue { Id = skillsValue.Id, Value = skillsValue.Value, Skill = Map(skillsValue.Skill) };
        }

        public static List<CategorizedUserDTO> Map(List<User> toList, List<Category> cats)
        {
            return toList.Select(u => Map(u, cats)).ToList();
        }

        public static List<ValuedCategoryDTO> Map(List<Category> getAll)
        {
            return getAll.Select(Map).ToList();
        }

        public static List<TeamDTO> MapLazy(List<Team> teams)
        {
            return teams.Select(MapLazy).ToList();
        }

        private static TeamDTO MapLazy(Team t)
        {
            return new TeamDTO { Id = t.Id, Name = t.Name };
        }

        public static Team MapLazy(TeamDTO t)
        {
            return new Team
            {
                Id = t.Id,
                Name = t.Name
            };
        }

        public static User Map(UserDTO u)
        {
            return new User
            {
                Id = u.Id,
                Email = u.Email,
                Name = u.Name,
                Skills = Map(u.Skills),
                Teams = MapLazy(u.Teams),
                IdentityId = u.IdentityId
            };
        }

        private static List<Team> MapLazy(List<TeamDTO> teams)
        {
            return teams.Select(MapLazy).ToList();
        }

        public static List<CategoryDTO> MapUnvalued(List<Category> cats)
        {
            return cats.Select(MapUnvalued).ToList();
        }

        private static CategoryDTO MapUnvalued(Category cats)
        {
            var result = new CategoryDTO { Id = cats.Id, Name = cats.Name, Skills = new List<SkillDTO>() };

            foreach (var s in cats.Skills)
            {
                result.Skills.Add(Map(s));
            }

            return result;
        }

        public static Category MapUnvalued(CategoryDTO cat)
        {
            var result = new Category { Id = cat.Id, Name = cat.Name, Skills = new List<Skill>() };

            if (cat.Skills == null || cat.Skills.Count == 0) return result;
            foreach (var s in cat.Skills)
            {
                result.Skills.Add(Map(s));
            }
            return result;
        }

        public static List<UserIdentityDTO> Map(List<ApplicationUser> skillId, List<ApplicationRole> roles)
        {
            return skillId.Select(u => Map(u, roles)).ToList();
        }

        public static UserIdentityDTO Map(ApplicationUser skillId, List<ApplicationRole> roles)
        {
            var dbrole = roles.First(x => x.Id == skillId.Roles.First().RoleId);
            RoleDTO role = new RoleDTO { Name = dbrole.Name, Id = dbrole.Id };

            return new UserIdentityDTO
            {
                Email = skillId.Email,
                Name = skillId.ClientProfile.Name,
                Address = skillId.ClientProfile.Address,
                Role = role,
                UserName = skillId.UserName,
                IdentityId = skillId.Id
            };
        }

        public static List<RoleDTO> Map(List<ApplicationRole> skillId)
        {
            return skillId.Select(Map).ToList();
        }

        private static RoleDTO Map(ApplicationRole skillId)
        {
            return new RoleDTO { Id = skillId.Id, Name = skillId.Name };
        }
    }
}