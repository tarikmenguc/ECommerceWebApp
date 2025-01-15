using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using SellerAutomationSystem.Data;

namespace ECommerceWebApp.Pages.Product
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public SellerAutomationSystem.Models.Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Ürünü veritabanýndan ID'ye göre çekiyoruz
            Product = await _context.Products
                .Include(p => p.Category) // Kategori iliþkisini dahil ediyoruz
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Product == null)
            {
                return NotFound(); // Ürün bulunamazsa hata döndür
            }

            return Page();
        }
    }
}
