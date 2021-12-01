using Newtonsoft.Json;

namespace minimalApiAksPoc.Features.Orders.Models
{
    public class CustomerOrder
    {
        [JsonProperty("orderId")]
        public Guid OrderId { get; }

        [JsonProperty("customerName")]
        public string? CustomerName { get; set; }

        [JsonProperty("orderItems")]
        public List<CustomerOrderItem>? OrderItems { get; set; }

        public CustomerOrder()
        {
            OrderId = Guid.NewGuid();
        }
    }
}