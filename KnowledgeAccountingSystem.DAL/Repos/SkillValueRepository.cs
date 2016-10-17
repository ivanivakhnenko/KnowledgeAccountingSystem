using System.Collections.Generic;
using System.Data.Entity;
using KnowledgeAccountingSystem.DAL.ContextDb;
using KnowledgeAccountingSystem.DAL.Interfaces;
using KnowledgeAccountingSystem.DAL.Models;

namespace KnowledgeAccountingSystem.DAL.Repos
{
    public class SkillValueRepository : IRepository<SkillValue>
    {
        private readonly KnowledgeContext _db;

        public SkillValueRepository(KnowledgeContext context)
        {
            _db = context;
        }

        public IEnumerable<SkillValue> GetAll()
        {
            return _db.SkillValues.Include(x => x.Skill);
        }//.Select(q => q)

        public SkillValue Get(int id)
        {
            return _db.SkillValues.Find(id);
        }

        public void Create(SkillValue skillValue)
        {
            _db.SkillValues.Add(skillValue);
        }

        public void Update(SkillValue user)
        {
            _db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            SkillValue skillValue = _db.SkillValues.Find(id);
            if (skillValue != null)
                _db.SkillValues.Remove(skillValue);
        }

        public void Save()
        {
            if (_db.ChangeTracker.HasChanges())
                _db.SaveChanges();
        }
    }
}