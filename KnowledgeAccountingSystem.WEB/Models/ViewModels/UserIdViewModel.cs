using System;

namespace KnowledgeAccountingSystem.WEB.Models.ViewModels
{
    [Serializable]
    public class UserIdViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentityId { get; set; }
        public string Email { get; set; }
        public RoleViewModel Role { get; set; }
    }
}