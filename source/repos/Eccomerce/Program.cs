using Eccomerce.ConexionBD;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar servicios necesarios
builder.Services.AddScoped<ProductoRepository>();
builder.Services.AddScoped<ConexionBD>();

// Configurar CORS para permitir acceso desde http://127.0.0.1:5500
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://127.0.0.1:5500") // Permitir solicitudes desde este origen
                         .AllowAnyMethod()  // Permitir cualquier método (GET, POST, etc.)
                         .AllowAnyHeader()  // Permitir cualquier cabecera
                         .AllowCredentials(); // Permitir envío de cookies o credenciales si es necesario
        });
});

var app = builder.Build();

// Usar CORS para permitir que las solicitudes desde el frontend puedan acceder a la API
app.UseCors("AllowSpecificOrigins");

// Habilitar Swagger para documentar la API
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger"; // Swagger estará disponible en /swagger
});


// Configurar HTTPS redirection y otros middleware necesarios
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
