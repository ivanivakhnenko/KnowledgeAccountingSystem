using System.Collections.Generic;

namespace KnowledgeAccountingSystem.DAL.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Skill> Skills { get; set; }
    }
}