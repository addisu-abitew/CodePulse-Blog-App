using CodePulse.Backend.Data;
using CodePulse.Backend.Models.Domain;
using CodePulse.Backend.Models.DTO;
using CodePulse.Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository categoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository) => this.categoryRepository = categoryRepository;

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto req)
    {
        // map dto to domain
        var category = new Category
        {
            Name = req.Name,
            UrlHandle = req.UrlHandle,
        };

        await categoryRepository.CreateCategory(category);

        // Domain model to DTO
        var response = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            UrlHandle = category.UrlHandle,
        };
        return Ok(response);
    }
}