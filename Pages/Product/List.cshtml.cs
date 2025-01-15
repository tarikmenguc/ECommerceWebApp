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

        // Ürün listesi
        public List<SellerAutomationSystem.Models.Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            // Tüm ürünleri gerekli iliþkilerle birlikte alýyoruz
            Products = await _context.Products
                .Include(p => p.Category) // Kategori iliþkisini dahil ediyoruz
                .Include(p => p.Brand)   // Marka iliþkisini dahil ediyoruz
                .Where(p => p.IsActive && p.IsPublished) // Sadece aktif ve yayýndaki ürünler
                .ToListAsync();
        }
    }
}
