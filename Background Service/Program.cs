using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Background_Service;

var builder = Host.CreateApplicationBuilder(args);

// Registrar el HttpClient
builder.Services.AddHttpClient();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
