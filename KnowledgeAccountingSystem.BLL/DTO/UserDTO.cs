using System.Collections.Generic;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual List<SkillValueDTO> Skills { get; set; }
        public virtual List<TeamDTO> Teams { get; set; }
        public string IdentityId { get; set; }
    }
}
