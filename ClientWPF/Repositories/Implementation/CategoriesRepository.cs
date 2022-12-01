using ModelsLibrary.Models;
using ClientWPF.Repositories.Implementation.Manager;
using ClientWPF.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows;

namespace ClientWPF.Repositories.Implementation
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ModelsManager _dbManager;
        public CategoriesRepository()
        {
            _dbManager = new ModelsManager();
        }

        public void AddCategory(Category newCategory)
        {
            bool isExist = (_dbManager.Categories.Where(c => c.Name == newCategory.Name).FirstOrDefault() != null) ? true : false;
            if (isExist)
                throw new Exception($"Category with name {newCategory.Name} is already existed!");
            else
            {
                _dbManager.Categories.Add(newCategory);
                _dbManager.SaveChanges();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            _dbManager.Categories.Remove(_dbManager.Categories.Find(categoryId));
            _dbManager.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            return _dbManager.Categories.ToList();
        }

        public List<Category> GetCategoriesByContaintsLetters(string phrase)
        {
            return _dbManager.Categories.Where(c => c.Name.Contains(phrase)).ToList();
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

        public int UpdateCategory(Category changedCategory)
        {
            _dbManager.Categories.Find(changedCategory.Id).Name = changedCategory.Name;
            return _dbManager.SaveChanges();
        }
    }
}
