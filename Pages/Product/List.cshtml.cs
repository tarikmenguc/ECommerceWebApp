using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SellerAutomationSystem.Data;
using SellerAutomationSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceWebApp.Pages.Product
{
    public class ListModel : PageModel
    {
        private readonly AppDbContext _context;

        public ListModel(AppDbContext context)
        {
            _context = context;
        }

        // �r�n listesi
        public List<SellerAutomationSystem.Models.Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            // T�m �r�nleri gerekli ili�kilerle birlikte al�yoruz
            Products = await _context.Products
                .Include(p => p.Category) // Kategori ili�kisini dahil ediyoruz
                .Include(p => p.Brand)   // Marka ili�kisini dahil ediyoruz
                .Where(p => p.IsActive && p.IsPublished) // Sadece aktif ve yay�ndaki �r�nler
                .ToListAsync();
        }
    }
}
