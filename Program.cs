using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SellerAutomationSystem.Data; // DbContext i�in

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant�s� ekleniyor
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// DbContext ekleniyor
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


// Cookie tabanl� kimlik do�rulama ekleyin
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giri� sayfas�
        options.LogoutPath = "/Account/Logout"; // ��k�� sayfas�
        options.AccessDeniedPath = "/Account/AccessDenied"; // Eri�im reddedildi sayfas�
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(14); // �erez ge�erlilik s�resi
        options.SlidingExpiration = true;
    });



builder.Services.AddAuthorization();

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

// E�er kimlik do�rulama kullan�yorsan�z bu sat�r kals�n



// Razor Pages rotalar�
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Home/Index");
    return Task.CompletedTask;
});

app.Run();
