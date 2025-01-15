using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SellerAutomationSystem.Data;
using SellerAutomationSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SellerAutomationSystem.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        // Sepetteki ��eler ve toplam fiyat
        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }

        // GET: /Cart/Index
        public async Task<IActionResult> OnGetAsync()
        {
            // Kullan�c� kimli�i al�n�yor
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToPage("/Account/Login");
            }

            int userId = int.Parse(userIdClaim.Value);

            // Kullan�c�n�n sepeti y�kleniyor
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

        // POST: /Cart/Index
        public async Task<IActionResult> OnPostAsync(int productId, int quantity)
        {
            if (productId <= 0 || quantity <= 0)
            {
                return BadRequest("Invalid product ID or quantity.");
            }

            // Kullan�c�n�n kimli�ini al
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToPage("/Account/Login");
            }

            int userId = int.Parse(userIdClaim.Value);

            // Kullan�c�n�n mevcut sepetini kontrol et
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            // E�er sepet yoksa, yeni bir tane olu�tur
            if (cart == null)
            {
                cart = new SellerAutomationSystem.Models.Cart
                {
                    UserId = userId,
                    CreatedDate = DateTime.Now
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            // CartItems koleksiyonunun null olmad���ndan emin ol
            if (cart.CartItems == null)
            {
                cart.CartItems = new List<CartItem>();
            }

            // Sepette �r�n var m� kontrol et
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem != null)
            {
                // E�er �r�n sepette varsa miktar�n� art�r
                cartItem.Quantity += quantity;
            }
            else
            {
                // E�er �r�n sepette yoksa yeni bir CartItem olu�tur
                var product = await _context.Products.FindAsync(productId);
                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                };
                _context.CartItems.Add(cartItem);
            }

            // De�i�iklikleri kaydet
            await _context.SaveChangesAsync();

            // Kullan�c�y� sepete y�nlendir
            return RedirectToPage("/Cart/Index");
        }




        // POST: /Cart/Remove
        public async Task<IActionResult> OnPostRemoveAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Cart/Index");
        }
    }
}
