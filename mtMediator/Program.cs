using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace mtMediator
{
    class Program
    {
        public static async Task Main(string[] args)
        {
             var services = new ServiceCollection();

            services.AddMediator(cfg =>
            {
                cfg.AddConsumer<SubmitOrderConsumer>();
                cfg.AddConsumer<OrderStatusConsumer>();
            });

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            Guid orderId = NewId.NextGuid();
            await mediator.Send<SubmitOrder>(new
            {
                OrderId = orderId
            });

            var client = mediator.CreateRequestClient<GetOrderStatus>();
            var response = await client.GetResponse<OrderStatus>(new
            {
                OrderId = orderId
            });
            Console.WriteLine("Order Status: {0}", response.Message.Status); 
        }
    }
}