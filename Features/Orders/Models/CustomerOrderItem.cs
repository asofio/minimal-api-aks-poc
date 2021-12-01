using Newtonsoft.Json;

namespace minimalApiAksPoc.Features.Orders.Models
{
    public class CustomerOrderItem
    {
        [JsonProperty("menuItemId")]
        public int MenuItemId { get; set; }
        
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        
    }
}