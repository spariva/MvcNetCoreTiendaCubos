using Microsoft.EntityFrameworkCore;
using MvcNetCoreTiendaCubos.Data;
using MvcNetCoreTiendaCubos.Repositories;
using MvcNetCoreTiendaCubos.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("MySqlCubos");
builder.Services.AddDbContext<CubosContext>(options => options.UseMySQL(connectionString));

builder.Services.AddTransient<RepositoryCubos>();
builder.Services.AddTransient<HelperPathProvider>();

builder.Services.AddSession();
builder.Services.AddMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.UseAuthorization();
app.UseSession();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cubos}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
