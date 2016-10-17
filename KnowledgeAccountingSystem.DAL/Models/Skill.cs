using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeAccountingSystem.DAL.Models
{
    public class Skill
    {
        [Key, Column(Order = 1)]
        public int Id { get; set; }
        public string Name { get; set; }

        public int Category_Id { get; set; }

        //[Required]
        [Key, ForeignKey("Category_Id"), Column(Order = 1)]
        public virtual Category Category { get; set; }
    }
}