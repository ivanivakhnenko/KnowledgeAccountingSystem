using System.Collections.Generic;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Util;

namespace KnowledgeAccountingSystem.BLL.Services
{
    public interface ISkillsService
    {
        void AddSkill(SkillDTO skill);
        List<CategoryDTO> GetSkills();
        void RemoveCategory(CategoryDTO category);
        void RemoveSkill(SkillDTO skill);
        OperationDetails RenameCategory(CategoryDTO category);
        void RenameSkill(SkillDTO skill);
    }
}