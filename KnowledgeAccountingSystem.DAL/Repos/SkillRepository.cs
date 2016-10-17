using System.Collections.Generic;
using System.Data.Entity;
using KnowledgeAccountingSystem.DAL.ContextDb;
using KnowledgeAccountingSystem.DAL.Interfaces;
using KnowledgeAccountingSystem.DAL.Models;

namespace KnowledgeAccountingSystem.DAL.Repos
{
    public class SkillRepository : IRepository<Skill>
    {
        private readonly KnowledgeContext _db;

        public SkillRepository(KnowledgeContext context)
        {
            _db = context;
        }

        public IEnumerable<Skill> GetAll()
        {
            return _db.Skills;
        }

        public Skill Get(int id)
        {
            return _db.Skills.Find(id);
        }

        public void Create(Skill skill)
        {
            _db.Skills.Add(skill);
        }

        public void Update(Skill skill)
        {
            _db.Entry(skill).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Skill sid = _db.Skills.Find(id);
            if (sid != null)
                _db.Skills.Remove(sid);
        }

        public void Save()
        {
            if (_db.ChangeTracker.HasChanges())
                _db.SaveChanges();
        }
    }
}