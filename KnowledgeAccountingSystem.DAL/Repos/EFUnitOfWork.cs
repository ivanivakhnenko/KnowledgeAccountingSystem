using System;
using KnowledgeAccountingSystem.DAL.ContextDb;
using KnowledgeAccountingSystem.DAL.Interfaces;
using KnowledgeAccountingSystem.DAL.Models;

namespace KnowledgeAccountingSystem.DAL.Repos
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly KnowledgeContext _db;

        private CategoryRepository _categoryRepository;
        private UserRepository _userRepository;
        private TeamRepository _teamRepository;
        private SkillRepository _skillRepository;
        private SkillValueRepository _skillValueRepository;

        public EFUnitOfWork(string connectionString)
        {
            _db = new KnowledgeContext(connectionString);
        }

        public IRepository<Category> Categories => 
            _categoryRepository ?? (_categoryRepository = new CategoryRepository(_db));

        public IRepository<User> Users => 
            _userRepository ?? (_userRepository = new UserRepository(_db));

        public IRepository<Team> Teams => 
            _teamRepository ?? (_teamRepository = new TeamRepository(_db));

        public IRepository<Skill> Skills => 
            _skillRepository ?? (_skillRepository = new SkillRepository(_db));

        public IRepository<SkillValue> SkillValues => 
            _skillValueRepository ?? (_skillValueRepository = new SkillValueRepository(_db));

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}