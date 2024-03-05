using CodePulse.Backend.Models.Domain;

namespace CodePulse.Backend.Repositories.Interfaces;
public interface ICategoryRepository
{
    Task<Category> CreateCategory(Category category);
    Task<IEnumerable<Category>> GetCategories();
}