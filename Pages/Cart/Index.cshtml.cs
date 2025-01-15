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

        // Sepetteki öðeler ve toplam fiyat
        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }

        // GET: /Cart/Index
        public async Task<IActionResult> OnGetAsync()
        {
            // Kullanýcý kimliði alýnýyor
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToPage("/Account/Login");
            }

            int userId = int.Parse(userIdClaim.Value);

            // Kullanýcýnýn sepeti yükleniyor
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

            // Kullanýcýnýn kimliðini al
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToPage("/Account/Login");
            }

            int userId = int.Parse(userIdClaim.Value);

            // Kullanýcýnýn mevcut sepetini kontrol et
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            // Eðer sepet yoksa, yeni bir tane oluþtur
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

            // CartItems koleksiyonunun null olmadýðýndan emin ol
            if (cart.CartItems == null)
            {
                cart.CartItems = new List<CartItem>();
            }

            // Sepette ürün var mý kontrol et
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem != null)
            {
                // Eðer ürün sepette varsa miktarýný artýr
                cartItem.Quantity += quantity;
            }
            else
            {
                // Eðer ürün sepette yoksa yeni bir CartItem oluþtur
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

            // Deðiþiklikleri kaydet
            await _context.SaveChangesAsync();

            // Kullanýcýyý sepete yönlendir
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
