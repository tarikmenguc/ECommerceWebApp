using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SellerAutomationSystem.Data; // DbContext için
using SellerAutomationSystem.Models; // Modeller için
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

            // Kullanýcýnýn sipariþlerini yükle
            Orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product) // Sipariþ ürünlerini yükle
                .Where(o => o.UserId == userId) // Giriþ yapan kullanýcýya ait sipariþler
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Page();
        }
    }
}
