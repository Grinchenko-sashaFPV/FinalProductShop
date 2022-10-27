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
            throw new NotImplementedException();
        }

        public List<Category> GetCategoriesByPopularityDesc()
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryByName(string categoryName)
        {
            throw new NotImplementedException();
        }
    }
}
