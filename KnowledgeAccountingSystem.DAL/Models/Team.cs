using System.Collections.Generic;

namespace KnowledgeAccountingSystem.DAL.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; } 
    }
}