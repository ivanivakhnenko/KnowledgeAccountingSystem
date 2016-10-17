using System;
using System.Collections.Generic;

namespace KnowledgeAccountingSystem.WEB.Models.ViewModels
{
    [Serializable]
    public class CategorizedUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string IdentityId { get; set; }
        public virtual List<ValuedCategoryViewModel> Skills { get; set; }
        public virtual List<TeamViewModel> Teams { get; set; }
    }
}