﻿using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mtMediator
{
    public class SubmitOrderConsumer : IConsumer<SubmitOrder>
    {
        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
           Console.WriteLine("Calling SubmitOrderConsumer: {0}", context.Message.OrderId.ToString());
        //code we need here is process the order
        // it might be change in database
           await context.RespondAsync<OrderStatus>(new
           {
               OrderId = context.Message.OrderId,
               Status = "Pending"
           });
        }
    }
}
