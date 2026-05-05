using FinanzApp.API.Filters;
using FinanzApp.Application.DTOs.Investment;
using FinanzApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanzApp.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InvestmentsController : ControllerBase
{
    private readonly IInvestmentService _service;

    public InvestmentsController(IInvestmentService service)
    {
        _service = service;
    }

    private Guid GetUserId() =>
    Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
        ?? throw new UnauthorizedAccessException());

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var investments = await _service.GetAllByUserAsync(GetUserId());
        return Ok(investments);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var investment = await _service.GetByIdAsync(id);
        return investment is null ? NotFound() : Ok(investment);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter<InvestmentCreateDto>))]

    public async Task<IActionResult> Create([FromBody]InvestmentCreateDto dto)
    {
        var created = await _service.CreateAsync(GetUserId(), dto);
        return CreatedAtAction(nameof(GetById),
            new { userId = GetUserId(), id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilter<InvestmentCreateDto>))]

    public async Task<IActionResult> Update(Guid userId, Guid id, [FromBody]InvestmentCreateDto dto)
    {
      await _service.UpdateAsync(id, dto);
      return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      await _service.DeleteAsync(id);
      return NoContent();        
    }

    
}