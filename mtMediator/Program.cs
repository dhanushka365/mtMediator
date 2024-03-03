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
            // Create a new collection of services
            var services = new ServiceCollection();

            // Add MassTransit Mediator to the services
            services.AddMediator(cfg =>
            {
                // Register SubmitOrderConsumer and OrderStatusConsumer as consumers
                cfg.AddConsumer<SubmitOrderConsumer>();
                cfg.AddConsumer<OrderStatusConsumer>();
            });

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();
            // Resolve the IMediator service from the service provider
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            // Generate a new Guid for the order ID
            Guid orderId = NewId.NextGuid();
            Console.WriteLine("Order ID: {0}", orderId);
            Console.WriteLine("Producer SubmitOrderConsumer: {0}", orderId.ToString());
            // Send a SubmitOrder message with the generated order ID
            await mediator.Send<SubmitOrder>(new
            {
                OrderId = orderId
            });
            
            // Create a request client for GetOrderStatus messages
            var client = mediator.CreateRequestClient<GetOrderStatus>();
            // Send a GetOrderStatus request with the generated order ID and await the response
            var response = await client.GetResponse<OrderStatus>(new
            {
                OrderId = orderId
            });
            // Print the order status received in the response
            Console.WriteLine("Order Status: {0}-({1})", response.Message.OrderId, response.Message.Status); 
        }
    }
}