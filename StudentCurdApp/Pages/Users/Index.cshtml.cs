using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentCurdApp.Models;

namespace StudentCurdApp.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly StudentCrudApp.Data.ApplicationDbContext _context;

        public IndexModel(StudentCrudApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        // This list will store users for the current page only
        public IList<User> User { get; set; } = default!;

        // Tracks the current page number
        public int CurrentPage { get; set; }

        // Total number of pages
        public int TotalPages { get; set; }

        // Number of users to display per page
        private const int PageSize = 50;

        // Called automatically on page load
        public async Task OnGetAsync(int? pagenum)
        {
            int currentPage = pagenum ?? 1;
            CurrentPage = currentPage;

            var totalUsers = await _context.Users.CountAsync();
            TotalPages = (int)Math.Ceiling(totalUsers / (double)PageSize);

            User = await _context.Users
                .OrderBy(u => u.Id)
                .Skip((currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }

    }
}
