using CodePulse.Backend.Data;
using CodePulse.Backend.Models.Domain;
using CodePulse.Backend.Models.DTO;
using CodePulse.Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BlogsController : ControllerBase
{
    private readonly IBlogRepository blogRepository;
    private readonly ICategoryRepository categoryRepository;

    public BlogsController(IBlogRepository blogRepository, ICategoryRepository categoryRepository) {
        this.blogRepository = blogRepository;
        this.categoryRepository = categoryRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlogRequestDto req)
    {
        // map dto to domain
        var blog = new Blog
        {
            Title = req.Title,
            ShortDescription = req.ShortDescription,
            Content = req.Content,
            FeaturedImageUrl = req.FeaturedImageUrl,
            UrlHandle = req.UrlHandle,
            PublishedDate = DateTime.UtcNow.Date,
            Author = req.Author,
            IsVisible = req.IsVisible,
            Categories = new List<Category>(),
        };

        foreach (var categoryId in req.Categories) {
            var category = await categoryRepository.GetCategoryById(categoryId);
            if (category != null) {
                blog.Categories.Add(category);
            }
        }

        await blogRepository.CreateBlog(blog);

        // Domain model to DTO
        var response = new BlogDto
        {
            Id = blog.Id,
            Title = blog.Title,
            ShortDescription = blog.ShortDescription,
            Content = blog.Content,
            FeaturedImageUrl = blog.FeaturedImageUrl,
            UrlHandle = blog.UrlHandle,
            PublishedDate = blog.PublishedDate,
            Author = blog.Author,
            IsVisible = blog.IsVisible,
            Categories = blog.Categories.Select(x => new CategoryDto{
                Id = x.Id,
                Name = x.Name,
                UrlHandle = x.UrlHandle,
            }).ToList(),
        };
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetBlogs()
    {
        var blogs = await blogRepository.GetBlogs();
        // map to dto
        var response = new List<BlogDto>();
        foreach (var blog in blogs)
        {
            response.Add(new BlogDto
            {
                Id = blog.Id,
                Title = blog.Title,
                ShortDescription = blog.ShortDescription,
                Content = blog.Content,
                FeaturedImageUrl = blog.FeaturedImageUrl,
                UrlHandle = blog.UrlHandle,
                PublishedDate = blog.PublishedDate,
                Author = blog.Author,
                IsVisible = blog.IsVisible,
                Categories = blog.Categories.Select(x => new CategoryDto{
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList(),
            });
        }
        return Ok(response);
    }

    [HttpGet]
    [Route("{Id:Guid}")]
    public async Task<IActionResult> GetBlogById([FromRoute] Guid Id)
    {
        var blog = await blogRepository.GetBlogById(Id);
        return Ok(blog);
        if (blog is null)
        {
            return NotFound();
        }
        // map to dto
        var response = new BlogDto
            {
                Id = blog.Id,
                Title = blog.Title,
                ShortDescription = blog.ShortDescription,
                Content = blog.Content,
                FeaturedImageUrl = blog.FeaturedImageUrl,
                UrlHandle = blog.UrlHandle,
                PublishedDate = blog.PublishedDate,
                Author = blog.Author,
                IsVisible = blog.IsVisible,
                Categories = blog.Categories.Select(x => new CategoryDto{
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList(),
            };
        return Ok(response);
    }

    [HttpPut]
    [Route("{Id:Guid}")]
    public async Task<IActionResult> UpdateBlog([FromRoute] Guid Id, [FromBody] UpdateBlogRequestDto req)
    {
        // DTO to Domain model
        var blog = new Blog
        {
            Id = Id,
            Title = req.Title,
            ShortDescription = req.ShortDescription,
            Content = req.Content,
            FeaturedImageUrl = req.FeaturedImageUrl,
            UrlHandle = req.UrlHandle,
            PublishedDate = req.PublishedDate,
            Author = req.Author,
            IsVisible = req.IsVisible,
            Categories = new List<Category>(),
        };

        foreach(var categoryId in req.Categories) {
            var category = await categoryRepository.GetCategoryById(categoryId);
            if (category != null) {
                blog.Categories.Add(category);
            }
        }

        blog = await blogRepository.UpdateBlog(blog);

        if (blog == null)
        {
            return NotFound();
        }

        // Domain model to DTO
        var response = new BlogDto
        {
            Id = blog.Id,
            Title = blog.Title,
            ShortDescription = blog.ShortDescription,
            Content = blog.Content,
            FeaturedImageUrl = blog.FeaturedImageUrl,
            UrlHandle = blog.UrlHandle,
            PublishedDate = blog.PublishedDate,
            Author = blog.Author,
            IsVisible = blog.IsVisible,
            Categories = blog.Categories.Select(x => new CategoryDto{
                Id = x.Id,
                Name = x.Name,
                UrlHandle = x.UrlHandle,
            }).ToList(),
        };
        return Ok(response);
    }

    [HttpDelete]
    [Route("{Id:Guid}")]
    public async Task<IActionResult> DeleteBlog([FromRoute] Guid Id)
    {
        var blog = await blogRepository.DeleteBlog(Id);
        if (blog == null)
        {
            return NotFound();
        }

        var response = new BlogDto
        {
            Id = blog.Id,
            Title = blog.Title,
            ShortDescription = blog.ShortDescription,
            Content = blog.Content,
            FeaturedImageUrl = blog.FeaturedImageUrl,
            UrlHandle = blog.UrlHandle,
            PublishedDate = blog.PublishedDate,
            Author = blog.Author,
            IsVisible = blog.IsVisible,
            Categories = blog.Categories.Select(x => new CategoryDto{
                Id = x.Id,
                Name = x.Name,
                UrlHandle = x.UrlHandle,
            }).ToList(),
        };
        return Ok(response);
    }
}
