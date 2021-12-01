namespace minimalApiAksPoc.Features.Orders.Models
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