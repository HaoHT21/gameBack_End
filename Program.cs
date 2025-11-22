using Microsoft.EntityFrameworkCore;
using AssignmentGD1.Data;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// =========================================================================
// 1. Cấu hình DỊCH VỤ (SERVICES)
// =========================================================================

// Thêm dịch vụ DbContext để kết nối SQL Server
builder.Services.AddDbContext<MinecraftDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Thêm dịch vụ cho Controllers (đủ cho cả API và Views)
builder.Services.AddControllersWithViews();

// Thêm dịch vụ Swagger/OpenAPI (Rất hữu ích để kiểm tra API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// =========================================================================
// 2. Cấu hình HTTP Request Pipeline
// =========================================================================

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Bật Swagger/Swagger UI chỉ trong môi trường phát triển
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

// Định tuyến cho API Controllers
app.MapControllers();

// Định tuyến cho MVC Controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();