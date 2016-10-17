using System.Collections.Generic;

namespace KnowledgeAccountingSystem.WEB.Models.ViewModels
{
    public class UserViewModel
    {
        public UserIdViewModel Id { get; set; }
        public virtual List<SkillValueViewModel> Skills { get; set; }
    }
}