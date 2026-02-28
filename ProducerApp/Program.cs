using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Simone.Common.RabbitMQ.Extensions;

namespace ProducerApp
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddRabbitMQ(builder.Configuration.GetSection("RabbitMQ"));
            builder.Services.AddSingleton<Publisher>();

            var app = builder.Build();

            var publisher = app.Services.GetRequiredService<Publisher>();
            await publisher.SendAsync();

            await app.RunAsync();
        }
    }
}
