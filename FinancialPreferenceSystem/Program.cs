using FinancialPreferenceSystem.Services;
using FinancialPreferenceSystem.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ���UDI
builder.Services.AddScoped<LikeRepository>();
builder.Services.AddScoped<LikeService>();

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
    pattern: "{controller=Like}/{action=Index}/{userid?}");

app.Run();
