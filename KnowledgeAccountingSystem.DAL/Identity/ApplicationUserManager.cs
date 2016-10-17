using KnowledgeAccountingSystem.DAL.Models.Identity;
using Microsoft.AspNet.Identity;

namespace KnowledgeAccountingSystem.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }
    }
}
