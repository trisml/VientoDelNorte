using Tienda.Repo.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Tienda.Repo.Interfaces;
using Tienda.Repo.Desarrollo;
using Tienda.Mapeado;
using Tienda.Servicio.Desarrollo;
using Tienda.Servicio.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ProyectoTristanContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionBD"));
});

builder.Services.AddTransient(typeof(IEstandar<>), typeof(EstandarRepositorio<>));
builder.Services.AddScoped<IPedido,RepositorioPedido>();

builder.Services.AddAutoMapper(typeof(PerfilAutoMapper));

builder.Services.AddScoped<IServicioUsuario, ServicioUsuario>();
builder.Services.AddScoped<IServicioCategoria, ServicioCategoria>();
builder.Services.AddScoped<IServicioProducto, ServicioProducto>();
builder.Services.AddScoped<IServicioPedido, ServicioPedido>();

builder.Services.AddCors(o =>
{
    o.AddPolicy("Policy", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Policy");
app.UseAuthorization();

app.MapControllers();

app.Run();
