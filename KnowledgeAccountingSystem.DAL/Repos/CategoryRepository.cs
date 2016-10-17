using System.Collections.Generic;
using System.Data.Entity;
using KnowledgeAccountingSystem.DAL.ContextDb;
using KnowledgeAccountingSystem.DAL.Interfaces;
using KnowledgeAccountingSystem.DAL.Models;

namespace KnowledgeAccountingSystem.DAL.Repos
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly KnowledgeContext _db;

        public CategoryRepository(KnowledgeContext context)
        {
            _db = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Categories;
        }

        public Category Get(int id)
        {
            return _db.Categories.Find(id);
        }

        public void Create(Category book)
        {
            _db.Categories.Add(book);
        }

        public void Update(Category book)
        {
            _db.Entry(book).State = EntityState.Modified;
        }

        public void Save()
        {
             if (_db.ChangeTracker.HasChanges())
                    _db.SaveChanges();   
        }

        public void Delete(int id)
        {
            Category cat = _db.Categories.Find(id);
            if (cat != null)
                _db.Categories.Remove(cat);
        }
    }
}