using System.Collections.Generic;
using System.Linq;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.WEB.Models.ViewModels;

namespace KnowledgeAccountingSystem.WEB.Controllers.Mappers
{
    public static class Mapper
    {
        public static UserViewModel Map(UserDTO user)
        {
            List<SkillValueViewModel> skills = user.Skills.Select(Map).ToList();

            var result = new UserViewModel
            {
                Id = new UserIdViewModel { Id = user.Id, Name = user.Name, IdentityId = user.IdentityId, Email = user.Email },
                Skills = skills
            };

            return result;
        }

        public static List<UserViewModel> Map(List<UserDTO> users)
        {
            return users.Select(Map).ToList();
        }

        public static SkillValueViewModel Map(SkillValueDTO s)
        {
            return new SkillValueViewModel
            {
                Id = s.Id,
                Skill = new SkillViewModel
                {
                    Id = s.Skill.Id,
                    Name = s.Skill.Name,
                    Category = s.Category == null ? null : Map(s.Category)
                },
                Value = s.Value
            };
        }

        public static CategorizedUserViewModel Map(CategorizedUserDTO u)
        {
            return new CategorizedUserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Skills = Map(u.Categories),
                Teams = MapLazy(u.Teams),
                IdentityId = u.IdentityId,
                Email = u.Email
            };
        }

        public static ValuedCategoryViewModel Map(ValuedCategoryDTO vc)
        {
            ValuedCategoryViewModel result = new ValuedCategoryViewModel
            {
                Id = vc.Id,
                Name = vc.Name,
                Skills = Map(vc.Skills)
            };

            return result;
        }

        public static List<SkillValueViewModel> Map(List<SkillValueDTO> ss)
        {
            return ss.Select(Map).ToList();
        }

        public static UserDTO Map(CategorizedUserViewModel users)
        {
            var result = new UserDTO
            {
                Id = users.Id,
                Name = users.Name,
                IdentityId = users.IdentityId,
                Skills = new List<SkillValueDTO>(),
                Teams = MapLazy(users.Teams),
                Email = users.Email
            };

            foreach (var cat in users.Skills)
            {
                foreach (var s in cat.Skills)
                {
                    result.Skills.Add(new SkillValueDTO
                    {
                        Id = s.Id,
                        Skill = new SkillDTO
                        {
                            Id = s.Skill.Id,
                            Name = s.Skill.Name
                        },
                        Value = s.Value
                    });
                }
            }

            return result;
        }

        public static SkillSetDTO Map(FilterViewModel skills)
        {
            var result = new SkillSetDTO { Skills = new List<SkillValueDTO>(), IncludeTeamed = skills.IncludeTeamed };

            foreach (var cat in skills.Categories)
            {
                foreach (var s in cat.Skills)
                {
                    result.Skills.Add(Map(s));
                }
            }

            return result;
        }

        public static SkillValueDTO Map(SkillValueViewModel skillValue)
        {
            return new SkillValueDTO { Id = skillValue.Id, Value = skillValue.Value, Skill = Map(skillValue.Skill) };
        }

        public static SkillDTO Map(SkillViewModel skill)
        {
            return new SkillDTO { Id = skill.Id, Name = skill.Name, Category = skill.Category == null ? null : Map(skill.Category) };
        }

        public static List<CategorizedUserViewModel> Map(List<CategorizedUserDTO> getUsers)
        {
            return getUsers.Select(Map).ToList();
        }

        public static List<ValuedCategoryViewModel> Map(List<ValuedCategoryDTO> getSkills)
        {
            return getSkills.Select(Map).ToList();
        }

        public static List<TeamViewModel> MapLazy(List<TeamDTO> teams)
        {
            return teams.Select(MapLazy).ToList();
        }

        public static TeamViewModel MapLazy(TeamDTO team)
        {
            return new TeamViewModel
            {
                Id = team.Id,
                Name = team.Name
            };
        }

