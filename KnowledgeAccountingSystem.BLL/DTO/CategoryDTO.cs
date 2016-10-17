using System.Collections.Generic;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<SkillDTO> Skills { get; set; }
    }
}