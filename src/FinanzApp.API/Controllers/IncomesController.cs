using FinanzApp.Application.DTOs.Income;
using FinanzApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanzApp.API.Controllers;

[ApiController]
[Route("api/users/{userId:guid}/[controller]")]
public class IncomesController : ControllerBase
{
    private readonly IIncomeService _service;

    public IncomesController(IIncomeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(Guid userId)
    {
        var incomes = await _service.GetAllByUserAsync(userId);
        return Ok(incomes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid userId, Guid id)
    {
        var income = await _service.GetByIdAsync(id);
        return income is null ? NotFound() : Ok(income);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid userId, [FromBody]IncomeCreateDto dto)
    {
        var created = await _service.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetById),
            new { userId, id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid userId, Guid id, [FromBody]IncomeCreateDto dto)
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
    public async Task<IActionResult> Delete(Guid userId, Guid id)
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