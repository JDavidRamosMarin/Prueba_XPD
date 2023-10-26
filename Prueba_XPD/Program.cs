using Prueba_XPD.Models;
using Prueba_XPD.Repositorio.Contrato;
using Prueba_XPD.Repositorio.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGenericRepository<Peliculas>, PeliculasRepository>();
builder.Services.AddScoped<IPeliculas<Peliculas>, PeliculasRepository>();
builder.Services.AddScoped<IGenericRepository<Boleto>, BoletosRepository>();
builder.Services.AddScoped<IGenericRepository<Genero>, GeneroRepository>();
builder.Services.AddScoped<IGenericRepository<Ventas>, VentasRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
