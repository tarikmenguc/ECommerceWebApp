using Microsoft.AspNetCore.Authentication.Cookies;
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


// Cookie tabanlý kimlik doðrulama ekleyin
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giriþ sayfasý
        options.LogoutPath = "/Account/Logout"; // Çýkýþ sayfasý
        options.AccessDeniedPath = "/Account/AccessDenied"; // Eriþim reddedildi sayfasý
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(14); // Çerez geçerlilik süresi
        options.SlidingExpiration = true;
    });



builder.Services.AddAuthorization();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Home/Index");
    return Task.CompletedTask;
});

app.Run();
