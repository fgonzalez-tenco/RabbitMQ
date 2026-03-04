using Microsoft.Extensions.DependencyInjection;
using Simone.Common.RabbitMQ.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProducerApp;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddRabbitMQ(builder.Configuration);
builder.Services.TryAddTransient<Publisher>();

var app = builder.Build();
_ = app.StartAsync();

var publisher = app.Services.GetRequiredService<Publisher>();
await publisher.SendAsync();

Console.WriteLine("¡Mensaje enviado!. Presiona cualquier tecla para salir...");
Console.ReadKey();