#nullable disable
using CommitMaster.Hera.Core.Entities;
using CommitMaster.Hera.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CommitMaster.Hera.Admin.Controllers
{
    public class LessonController : Controller
    {
        private readonly AdminContext _context;

        public LessonController(AdminContext context)
        {
            _context = context;
        }

        // GET: Lesson
        public async Task<IActionResult> Index()
        {
            var adminContext = _context.Lessons.Include(l => l.Module);
            return View(await adminContext.ToListAsync());
        }

        // GET: Lesson/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lesson/Create
        public IActionResult Create()
        {
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Name");
            return View();
        }

        // POST: Lesson/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,ModuleId,Id,CreatedAt")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                lesson.Id = Guid.NewGuid();
                _context.Add(lesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Name", lesson.ModuleId);
            return View(lesson);
        }

        // GET: Lesson/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Name", lesson.ModuleId);
            return View(lesson);
        }

        // POST: Lesson/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,ModuleId,Id,CreatedAt")] Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Name", lesson.ModuleId);
            return View(lesson);
        }

        // GET: Lesson/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lesson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(Guid id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }
    }
}
