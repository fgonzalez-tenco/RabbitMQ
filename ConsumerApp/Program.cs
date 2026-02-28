using Microsoft.Extensions.Hosting;
using Simone.Common.RabbitMQ.Extensions;
using ConsumerApp;
using Simone.Common.RabbitMQ.Models;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddRabbitMQ(builder.Configuration.GetSection("RabbitMQ"))
            .AddConsumer<Message, Consumer>(services: builder.Services);

        var app = builder.Build();
        await app.RunAsync();
    }
}