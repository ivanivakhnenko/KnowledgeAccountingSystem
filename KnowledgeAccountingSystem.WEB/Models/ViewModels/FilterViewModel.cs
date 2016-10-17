using System;
using System.Collections.Generic;

namespace KnowledgeAccountingSystem.WEB.Models.ViewModels
{
    [Serializable]
    public class FilterViewModel
    {
        public List<ValuedCategoryViewModel> Categories { get; set; }
        public bool IncludeTeamed { get; set; }
    }
}