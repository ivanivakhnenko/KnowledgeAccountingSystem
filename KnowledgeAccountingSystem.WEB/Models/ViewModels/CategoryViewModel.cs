using System.Collections.Generic;

namespace KnowledgeAccountingSystem.WEB.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<SkillViewModel> Skills { get; set; }
    }
}