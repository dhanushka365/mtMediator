using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mtMediator
{
    public interface OrderStatus
    {
        Guid OrderId { get; }
        string Status { get; }

    }
}
