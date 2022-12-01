using ModelsLibrary.Models;
using System.Collections.Generic;

namespace ClientWPF.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        List<Category> GetAllCategories();
        List<Category> GetCategoriesByPopularityAsc();
        List<Category> GetCategoriesByPopularityDesc();
        List<Category> GetCategoriesByContaintsLetters(string phrase);
        Category GetCategoryById(int categoryId);
        Category GetCategoryByName(string categoryName);
        int UpdateCategory(Category changedCategory);
        void DeleteCategory(int categoryId);
        void AddCategory(Category newCategory);
    }
}
