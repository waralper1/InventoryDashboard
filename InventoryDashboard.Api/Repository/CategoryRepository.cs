﻿using InventoryDashboard.Api.Data;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Repository
{
    public class CategoryRepository : ICategoryInterface
    {
        private DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(p => p.CategoryId == id);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(p => p.CategoryId).ToList();
        }
        public IEnumerable<Product> GetProductsByCategory(int id)
        {
            return _context.Products.Where(p => p.CategoryId == id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(p => p.CategoryId == id).FirstOrDefault();
        }

        public bool CreateCategory(Category category)
        {
            // Notes to self
            //Change Tracker
            //Tracker states: add, updating, modifying
            //connected - disconnected
            //Entity.State.Added 
            //
            _context.Add(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {

            _context.Update(category);
            return Save();
        }


        public bool DeleteCategory(Category category)
        {

            _context.Remove(category);
            return Save();
        }
    }
}
