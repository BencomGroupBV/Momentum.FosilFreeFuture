using Benchain.FosilFreeFuture.Service;
using Benchain.FosilFreeFuture.Service.Interfaces;
using Benchain.FosilFreeFuture.Web.Models.DbEntities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProjectSmartContractService, ProjectSmartContractService>();

builder.Services.AddDbContextPool<MomentumContext>(options => options.UseSqlServer("Server=tcp:bc-momentum-2022.database.windows.net,1433;Initial Catalog=momentum-2022;Persist Security Info=False;User ID=momentumadmin;Password=42gfQL3uzhn75NG;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution));
builder.Services.AddScoped<MomentumContext>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
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
