using CodePulse.Backend.Models.Domain;

namespace CodePulse.Backend.Repositories.Interfaces;
public interface IBlogRepository
{
    Task<Blog> CreateBlog(Blog blog);
    Task<IEnumerable<Blog>> GetBlogs();
    Task<Blog?> GetBlogById(Guid id);
    Task<Blog?> UpdateBlog(Blog blog);
    Task<Blog?> DeleteBlog(Guid id);
}