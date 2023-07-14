using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models;
using Task = Planner.Models.Task;


namespace Planner.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Calendar(Task task, string filterName)
        {
            //IQueryable<Task> tasksQuery = _context.Tasks;

            if (User.Identity.IsAuthenticated)
            {

                var email = User.FindFirstValue(ClaimTypes.Email);
                var tasksQuery = _context.Tasks.Where(t => t.Email == email);

                if (!string.IsNullOrEmpty(filterName))
                {
                    tasksQuery = tasksQuery.Where(t => t.Name.Contains(filterName));
                }

                var tasks = await tasksQuery.ToListAsync();

                ViewBag.FilterName = filterName;
                ViewBag.AllNames = await _context.Tasks.Where(t => t.Email == email).Select(t => t.Name).Distinct().ToListAsync();

                return View(tasks);
            }

            return _context.Tasks != null ?
                          View(await _context.Tasks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tasks' is null.");

        }


        // GET: Tasks
        [Authorize]
        public async Task<IActionResult> Index(Task task, string filterName)
        {
            //IQueryable<Task> tasksQuery = _context.Tasks;

            if (User.Identity.IsAuthenticated)
            {

                var email = User.FindFirstValue(ClaimTypes.Email);
                var tasksQuery = _context.Tasks.Where(t => t.Email == email);

                if (!string.IsNullOrEmpty(filterName))
                {
                    tasksQuery = tasksQuery.Where(t => t.Name.Contains(filterName));
                }

                var tasks = await tasksQuery.ToListAsync();

                ViewBag.FilterName = filterName;
                ViewBag.AllNames = await _context.Tasks.Where(t => t.Email == email).Select(t => t.Name).Distinct().ToListAsync();

                return View(tasks);
            }

            return _context.Tasks != null ?
                          View(await _context.Tasks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tasks' is null.");


        }


        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }


            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IsCompleted,Email,Deadline")] Task task)
        {

            if (User.Identity.IsAuthenticated) // Перевірка, чи користувач авторизований
            {
                var email = User.FindFirstValue(ClaimTypes.Email); // Отримання електронної пошти з клейма
                task.Email = email; // Присвоєння значення електронної пошти полю "Email" моделі "Task"
            }
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IsCompleted,Email,Deadline")] Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }
            if (User.Identity.IsAuthenticated) // Перевірка, чи користувач авторизований
            {
                var email = User.FindFirstValue(ClaimTypes.Email); // Отримання електронної пошти з клейма
                task.Email = email; // Присвоєння значення електронної пошти полю "Email" моделі "Task"
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
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
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tasks'  is null.");
            }
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
          return (_context.Tasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
