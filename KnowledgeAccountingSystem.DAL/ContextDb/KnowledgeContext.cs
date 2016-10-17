using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using KnowledgeAccountingSystem.DAL.Models;

namespace KnowledgeAccountingSystem.DAL.ContextDb
{
    public class KnowledgeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillValue> SkillValues { get; set; }

        public KnowledgeContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new KnowledgeDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().
                HasRequired(x => x.Category).
                WithMany(y => y.Skills).
                WillCascadeOnDelete(true);

            modelBuilder.Entity<SkillValue>().
                HasRequired(x => x.Skill).
                WithMany().
                WillCascadeOnDelete(true);
        }
    }

    public class KnowledgeContextFactory : IDbContextFactory<KnowledgeContext>
    {
        public KnowledgeContext Create()
        {
            return new KnowledgeContext("DefaultConnection");
        }
    }
}