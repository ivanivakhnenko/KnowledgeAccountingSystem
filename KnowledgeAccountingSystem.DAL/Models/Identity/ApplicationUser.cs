using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowledgeAccountingSystem.DAL.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}