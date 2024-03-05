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
}