using System.Collections.Generic;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class SkillSetDTO
    {
        public List<SkillValueDTO> Skills { get; set; }
        public bool IncludeTeamed { get; set; }
    }
}