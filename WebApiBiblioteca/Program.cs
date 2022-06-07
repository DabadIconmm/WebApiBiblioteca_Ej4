using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApiBiblioteca;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Registramos en el sistema de inyecci�n de dependencias de la aplicaci�n el ApplicationDbContext

// Actividad 4.1
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(connectionString);
    //Con esto hacemos que no se realize el traking de los registros de una BBDD, as� somos expl�citos donde queremos hacer el tracking.
    opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
);

//4.3.d
builder.Services.AddControllers().AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
