using ITSwormTestTask.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

// ������ ����������� � �� ������������� � appsettings.json
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FurnitureContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseStaticFiles();
app.UseRouting();

// ���� �� ���������: Home/Index/Id?
app.UseEndpoints(endpoints =>
    endpoints.MapDefaultControllerRoute());

app.Run();
