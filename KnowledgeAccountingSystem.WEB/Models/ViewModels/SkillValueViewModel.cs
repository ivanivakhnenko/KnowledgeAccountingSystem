using System;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeAccountingSystem.WEB.Models.ViewModels
{
    [Serializable]
    public class SkillValueViewModel
    {
        public int Id { get; set; }
        public SkillViewModel Skill { get; set; }
        [Range(0, 5)]
        public int Value { get; set; }
    }
}