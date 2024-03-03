using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mtMediator
{
    public class OrderStatusConsumer : IConsumer<GetOrderStatus>
    {
        public async Task Consume(ConsumeContext<GetOrderStatus> context)
        {
            await context.RespondAsync<OrderStatus>(new
            {
                OrderId = context.Message.OrderId,
                Status = "Pending"
            });
        }   
    }
}
