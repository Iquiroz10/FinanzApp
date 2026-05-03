using FinanzApp.Application.DTOs.Expense;
using FinanzApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanzApp.API.Controllers;

[ApiController]
[Route("api/users/{userId:guid}/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _service;

    public ExpensesController(IExpenseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(Guid userId)
    {
        var expenses = await _service.GetAllByUserAsync(userId);
        return Ok(expenses);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid userId, Guid id)
    {
        var expense = await _service.GetByIdAsync(id);
        return expense is null ? NotFound() : Ok(expense);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid userId, ExpenseCreateDto dto)
    {
        var created = await _service.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetById),
            new { userId, id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid userId, Guid id, ExpenseCreateDto dto)
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