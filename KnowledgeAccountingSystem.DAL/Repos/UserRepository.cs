using System.Collections.Generic;
using System.Data.Entity;
using KnowledgeAccountingSystem.DAL.ContextDb;
using KnowledgeAccountingSystem.DAL.Interfaces;
using KnowledgeAccountingSystem.DAL.Models;

namespace KnowledgeAccountingSystem.DAL.Repos
{
    public class UserRepository : IRepository<User>
    {
        private readonly KnowledgeContext _db;

        public UserRepository(KnowledgeContext context)
        {
            _db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users.Include(x => x.Skills);
            //.Select(s => s.SkillId)
        }

        public User Get(int id)
        {
            return _db.Users.Find(id);
        }

        public void Create(User user)
        {
            _db.Users.Add(user);
        }

        public void Update(User user)
        {
            //var original = Get(user.Id);

            //db.Entry(original).CurrentValues.SetValues(user);

            //db.Entry(original).State = EntityState.Modified;

            foreach (var s in user.Skills)
            {
                _db.Entry(s.Skill).State = EntityState.Unchanged;
            }

            _db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = _db.Users.Find(id);
            if (user != null)
                _db.Users.Remove(user);
        }

        public void Save()
        {
            if (_db.ChangeTracker.HasChanges())
                _db.SaveChanges();
        }
    }
}