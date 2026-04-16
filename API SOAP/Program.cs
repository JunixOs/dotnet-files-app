using System.Web.Services.Description;
using API_SOAP.Controllers;
using API_SOAP.Controllers.Interfaces;
using API_SOAP.Src.Middleware;
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();

// ############# Add AppDbContext #############

// ############# 

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

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    // se registra la clase HomeController como un "servicio SOAP"
    serviceBuilder.AddService<HomeController>();

    serviceBuilder.AddServiceEndpoint<HomeController , IHomeController>( // Primero se define la relacion entre "Implementacion" y "Contrato"
        new BasicHttpBinding(),
        "/api-soap/v1" // Aqui se define la URL donde estara disponible el servicio
        // La ruta del endpoint es:
        // http://localhost:5000/api-soap/v1
        // para el WSDL: http://localhost:5000/api-soap/v1?wdsl
    );
});

// ############# Configurar APP para que use CORS
app.UseCors("AllowRazorApp");

// ############# Registro de Middlewares
app.UseMiddleware<ExceptionMiddleware>();
// ############# 

var metadata = app.Services.GetRequiredService<ServiceMetadataBehavior>();
metadata.HttpGetEnabled = true;

app.Run();
