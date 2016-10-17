namespace KnowledgeAccountingSystem.DAL.MigrationsIdentity
{
    using System.Data.Entity.Migrations;

    internal sealed class ConfigurationIdentity : DbMigrationsConfiguration<KnowledgeAccountingSystem.DAL.ContextDb.IdentityContext>
    {
        public ConfigurationIdentity()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "KnowledgeAccountingSystem.DAL.ContextDb.IdentityContext";
        }

        protected override void Seed(KnowledgeAccountingSystem.DAL.ContextDb.IdentityContext context)
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
