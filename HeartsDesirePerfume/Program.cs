using Entities.DB;
using Entities.IdentityEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//IOC Container
builder.Services.AddScoped<IProductServices, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryServices>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<ApplicationUser,ApplicationUserRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser,ApplicationUserRole,ApplicationDbContext,Guid>>()
    .AddRoleStore<RoleStore<ApplicationUserRole,ApplicationDbContext,Guid>>();

var app = builder.Build();

//builder.Services.AddDbContext<ApplicationDbContext>(optins=>
//{
//    optins.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
//});


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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute
    (
        name: "areas",
        pattern: "{area:exists}/{controller=home}/{action=Index}"

        );

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
