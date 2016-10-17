using System.Collections.Generic;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class CategorizedUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string IdentityId { get; set; }
        public virtual List<ValuedCategoryDTO> Categories { get; set; }
        public List<TeamDTO> Teams { get; set; }
    }
}