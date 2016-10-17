using System.Collections.Generic;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<UserDTO> Users { get; set; }
    }
}