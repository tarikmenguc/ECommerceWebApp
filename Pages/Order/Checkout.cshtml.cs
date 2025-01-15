using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SellerAutomationSystem.Data;
using SellerAutomationSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SellerAutomationSystem.Pages.Order
{
    public class CheckoutModel : PageModel
    {
        private readonly AppDbContext _context;

        public CheckoutModel(AppDbContext context)
        {
            _context = context;
        }

        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        [BindProperty]
        public string ShippingAddress { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToPage("/Account/Login");
            }

            int userId = int.Parse(userIdClaim.Value);

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                CartItems = cart.CartItems.ToList();
                TotalPrice = CartItems.Sum(ci => ci.Quantity * ci.Price);
            }
            else
            {
                CartItems = new List<CartItem>();
                TotalPrice = 0;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userNameClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Name);

            if (userIdClaim == null || userNameClaim == null)
            {
                return RedirectToPage("/Account/Login");
            }

            int userId = int.Parse(userIdClaim.Value);
            string userName = userNameClaim.Value;

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                return RedirectToPage("/Cart/Index");
            }

            // Yeni Sipariþ Oluþtur
            var order = new SellerAutomationSystem.Models.Order
            {
                UserId = userId,
                UserName = userName, // Kullanýcý adýný burada ekliyoruz
                ShippingAddress = ShippingAddress,
                TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Price),
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Price
                }).ToList()
            };

            _context.Orders.Add(order);

            // Sepeti Temizle
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Order/Confirmation", new { orderId = order.Id });
        }

    }
}
