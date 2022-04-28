using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Movie_01.Data;
using Movie_01.Data.Cart;
using Movie_01.Data.Helpers;
using Movie_01.Data.Services;
using Movie_01.Models;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;

// Connection
service.AddDbContext<AppDbContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// AutoMapper
service.AddAutoMapper(typeof(MappingProfiles));

// Services
service.AddScoped<ICategoriesService, CategoriesService>();
service.AddScoped<IActorsService, ActorsService>();
service.AddScoped<IProducersService, ProducersService>();
service.AddScoped<ICinemasService, CinemasService>();
service.AddScoped<IMoviesService, MoviesService>();
service.AddScoped<IOrdersService, OrdersService>();

// Identity
service.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddErrorDescriber<CustomIdentityErrorDescriber>();    

service.AddAuthentication(opt => opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme);

// Shopping Cart
service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
service.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
service.AddSession();
service.AddMemoryCache();

// Add services to the container.
service.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");    

await AppDbInitializer.SeedAsync(app);
await AppDbInitializer.SeedUserAndRolesAsync(app);

app.Run();
