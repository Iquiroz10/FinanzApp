using FinanzApp.API.Filters;
using FinanzApp.Application.DTOs.Debt;
using FinanzApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanzApp.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DebtsController : ControllerBase
{
    private readonly IDebtService _service;

    public DebtsController(IDebtService service)
    {
        _service = service;
    }

    private Guid GetUserId() =>
    Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
        ?? throw new UnauthorizedAccessException());

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var debts = await _service.GetAllByUserAsync(GetUserId());
        return Ok(debts);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var debt = await _service.GetByIdAsync(id);
        return debt is null ? NotFound() : Ok(debt);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter<DebtCreateDto>))]
    public async Task<IActionResult> Create([FromBody]DebtCreateDto dto)
    {
        var created = await _service.CreateAsync(GetUserId(), dto);
        return CreatedAtAction(nameof(GetById),
            new { userId = GetUserId(), id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilter<DebtCreateDto>))]
    public async Task<IActionResult> Update(Guid id, [FromBody]DebtCreateDto dto)
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