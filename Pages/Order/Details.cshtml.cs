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
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public SellerAutomationSystem.Models.Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int orderId)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToPage("/Account/Login");
            }

            int userId = int.Parse(userIdClaim.Value);

            // Sipari� ve detaylar�n� y�kle
            Order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (Order == null)
            {
                return NotFound("Order not found.");
            }

            OrderItems = Order.OrderItems.ToList();

            return Page();
        }
    }
}
