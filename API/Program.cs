using API.Model;
using API.Model.Impl;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
// Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost7226", builder =>
    {
        builder.WithOrigins("https://localhost:7226")  // Cho phép truy cập từ localhost:7226
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
// Cấu hình DbContext
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

// Thêm các repository với Dependency Injection
builder.Services.AddTransient<INguoiDungRepository, NguoiDungRepository>();
builder.Services.AddTransient<IThietBiRepository, ThietBiRepository>();

builder.Services.AddTransient<ICTPhieuNhapRepository, CTPhieuNhapRepository>();
builder.Services.AddTransient<ICTPhieuMuonRepository, CTPhieuMuonRepository>();
builder.Services.AddTransient<ICTPhieuTraRepository, CTPhieuTraRepository>();

builder.Services.AddTransient<IPhieuNhapRepository, PhieuNhapRepository>();
builder.Services.AddTransient<IPhieuMuonRepository, PhieuMuonRepository>();
builder.Services.AddTransient<IPhieuTraRepository, PhieuTraRepository>();
builder.Services.AddTransient<IThongKeRepository, ThongKeRepository>();
// mới thêm
builder.Services.AddTransient<IDanhMucTBRepository, DanhMucTBRepository>();

// Thêm service kiểm tra phiếu mượn
builder.Services.AddTransient<IKiemTraPhieuMuonService, KiemTraPhieuMuonService>();
builder.Services.AddHostedService<PhieuMuonBackgroundService>();

// Cấu hình JSON Serialization
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddMemoryCache();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "DAPM",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowLocalhost7226");
app.Run();
