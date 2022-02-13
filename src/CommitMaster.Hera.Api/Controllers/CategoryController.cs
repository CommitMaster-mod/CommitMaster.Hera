using CommitMaster.Hera.Core.Entities;
using CommitMaster.Hera.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommitMaster.Hera.Api.Controllers;

[Authorize(Roles = "AssinaturaValida")]
[ApiController]
[Route("api/v1/categoria")]
public class CategoryController : ControllerBase
{
    private readonly MyAppContext _context;

    public CategoryController(MyAppContext context)
    {
        _context = context;
    }


    [HttpGet()]
    public async Task<ActionResult<List<Category>>> GetAllCategories()
    {
        var result = await _context.Categories.AsNoTracking().ToListAsync();
        
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Category>> GetCategoryById(Guid id)
    {
        var result = await _context.Categories
                .Where(c => c.Id == id)
                .Include(c => c.Courses)
                .AsNoTracking().FirstOrDefaultAsync();
        
        if (result == null) {
            return NotFound();
        }

        
        return Ok(result);
    }
    

}
