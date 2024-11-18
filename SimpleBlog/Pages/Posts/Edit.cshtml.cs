using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleBlog.Data;
using SimpleBlog.Models;

namespace SimpleBlog.Pages.Posts
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Post Post { get; set; }

        public IActionResult OnGet(int id)
        {
            Post = _context.Posts.Find(id);
            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Posts.Update(Post);
            _context.SaveChanges();
            return RedirectToPage("/Index");
        }
    }
}
