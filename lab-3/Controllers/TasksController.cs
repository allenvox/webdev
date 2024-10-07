using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using lab_3.Data;
using lab_3.Models;
using System.Linq;

namespace lab_3.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Просмотр списка задач
        public async System.Threading.Tasks.Task<IActionResult> Index()
        {
            var tasks = _context.Tasks.Include(t => t.Author).Include(t => t.Executor).Include(t => t.Project);
            return View(await tasks.ToListAsync());
        }

        // Просмотр деталей задачи
        public async System.Threading.Tasks.Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.Tasks
                .Include(t => t.Author)
                .Include(t => t.Executor)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null) return NotFound();

            return View(task);
        }

        // Отображение формы создания задачи (GET)
        public IActionResult Create()
        {
            // Заполняем ViewBag данными для выпадающих списков
            ViewData["Employees"] = new SelectList(_context.Employees, "Id", "FirstName");
            ViewData["Projects"] = new SelectList(_context.Projects, "Id", "Name");
            return View(new lab_3.Models.Task()); // Передаем новую задачу в представление
        }

        // Создание задачи (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> Create([Bind("Title,AuthorId,ExecutorId,ProjectId,Status,Comment,Priority")] lab_3.Models.Task task)
        {
            task.Author = await _context.Employees.FindAsync(task.AuthorId);
            task.Executor = await _context.Employees.FindAsync(task.ExecutorId);
            task.Project = await _context.Projects.FindAsync(task.ProjectId);
            _context.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // Перенаправляем на список задач
        }

        // Редактирование задачи (GET)
        public async System.Threading.Tasks.Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            ViewData["Employees"] = new SelectList(_context.Employees, "Id", "FirstName", task.ExecutorId);
            ViewData["Projects"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
            return View(task);
        }

        // Редактирование задачи (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> Edit(int id, [Bind("Id,Title,AuthorId,ExecutorId,ProjectId,Status,Comment,Priority")] lab_3.Models.Task task)
        {
            if (id != task.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Employees"] = new SelectList(_context.Employees, "Id", "FirstName", task.ExecutorId);
            ViewData["Projects"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
            return View(task);
        }

        // Удаление задачи (GET)
        public async System.Threading.Tasks.Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.Tasks
                .Include(t => t.Author)
                .Include(t => t.Executor)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null) return NotFound();

            return View(task);
        }

        // Удаление задачи (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
