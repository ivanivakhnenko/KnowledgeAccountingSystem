using System.Collections.Generic;

namespace KnowledgeAccountingSystem.WEB.Models.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<UserViewModel> Users { get; set; }
    }
}