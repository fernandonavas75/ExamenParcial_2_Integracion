using Back_Examen_Fn.Models; // Importar el namespace del contexto
using Microsoft.EntityFrameworkCore; // Importar EF Core

var builder = WebApplication.CreateBuilder(args);

// Registrar el DbContext en el contenedor de servicios
builder.Services.AddDbContext<Parcial02Context>(options =>
    options.UseSqlServer("server=FERNANDOBD\\SQLEXPRESS;database=Parcial02;Trusted_Connection=true;Encrypt=false"));

// Agregar controladores
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
