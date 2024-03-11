using CodePulse.Backend.Data;
using CodePulse.Backend.Models.Domain;
using CodePulse.Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.Backend.Repositories.Implementations;

public class BlogRepository : IBlogRepository
{
    private readonly ApplicationDbContext dbContext;

    public BlogRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Blog> CreateBlog(Blog blog)
    {
        await dbContext.Blogs.AddAsync(blog);
        await dbContext.SaveChangesAsync();
        return blog;
    }


    public async Task<IEnumerable<Blog>> GetBlogs()
    {
        return await dbContext.Blogs.Include(x => x.Categories).ToListAsync();
    }

    public async Task<Blog?> GetBlogById(Guid Id)
    {
        // return await dbContext.Blogs.FindAsync(Id);
        return await dbContext.Blogs.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == Id);
    }
    public async Task<Blog?> UpdateBlog(Blog blog)
    {
        var oldBlog = await GetBlogById(blog.Id);
        if (oldBlog != null)
        {
            dbContext.Entry(oldBlog).CurrentValues.SetValues(blog);
            await dbContext.SaveChangesAsync();
            return blog;
        }
        return null;
    }
    public async Task<Blog?> DeleteBlog(Guid id)
    {

        var blog = await GetBlogById(id);
        if (blog != null)
        {
            dbContext.Blogs.Remove(blog);
            await dbContext.SaveChangesAsync();
            return blog;
        }
        return null;
    }
}