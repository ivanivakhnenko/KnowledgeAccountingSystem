using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeAccountingSystem.DAL.Models
{
    public class SkillValue
    {
        [Key, Column(Order = 1)]
        public int Id { get; set; }

        public int Skill_Id { get; set; }

        [Key, ForeignKey("Skill_Id"), Column(Order = 2)]
        [Required]
        public virtual Skill Skill { get; set; }

        public int Value { get; set; }
    }
}