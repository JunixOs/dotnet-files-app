using Construccion_II___App_API_Rest.src.films.application;
using Construccion_II___App_API_Rest.Src.Actors;
using Construccion_II___App_API_Rest.Src.Actors.Application;
using Construccion_II___App_API_Rest.Src.Films;
using Construccion_II___App_API_Rest.Src.Films.Application;
using Construccion_II___App_API_Rest.Src.Films.Middleware;
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ############# Configure API SOAP
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();

// Activar Debug Preciso en SOAP
builder.Services.AddSingleton<IServiceBehavior>(
    new ServiceDebugBehavior
    {
        IncludeExceptionDetailInFaults = true
    });
// #############


// ############# Add AppDbContext #############
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// #############

// ############# Configure DI (Dependency Inyection) #############
//  Aqui añade más conforme vayan apareciendo clases que necesiten DI

// Esto le dice a .NET que si le piden IFilmRepository que le pase FilmRepository junto con
// todas las dependencias necesarias
builder.Services.AddScoped<IFilmRepository , FilmRepository>();

// Se crea una instancia por request HTTP
// es ideal para:
//  - bases de datos
//  - repositorios
//  - servicios
builder.Services.AddScoped<FilmController>();

builder.Services.AddScoped<ListFilms>();
builder.Services.AddScoped<FindFilmById>();
builder.Services.AddScoped<SaveFilm>();
builder.Services.AddScoped<UpdateFilm>();
builder.Services.AddScoped<DeleteFilmById>();

builder.Services.AddScoped<IActorRepository , ActorRepository>();

builder.Services.AddScoped<ActorController>();

builder.Services.AddScoped<SaveActor>();
builder.Services.AddScoped<GetActorById>();
builder.Services.AddScoped<ListAllActors>();

// Para Endpoints SOAP
builder.Services.AddScoped<FilmSoapService>();
builder.Services.AddScoped<ActorSoapService>();

// #############

// ############# Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// ############# Configuracion CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowRazorApp",
        policy =>
        {
            policy.WithOrigins("https://localhost:7299")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
// ############# 

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = 
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

// ############# Ejecutar SCRIPT SQL
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

    var path = Path.Combine(env.ContentRootPath, "Database", "Scripts", "data-seed.sql");
    var sql = File.ReadAllText(path);

    context.Database.ExecuteSqlRaw(sql);
}
// #############

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// ############# Configure SOAP Services
app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<FilmSoapService>(serviceOptions =>
    {
        serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    // se registra la clase "FilmSoapService" como un "servicio SOAP"
    serviceBuilder.AddService<FilmSoapService>();
    serviceBuilder.AddService<ActorSoapService>();

    // qui se define la URL donde estara disponible el servicio
    serviceBuilder.AddServiceEndpoint<FilmSoapService , IFilmSoapService>(
        new BasicHttpBinding(),
        "/api-soap/v1/film"
        // La ruta del endpoint es:
        // http://localhost:5000/api-soap/v1/film
        // para el WSDL: http://localhost:5000/api-soap/v1/film?wdsl
    );
    serviceBuilder.AddServiceEndpoint<ActorSoapService , IActorSoapService>(
        new BasicHttpBinding(),
        "/api-soap/v1/actor"
    );
});
// #############

// ############# Configurar APP para que use CORS
app.UseCors("AllowRazorApp");

// ############# Registro de Middlewares
app.UseMiddleware<ExceptionMiddleware>();
// #############

// Mostrar WSDL para cada Endpoint en XML
var metadata = app.Services.GetRequiredService<ServiceMetadataBehavior>();
metadata.HttpGetEnabled = true;

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
