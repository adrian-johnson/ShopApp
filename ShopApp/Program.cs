using Microsoft.EntityFrameworkCore;
using ShopApp.Data;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("SystemDbConnection");

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSession();

// EF Core + SQLite
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlite("Data Source=app.db"));

// EF Core + SQL Server
builder.Services.AddDbContext<AppDbContext>(c => c.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapRazorPages();

app.UseAuthorization();

app.Run();
