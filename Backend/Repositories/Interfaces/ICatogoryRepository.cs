using CodePulse.Backend.Models.Domain;

namespace CodePulse.Backend.Repositories.Interfaces;
public interface ICategoryRepository
{
    Task<Category> CreateCategory(Category category);
    Task<IEnumerable<Category>> GetCategories();
    Task<Category?> GetCategoryById(Guid id);
    Task<Category?> UpdateCategory(Category category);
    Task<Category?> DeleteCategory(Guid id);
}