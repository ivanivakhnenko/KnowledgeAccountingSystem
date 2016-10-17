using System.Collections.Generic;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class ValuedCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<SkillValueDTO> Skills { get; set; }
    }
}