using CommitMaster.Hera.Core.Entities;
using CommitMaster.Hera.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommitMaster.Hera.Api.Controllers

{
    [ApiController]
    [Route("api/v1/Lesson")]
    public class LessonController : ControllerBase
    {
        private readonly MyAppContext _context;

        public LessonController(MyAppContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Lesson>>> GetAllLesson()
        {
            var result = await _context.Lessons.AsNoTracking().ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Lesson>> GetLessonById(Guid id)
        {
            var result = await _context.Lessons
                    .Where(c => c.Id == id)
                    .Include(c => c.Module)
                    .AsNoTracking().FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
