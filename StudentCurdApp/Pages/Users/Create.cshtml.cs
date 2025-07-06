using Microsoft.EntityFrameworkCore; // Required for EF Core methods
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; // Required for SelectList
using StudentCurdApp.Enums; // Import enum namespace
using StudentCurdApp.Models;

namespace StudentCurdApp.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly StudentCrudApp.Data.ApplicationDbContext _context;

        public CreateModel(StudentCrudApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        // Add this property to hold the dropdown options for the Role field
        public SelectList RoleOptions { get; set; } = default!;

        public IActionResult OnGet()
        {
            // Populate dropdown with UserRole enum values
            RoleOptions = new SelectList(Enum.GetValues(typeof(UserRole)));

            return Page();
        }

        [BindProperty] 
        public User User { get; set; } = default!; // Ensure User is initialized to avoid null reference errors

        public string Password { get; set; } = string.Empty; // Property to hold the password input

        // This method handles the form submission when the user clicks the "Create" button
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If form validation fails, reload dropdown again so it doesn't break
                RoleOptions = new SelectList(Enum.GetValues(typeof(UserRole)));

                return Page();
            }
            // 🔍 Check if a user with the same name already exists (case-insensitive)
            bool userExists = await _context.Users
                .AnyAsync(u => u.Name.ToLower() == User.Name.ToLower());

            if (userExists)
            {
                // ❌ Add a model state error
                ModelState.AddModelError("User.Name", "User with this name already exists.");

                RoleOptions = new SelectList(Enum.GetValues(typeof(UserRole)));
                return Page(); // Return to the page with error
            }

            _context.Users.Add(User); // Add user to database
            await _context.SaveChangesAsync(); // Save changes

            return RedirectToPage("./Index"); // Redirect after successful creation
        }
    }
}
