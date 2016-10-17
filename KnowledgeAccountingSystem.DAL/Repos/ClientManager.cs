using KnowledgeAccountingSystem.DAL.ContextDb;
using KnowledgeAccountingSystem.DAL.Interfaces;
using KnowledgeAccountingSystem.DAL.Models.Identity;

namespace KnowledgeAccountingSystem.DAL.Repos
{
    public class ClientManager : IClientManager
    {
        public IdentityContext Database { get; set; }
        public ClientManager(IdentityContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}