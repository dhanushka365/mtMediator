using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mtMediator
{
    public interface GetOrderStatus
    {
        Guid OrderId { get; }
    }
}
