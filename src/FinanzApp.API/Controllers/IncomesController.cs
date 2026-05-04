using FinanzApp.Application.DTOs.Income;
using FinanzApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanzApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomesController : ControllerBase
{
    private readonly IIncomeService _service;

    public IncomesController(IIncomeService service)
    {
        _service = service;
    }

    private Guid GetUserId() =>
    Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
        ?? throw new UnauthorizedAccessException());

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var incomes = await _service.GetAllByUserAsync(GetUserId());
        return Ok(incomes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var income = await _service.GetByIdAsync(id);
        return income is null ? NotFound() : Ok(income);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]IncomeCreateDto dto)
    {
        var created = await _service.CreateAsync(GetUserId(), dto);
        return CreatedAtAction(nameof(GetById),
            new { userId = GetUserId(), id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody]IncomeCreateDto dto)
    {
        try
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    
}