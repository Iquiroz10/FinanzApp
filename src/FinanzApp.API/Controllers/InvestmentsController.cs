using FinanzApp.Application.DTOs.Investment;
using FinanzApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanzApp.API.Controllers;

[ApiController]
[Route("api/users/{userId:guid}/[controller]")]
public class InvestmentsController : ControllerBase
{
    private readonly IInvestmentService _service;

    public InvestmentsController(IInvestmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(Guid userId)
    {
        var investments = await _service.GetAllByUserAsync(userId);
        return Ok(investments);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid userId, Guid id)
    {
        var investment = await _service.GetByIdAsync(id);
        return investment is null ? NotFound() : Ok(investment);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid userId, [FromBody]InvestmentCreateDto dto)
    {
        var created = await _service.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetById),
            new { userId, id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid userId, Guid id, [FromBody]InvestmentCreateDto dto)
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