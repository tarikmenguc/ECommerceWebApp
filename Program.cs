using Microsoft.EntityFrameworkCore;
using SellerAutomationSystem.Data; // DbContext i�in

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant�s� ekleniyor
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// DbContext ekleniyor
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Razor Pages ekleniyor
builder.Services.AddRazorPages();

var app = builder.Build();

// Hata y�netimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization(); // E�er kimlik do�rulama kullan�yorsan�z bu sat�r kals�n



// Razor Pages rotalar�
app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    // Varsay�lan olarak Home/Index'e y�nlendir
    endpoints.MapFallbackToPage("/Home/Index");
});

app.Run();
