using Microsoft.Extensions.Hosting;
using Simone.Common.RabbitMQ.Extensions;
using ConsumerApp;
using Microsoft.Extensions.Configuration;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddRabbitMQ(builder.Configuration)
    .AddConsumer<Message, Consumer>(services: builder.Services);

var app = builder.Build();
await app.RunAsync();