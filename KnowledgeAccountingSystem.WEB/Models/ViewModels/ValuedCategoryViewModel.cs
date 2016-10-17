using System;
using System.Collections.Generic;

namespace KnowledgeAccountingSystem.WEB.Models.ViewModels
{
    [Serializable]
    public class ValuedCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<SkillValueViewModel> Skills { get; set; }

    }
}