using Enigma_Protocol.DB; // Adjust this based on your actual namespace
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Authentication services
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        //options.LogoutPath = "/Account/Logout";
        options.SlidingExpiration = true; // Optional: Extend the expiration on each request
        options.ExpireTimeSpan = TimeSpan.FromDays(30); // Set the default expiration time

    });

builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//Parte Admin:
//Aggiustare le view per farle piu' belle, Aggiungere la foto del prodotto nella Product List.
//Aggiungere navigazione per le varie liste (inventory e Product).

//----

//Parte Client:
//Aggiungere la funzionalita' del carrello, per potere aggiungere il metodo di pagamento e salvarlo.
//Aggiungere la navigazione dal carrello al catalogo.