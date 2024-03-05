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
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto req)
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

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await categoryRepository.GetCategories();
        // map to dto
        var response = new List<CategoryDto>();
        foreach (var category in categories)
        {
            response.Add(new CategoryDto { Id = category.Id, Name = category.Name, UrlHandle = category.UrlHandle });
        }
        return Ok(response);
    }

    [HttpGet]
    [Route("{Id:Guid}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] Guid Id)
    {
        var category = await categoryRepository.GetCategoryById(Id);
        if (category is null)
        {
            return NotFound();
        }
        // map to dto
        var response = new CategoryDto { Id = category.Id, Name = category.Name, UrlHandle = category.UrlHandle };
        return Ok(response);
    }

    [HttpPut]
    [Route("{Id:Guid}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] Guid Id, [FromBody] UpdateCategoryRequestDto req)
    {
        // DTO to Domain model
        var category = new Category { Id = Id, Name = req.Name, UrlHandle = req.UrlHandle };

        category = await categoryRepository.UpdateCategory(category);

        if (category == null)
        {
            return NotFound();
        }

        // Domain model to DTO
        var response = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            UrlHandle = category.UrlHandle,
        };
        return Ok(response);
    }

    [HttpDelete]
    [Route("{Id:Guid}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid Id)
    {
        var result = await categoryRepository.DeleteCategory(Id);
        if (result == null)
        {
            return NotFound();
        }

        var response = new CategoryDto
        {
            Id = Id,
            Name = result.Name,
            UrlHandle = result.UrlHandle,
        };
        return Ok(response);
    }
}
