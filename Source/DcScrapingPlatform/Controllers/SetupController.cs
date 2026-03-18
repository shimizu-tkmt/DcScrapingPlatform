using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DcScrapingPlatform.Data;
using DcScrapingPlatform.Models;
using DcScrapingPlatform.Client.Models;

namespace DcScrapingPlatform.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SetupController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public SetupController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet("status")]
    public async Task<ActionResult<SetupStatus>> GetStatus()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var hasProfile = await _context.Profiles.AnyAsync(p => p.UserId == user.Id && !string.IsNullOrEmpty(p.SpreadsheetId));
        var hasCredential = await _context.DcCredentials.AnyAsync(c => c.UserId == user.Id);

        return new SetupStatus
        {
            IsComplete = hasProfile || hasCredential,
            HasProfile = hasProfile,
            HasCredential = hasCredential
        };
    }
}
