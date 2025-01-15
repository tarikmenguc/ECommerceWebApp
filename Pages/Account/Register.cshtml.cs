using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SellerAutomationSystem.Data;
using SellerAutomationSystem.Models;

namespace SellerAutomationSystem.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _context;

        public RegisterModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User Input { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Yeni kullanýcý ekleme
            _context.Users.Add(Input);
            _context.SaveChanges();

            return RedirectToPage("/Account/Login");
        }
    }
}
