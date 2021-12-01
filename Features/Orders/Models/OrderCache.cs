using minimalApiAksPoc.Features.Orders.Models;

namespace minimalApiAksPoc.Features.Orders.Models
{
    public class OrderCache : IOrderService
    {
        public List<CustomerOrder> Orders { get; set; }

        public OrderCache()
        {
            Orders = new List<CustomerOrder>();
        }

        public Guid AddOrder(CustomerOrder order)
        {
            Orders.Add(order);
            return order.OrderId;
        }

        public void RemoveOrder(Guid orderId)
        {
            if(Orders.Any(o => o.OrderId == orderId))
                Orders.Remove(Orders.First(o => o.OrderId == orderId));
        }

        public CustomerOrder? GetOrder(Guid orderId)
        {
            return Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public bool OrderExists(Guid orderId)
        {
            return Orders.Any(o => o.OrderId == orderId);
        }
    }
}