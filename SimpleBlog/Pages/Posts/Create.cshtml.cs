using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using SimpleBlog.Data;
using SimpleBlog.Models;

namespace SimpleBlog.Pages.Posts
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AdminSettings _adminSettings;

        public CreateModel(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            IOptions<AdminSettings> adminSettings)
        {
            _context = context;
            _userManager = userManager;
            _adminSettings = adminSettings.Value;
        }

        [BindProperty]
        public Post Post { get; set; }

        public IActionResult OnGet()
        {
            // Проверка: текущий пользователь — администратор?
            var currentUserEmail = _userManager.GetUserName(User);
            if (currentUserEmail != _adminSettings.AdminEmail)
            {
                return Forbid(); // Запретить доступ
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            // Проверка: текущий пользователь — администратор?
            var currentUserEmail = _userManager.GetUserName(User);
            if (currentUserEmail != _adminSettings.AdminEmail)
            {
                return Forbid(); // Запретить доступ
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Posts.Add(Post);
            _context.SaveChanges();
            return RedirectToPage("/Index");
        }
    }
}
