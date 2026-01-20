using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
// using Pomelo.EntityFrameworkCore.MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// 🔹 STRING DE CONEXÃO (vem do appsettings.json)
var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

// 🔹 DbContext MySQL
builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(
        connection,
        new MySqlServerVersion(new Version(8, 0, 36)) // versão mais atual comum
    )
);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger somente em Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares
app.UseHttpsRedirection();

app.UseAuthorization();

// Map Controllers
app.MapControllers();

app.Run();
