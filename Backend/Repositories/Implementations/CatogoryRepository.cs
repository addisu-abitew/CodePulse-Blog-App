using CodePulse.Backend.Data;
using CodePulse.Backend.Models.Domain;
using CodePulse.Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.Backend.Repositories.Implementations;

public class CatogoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext dbContext;

    public CatogoryRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Category> CreateCategory(Category category)
    {
        await dbContext.Catogories.AddAsync(category);
        await dbContext.SaveChangesAsync();
        return category;
    }


    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await dbContext.Catogories.ToListAsync();
    }

    public async Task<Category?> GetCategoryById(Guid Id)
    {
        return await dbContext.Catogories.FindAsync(Id);
    }
    public async Task<Category?> UpdateCategory(Category category)
    {
        var oldCategory = await dbContext.Catogories.FindAsync(category.Id);
        if (oldCategory != null)
        {
            dbContext.Entry(oldCategory).CurrentValues.SetValues(category);
            await dbContext.SaveChangesAsync();
            return category;
        }
        return null;
    }
    public async Task<Category?> DeleteCategory(Guid id)
    {

        var category = await GetCategoryById(id);
        if (category != null)
        {
            dbContext.Catogories.Remove(category);
            await dbContext.SaveChangesAsync();
            return category;
        }
        return null;
    }
}