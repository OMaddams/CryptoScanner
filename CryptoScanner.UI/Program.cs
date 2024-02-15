using CryptoScanner.Data.Database;
using CryptoScanner.Data.Repositories.Implementation;
using CryptoScanner.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("CoinDbConnection");
builder.Services.AddDbContext<CoinDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("CryptoScanner.UI")));

builder.Services.AddScoped<ICoinsRepository, CoinsRepository>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
