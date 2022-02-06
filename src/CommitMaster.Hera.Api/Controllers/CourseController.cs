using CommitMaster.Hera.Core.Entities;
using CommitMaster.Hera.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommitMaster.Hera.Api.Controllers;



[ApiController]
[Route("api/v1/course")]
public class CourseController : ControllerBase
{
    private readonly MyAppContext _context;

    public CourseController(MyAppContext context)
    {
        _context = context;
    }


    [HttpGet()]
    public async Task<ActionResult<List<Course>>> GetAllCourses()
    {
        var result = await _context.Courses.AsNoTracking().ToListAsync();
        
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Course>> GetCategoryById(Guid id)
    {
        var result = await _context.Courses
            .Where(c => c.Id == id)
            .Include(c => c.Modules)
            .ThenInclude(m => m.Lessons)
            .AsNoTracking().FirstOrDefaultAsync();

        if (result == null) {
            return NotFound();
        }
        
        return Ok(result);
    }
    

}