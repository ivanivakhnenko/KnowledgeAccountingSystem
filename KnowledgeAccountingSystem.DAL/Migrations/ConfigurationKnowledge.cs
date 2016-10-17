namespace KnowledgeAccountingSystem.DAL.MigrationsKnowledge
{
    using System.Data.Entity.Migrations;

    internal sealed class ConfigurationKnowledge : DbMigrationsConfiguration<KnowledgeAccountingSystem.DAL.ContextDb.KnowledgeContext>
    {
        public ConfigurationKnowledge()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "KnowledgeAccountingSystem.DAL.ContextDb.KnowledgeContext";
        }

        protected override void Seed(KnowledgeAccountingSystem.DAL.ContextDb.KnowledgeContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}