using minimalApiAksPoc.Features.Orders.Models;

namespace minimalApiAksPoc.Features.Orders.Services
{
    public interface IOrderService
    {
        List<CustomerOrder> Orders { get; set; }

        Guid AddOrder(CustomerOrder order);
        void RemoveOrder(Guid orderId);
        CustomerOrder? GetOrder(Guid orderId);
        bool OrderExists(Guid orderId);
    }
}