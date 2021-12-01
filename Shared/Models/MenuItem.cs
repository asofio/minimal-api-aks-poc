using Newtonsoft.Json;

namespace minimalApiAksPoc.Shared.Models
{
    public class MenuItem
    {
        [JsonProperty("menuItemid")]
        public int MenuItemId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("price")]
        public decimal Price { get; set; }

        public static List<MenuItem> GetAll()
        {
            var items = new List<MenuItem>();
            items.Add(new MenuItem
            {
                MenuItemId = 1,
                Name = "Americano",
                Description = "Americano",
                Price = 2.99m
            });
            items.Add(new MenuItem
            {
                MenuItemId = 2,
                Name = "Caramel Macchiato",
                Description = "Caramel Macchiato",
                Price = 4.99m
            });
            items.Add(new MenuItem
            {
                MenuItemId = 3,
                Name = "Latte",
                Description = "Latte",
                Price = 3.99m
            });

            return items;
        }
    }
}