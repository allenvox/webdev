using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab_3.Data;
using lab_3.Models;
using System.Threading.Tasks;
using System.Linq;

namespace lab_3.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Отображение списка проектов
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects.ToListAsync();
            return View(projects);
        }

        // Просмотр деталей проекта
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // Создание проекта (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Создание проекта (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CustomerCompany,ContractorCompany,StartDate,EndDate,Priority")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Перенаправляем на Index
            }

            // Логирование ошибок валидации
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage); // Вывод ошибок в консоль
            }

            // Если не прошли валидацию, возвращаем на форму
            return View(project);
        }

        // Редактирование проекта (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }

        // Редактирование проекта (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CustomerCompany,ContractorCompany,StartDate,EndDate,Priority")] Project project)
        {
            if (id != project.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Projects.Any(p => p.Id == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // Удаление проекта (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null) return NotFound();
            return View(project);
        }

        // Удаление проекта (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
