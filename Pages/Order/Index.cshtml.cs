using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SellerAutomationSystem.Data; // DbContext i�in
using SellerAutomationSystem.Models; // Modeller i�in
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerAutomationSystem.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<SellerAutomationSystem.Models.Order> Orders { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToPage("/Account/Login");
            }

            int userId = int.Parse(userIdClaim.Value);

            // Kullan�c�n�n sipari�lerini y�kle
            Orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product) // Sipari� �r�nlerini y�kle
                .Where(o => o.UserId == userId) // Giri� yapan kullan�c�ya ait sipari�ler
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Page();
        }
    }
}
