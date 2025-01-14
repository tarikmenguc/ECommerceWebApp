using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SellerAutomationSystem.Data; // DbContext için

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýsý ekleniyor
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// DbContext ekleniyor
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Razor Pages ekleniyor
builder.Services.AddRazorPages();

var app = builder.Build();

// Hata yönetimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Eðer kimlik doðrulama kullanýyorsanýz bu satýr kalsýn



// Razor Pages rotalarý

app.MapRazorPages();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Home/Index");
    return Task.CompletedTask;
});

app.Run();
