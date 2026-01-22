using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVCBlogApp.Db;
using MVCBlogApp.Filters;
using MVCBlogApp.Interface;
using MVCBlogApp.Service;
using Serilog;
using Microsoft.AspNetCore.Identity;
using MVCBlogApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<HandleExceptionAttribute>();
});


builder.Services.AddDbContext<BlogDbContext>(options =>
{
    options.UseSqlite("Data Source=blog.db");
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredLength = 10;
        
    })
    .AddEntityFrameworkStores<BlogDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Admin/Login";
        options.LogoutPath = "/Admin/Logout";
        options.AccessDeniedPath = "/BlogAdmin/AccessDenied";
    });

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICategoryManagmentService, CategoryManagmentService>();
builder.Services.AddScoped<IPostPublishingService, PostPublishingService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddTransient<DbSeeder>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<BlogSettings>(builder.Configuration.GetSection("BlogSettings"));

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    seeder.SeedData();
}

app.Run();