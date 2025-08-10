using BiblioFinder.Application.Contracts.Repositories;
using BiblioFinder.Application.Contracts.Services;
using BiblioFinder.Application.UseCases;
using BiblioFinder.Infrastructure.Persistence;
using BiblioFinder.Infrastructure.Repositories;
using BiblioFinder.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BiblioFinderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(BiblioFinderDbContext).Assembly.FullName)));

builder.Services.AddHttpClient<BookService, OpenLibraryBookService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<SearchHistoryRepository, EfSearchHistoryRepository>();

builder.Services.AddScoped<SearchBooksUseCase>();

builder.Services.AddScoped<SearchHistoryUseCase>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
