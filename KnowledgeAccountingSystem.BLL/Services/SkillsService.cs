using System.Collections.Generic;
using System.Linq;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Util;
using KnowledgeAccountingSystem.DAL.Interfaces;
using KnowledgeAccountingSystem.DAL.Models;

namespace KnowledgeAccountingSystem.BLL.Services
{
    public class SkillsService : ISkillsService
    {
        IUnitOfWork Database { get; }

        public SkillsService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public List<CategoryDTO> GetSkills()
        {
            return Mapper.MapUnvalued(Database.Categories.GetAll().ToList());
        }

        public void RemoveCategory(CategoryDTO category)
        {
            Database.Categories.Delete(category.Id);
            Database.Categories.Save();
        }

        //atomary rename of 1 stuff
        public OperationDetails RenameCategory(CategoryDTO category)
        {
            var dbCategory = Database.Categories.Get(category.Id);
            if (dbCategory == null) throw new UnexpectedException("The category you're trying to rename no longer exists");
            dbCategory.Name = category.Name;
            Database.Categories.Update(dbCategory);
            Database.Categories.Save();

            return new OperationDetails(true, "", "");
        }

        public void AddSkill(SkillDTO skill)
        {
            if (skill.Category.Id != 0)
            {
                var dbCategory = Database.Categories.Get(skill.Category.Id);

                skill.Category = null;
                //the category served its time, now we use it null to correctly inject it th the EF

                dbCategory.Skills.Add(Mapper.Map(skill));

                Database.Categories.Update(dbCategory);
                Database.Categories.Save();
            }
            else
            {
                var cat = Mapper.MapUnvalued(skill.Category);
                skill.Category = null;
                cat.Skills.Add(Mapper.Map(skill));
                Database.Categories.Create(cat);
                Database.Categories.Save();
            }
        }

        public void RemoveSkill(SkillDTO skill)
        {
            Database.Skills.Delete(skill.Id);
            Database.Skills.Save();
        }

        //atomary rename of 1 stuff
        public void RenameSkill(SkillDTO skill)
        {
            var dbSkill = Database.Skills.Get(skill.Id);
            if (dbSkill == null) throw new UnexpectedException("The skill you're trying to rename no longer exists");
            dbSkill.Name = skill.Name;
            Database.Skills.Update(dbSkill);
            Database.Skills.Save();
        }
    }
}
