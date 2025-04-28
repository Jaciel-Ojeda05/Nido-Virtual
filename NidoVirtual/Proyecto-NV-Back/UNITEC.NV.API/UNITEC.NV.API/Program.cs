using UNITEC.NV.API.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using UNITEC.NV.DAL.Generos;
using UNITEC.NV.DAL.Plataformas;
using UNITEC.NV.DAL.Reportes;
using UNITEC.NV.DAL.Usuarios;
using UNITEC.NV.DAL.Videojuegos;
using UNITEC.NV.BL.Generos;
using UNITEC.NV.BL.Plataformas;
using UNITEC.NV.BL.Reportes;
using UNITEC.NV.BL.Usuarios;
using UNITEC.NV.BL.Videojuegos;
using UNITEC.NV.BL.Generos.Interfaz;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar DbContext
builder.Services.ConfigureDbContext();

// Configurar dependencias
builder.Services.ConfigureDependencias();

// Registra los repositorios
builder.Services.AddTransient<GeneroRepository>();
builder.Services.AddTransient<PlataformaRepository>();
builder.Services.AddTransient<ReportesRepository>();
builder.Services.AddTransient<UsuarioRepository>();
builder.Services.AddTransient<VideojuegoRepository>();

// Registra los managers
builder.Services.AddTransient<GeneroManager>();
builder.Services.AddTransient<PlataformaManager>();
builder.Services.AddTransient<ReportesManager>();
builder.Services.AddTransient<UsuarioManager>();
builder.Services.AddTransient<VideojuegoManager>();

// Configurar JWT
var tokenValidationParameters = builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddSingleton(tokenValidationParameters);

// Registrar la autenticación con JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = tokenValidationParameters;

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Error de autenticación: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"Token válido: {context.SecurityToken}");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddSingleton(tokenValidationParameters);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
    });
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Usar CORS
app.UseCors("AllowAnyOrigin");

// ¡Agrega esta línea para servir archivos estáticos!
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes")),
    RequestPath = "/imagenes"
});

app.UseAuthentication();
app.UseAuthorization();

//Usar middleware JWT
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();