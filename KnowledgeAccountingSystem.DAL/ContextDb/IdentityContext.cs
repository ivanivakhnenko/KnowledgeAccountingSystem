using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using KnowledgeAccountingSystem.DAL.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowledgeAccountingSystem.DAL.ContextDb
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }

    public class IdentityContextFactory : IDbContextFactory<IdentityContext>
    {
        public IdentityContext Create()
        {
            return new IdentityContext("DefaultConnection");
        }
    }
}