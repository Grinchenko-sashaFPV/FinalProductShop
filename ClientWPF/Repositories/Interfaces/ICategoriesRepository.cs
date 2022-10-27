using ModelsLibrary.Models;
using System.Collections.Generic;

namespace ClientWPF.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        List<Category> GetAllCategories();
        List<Category> GetCategoriesByPopularityAsc();
        List<Category> GetCategoriesByPopularityDesc();
        Category GetCategoryById(int categoryId);
        Category GetCategoryByName(string categoryName);
    }
}
