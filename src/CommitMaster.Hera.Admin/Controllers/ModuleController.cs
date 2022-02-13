#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommitMaster.Hera.Core.Entities;
using CommitMaster.Hera.Infra.Data;

namespace CommitMaster.Hera.Admin.Controllers
{
    public class ModuleController : Controller
    {
        private readonly AdminContext _context;

        public ModuleController(AdminContext context)
        {
            _context = context;
        }

        // GET: Module
        public async Task<IActionResult> Index()
        {
            var adminContext = _context.Modules.Include(c => c.Course);
            return View(await adminContext.ToListAsync());
        }

        // GET: Module/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Module/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Description");
            return View();
        }

        // POST: Module/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Order,CourseId,Id,CreatedAt")] Module @module)
        {
            if (ModelState.IsValid)
            {
                @module.Id = Guid.NewGuid();
                _context.Add(@module);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Description", @module.CourseId);
            return View(@module);
        }

        // GET: Module/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Description", @module.CourseId);
            return View(@module);
        }

        // POST: Module/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Order,CourseId,Id,CreatedAt")] Module @module)
        {
            if (id != @module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(@module.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Description", @module.CourseId);
            return View(@module);
        }

        // GET: Module/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Module/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var @module = await _context.Modules.FindAsync(id);
            _context.Modules.Remove(@module);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(Guid id)
        {
            return _context.Modules.Any(e => e.Id == id);
        }
    }
}
