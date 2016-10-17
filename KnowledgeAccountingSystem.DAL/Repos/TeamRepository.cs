using System.Collections.Generic;
using System.Data.Entity;
using KnowledgeAccountingSystem.DAL.ContextDb;
using KnowledgeAccountingSystem.DAL.Interfaces;
using KnowledgeAccountingSystem.DAL.Models;

namespace KnowledgeAccountingSystem.DAL.Repos
{
    public class TeamRepository : IRepository<Team>
    {
        private readonly KnowledgeContext _db;

        public TeamRepository(KnowledgeContext context)
        {
            _db = context;
        }

        public IEnumerable<Team> GetAll()
        {
            return _db.Teams;
        }

        public Team Get(int id)
        {
            return _db.Teams.Find(id);
        }

        public void Create(Team team)
        {
            _db.Teams.Add(team);
        }

        public void Update(Team team)
        {
            _db.Entry(team).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Team team = _db.Teams.Find(id);
            if (team != null)
                _db.Teams.Remove(team);
        }

        public void Save()
        {
            if (_db.ChangeTracker.HasChanges())
                _db.SaveChanges();
        }
    }
}