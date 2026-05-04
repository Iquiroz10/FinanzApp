using FinanzApp.Application.DTOs.Expense;
using FinanzApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanzApp.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _service;

    public ExpensesController(IExpenseService service)
    {
        _service = service;
    }

     private Guid GetUserId() =>
    Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
        ?? throw new UnauthorizedAccessException());

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var expenses = await _service.GetAllByUserAsync(GetUserId());
        return Ok(expenses);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var expense = await _service.GetByIdAsync(id);
        return expense is null ? NotFound() : Ok(expense);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]ExpenseCreateDto dto)
    {
        var created = await _service.CreateAsync(GetUserId(), dto);
        return CreatedAtAction(nameof(GetById),
            new { userId = GetUserId(), id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody]ExpenseCreateDto dto)
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