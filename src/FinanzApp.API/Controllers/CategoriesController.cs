using FinanzApp.Application.DTOs.Category;
using FinanzApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanzApp.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _service.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _service.GetByIdAsync(id);
        return category is null ? NotFound() : Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    private Guid GetUserId() =>
    Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
        ?? throw new UnauthorizedAccessException());
}