using System;
using KnowledgeAccountingSystem.DAL.Models;

namespace KnowledgeAccountingSystem.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Category> Categories { get; }
        IRepository<Team> Teams { get; }
        IRepository<Skill> Skills { get; }
        IRepository<SkillValue> SkillValues { get; }

        void Save();
    }
}