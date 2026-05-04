using FinanzApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanzApp.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new UnauthorizedAccessException());

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var result = await _dashboardService.GetSummaryAsync(GetUserId());
        return Ok(result);
    }

    [HttpGet("debts-overview")]
    public async Task<IActionResult> GetDebtOverview()
    {
        var result = await _dashboardService.GetDebtOverviewAsync(GetUserId());
        return Ok(result);
    }

    [HttpGet("expense-analysis")]
    public async Task<IActionResult> GetExpenseAnalysis()
    {
        var result = await _dashboardService.GetExpenseAnalysisAsync(GetUserId());
        return Ok(result);
    }

    [HttpGet("available-balance")]
    public async Task<IActionResult> GetAvailableBalance()
    {
        var result = await _dashboardService.GetAvailableBalanceAsync(GetUserId());
        return Ok(result);
    }
}