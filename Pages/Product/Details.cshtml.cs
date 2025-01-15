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
            // �r�n� veritaban�ndan ID'ye g�re �ekiyoruz
            Product = await _context.Products
                .Include(p => p.Category) // Kategori ili�kisini dahil ediyoruz
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Product == null)
            {
                return NotFound(); // �r�n bulunamazsa hata d�nd�r
            }

            return Page();
        }
    }
}
