using CommitMaster.Hera.Core.Entities;
using CommitMaster.Hera.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommitMaster.Hera.Api.Controllers;

[Authorize(Roles = "AssinaturaValida")]
[ApiController]
[Route("api/v1/Module")]
public class ModuleController : ControllerBase
{
    private readonly MyAppContext _context;

    public ModuleController(MyAppContext context)
    {
        _context = context;
    }

    [HttpGet()]
    public async Task<ActionResult<List<Module>>> GetAllModule()
    {
        var result = await _context.Modules.AsNoTracking().ToListAsync();

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Module>> GetModuleById(Guid id)
    {
        var result = await _context.Modules
                .Where(c => c.Id == id)
                .AsNoTracking().FirstOrDefaultAsync();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}