        public static List<TeamDTO> MapLazy(List<TeamViewModel> getSkills)
        {
            List<TeamDTO> result = new List<TeamDTO>();

            if (getSkills != null && getSkills.Count != 0)
            {
                result.AddRange(from s in getSkills where s != null && s.Name != "" select MapLazy(s));
            }

            return result;
        }

        public static TeamDTO MapLazy(TeamViewModel skill)
        {
            return new TeamDTO { Id = skill.Id, Name = skill.Name };
        }

        public static List<UserDTO> MapLazy(List<UserViewModel> getSkills)
        {
            return getSkills.Select(MapLazy).ToList();
        }

        public static UserDTO MapLazy(UserViewModel user)
        {
            return new UserDTO { Id = user.Id.Id, Name = user.Id.Name, IdentityId = user.Id.IdentityId, Email = user.Id.Email };
        }

        public static List<CategoryViewModel> Map(List<CategoryDTO> getSkills)
        {
            return getSkills.Select(Map).ToList();
        }

        private static CategoryViewModel Map(CategoryDTO cat)
        {
            var result = new CategoryViewModel
            {
                Id = cat.Id,
                Name = cat.Name,
                Skills = new List<SkillViewModel>()
            };

            if (cat.Skills == null) return result;

            foreach (var s in cat.Skills)
            {
                result.Skills.Add(Map(s));
            }

            return result;
        }

        private static SkillViewModel Map(SkillDTO s)
        {
            return new SkillViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Category = s.Category == null ? null : Map(s.Category)
            };
        }

        public static List<CategoryViewModel> MapUnvalued(List<CategoryDTO> getSkills)
        {
            return getSkills.Select(MapUnvalued).ToList();
        }

        private static CategoryViewModel MapUnvalued(CategoryDTO cat)
        {
            CategoryViewModel result = new CategoryViewModel { Id = cat.Id, Skills = new List<SkillViewModel>(), Name = cat.Name };

            foreach (var s in cat.Skills)
            {
                result.Skills.Add(Map(s));
            }

            return result;
        }

        public static List<UserIdViewModel> Map(List<UserIdentityDTO> getSkills)
        {
            return getSkills.Select(Map).ToList();
        }

        public static UserIdViewModel Map(UserIdentityDTO user)
        {
            return new UserIdViewModel { Role = Map(user.Role), Name = user.Name, IdentityId = user.IdentityId };
        }

        public static RoleViewModel Map(RoleDTO role)
        {
            return new RoleViewModel { Id = role.Id, Name = role.Name };
        }

        public static List<RoleViewModel> Map(List<RoleDTO> getSkills)
        {
            return getSkills.Select(Map).ToList();
        }

        public static UserIdentityDTO Map(UserIdViewModel user)
        {
            return new UserIdentityDTO
            {
                Role = new RoleDTO { Id = user.Role.Id, Name = user.Role.Name },
                IdentityId = user.IdentityId,
                Name = user.Name
            };
        }

        public static FilterViewModel MapFilter(List<ValuedCategoryDTO> getSkills)
        {
            FilterViewModel result = new FilterViewModel { IncludeTeamed = false, Categories = new List<ValuedCategoryViewModel>() };

            foreach (var s in getSkills)
            {
                result.Categories.Add(Map(s));
            }

            return result;
        }

        public static List<CategoryDTO> Map(List<CategoryViewModel> getSkills)
        {
            return getSkills.Select(Map).ToList();
        }

        public static CategoryDTO Map(CategoryViewModel cat)
        {
            return new CategoryDTO()
            {
                Id = cat.Id,
                Name = cat.Name,
                Skills = cat.Skills == null ? null : Map(cat.Skills)
            };
        }

        public static List<SkillDTO> Map(List<SkillViewModel> getSkills)
        {
            return getSkills.Select(Map).ToList();
        }

        public static UserTeamDTO Map(UserTeamViewModel skillValue)
        {
            return new UserTeamDTO() { TeamId = skillValue.TeamId, UserId = skillValue.UserId };
        }
    }
}