using F2GTraining.Data;
using F2GTraining.Helpers;
using F2GTraining.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(12);
});

string connectionString = builder.Configuration.GetConnectionString("databaseF2G");
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<HelperRutasProvider>();
builder.Services.AddSingleton<HelperSubirFicheros>();
builder.Services.AddTransient<RepositoryUsuarios>();
builder.Services.AddTransient<RepositoryJugadores>();
builder.Services.AddTransient<RepositoryEquipos>();
builder.Services.AddTransient<RepositoryEntrenamientos>();
builder.Services.AddDbContext<F2GDataBaseContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=InicioSesion}");

app.Run();
