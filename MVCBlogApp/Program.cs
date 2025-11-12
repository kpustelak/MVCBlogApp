using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVCBlogApp.Db;
using MVCBlogApp.Filters;
using MVCBlogApp.Interface;
using MVCBlogApp.Service;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<HandleExceptionAttribute>();
});

builder.Services.AddDbContext<BlogDbContext>(options =>
{
    options.UseSqlite("Data Source=blog.db");
});

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICategoryManagmentService, CategoryManagmentService>();
builder.Services.AddScoped<IPostPublishingService, PostPublishingService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddTransient<DbSeeder>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/500"); 
    app.UseStatusCodePagesWithReExecute("/Error/{0}"); 
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    seeder.SeedCategories();
}

app.Run();