using ModelsLibrary.Models;
using ClientWPF.Repositories.Implementation.Manager;
using ClientWPF.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ClientWPF.Repositories.Implementation
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ModelsManager _dbManager;
        public CategoriesRepository()
        {
            _dbManager = new ModelsManager();
        }

        public List<Category> GetAllCategories()
        {
            return _dbManager.Categories.ToList();
        }

        public List<Category> GetCategoriesByPopularityAsc()
        {
            return _dbManager.Categories.ThenBy(p => p.Popularity).ToList();
        }

        public List<Category> GetCategoriesByPopularityDesc()
        {
            return _dbManager.Categories.ThenByDescending(p => p.Popularity).ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _dbManager.Categories.Find(categoryId);
        }

        public Category GetCategoryByName(string categoryName)
        {
            return _dbManager.Categories.Where(p => p.Name == categoryName).FirstOrDefault();
        }
    }
}
