using System;

namespace KnowledgeAccountingSystem.WEB.Models.ViewModels
{
    public class SkillViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}