using System.Data.Entity;

namespace KnowledgeAccountingSystem.DAL.ContextDb
{
    public class KnowledgeDbInitializer : DropCreateDatabaseIfModelChanges<KnowledgeContext>
    {
        protected override void Seed(KnowledgeContext db)
        {
            //there was a seed. I promise you. There was.
        }
    }
}