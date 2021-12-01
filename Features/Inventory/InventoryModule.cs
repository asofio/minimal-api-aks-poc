using Carter;
using minimalApiAksPoc.Shared.Models;

namespace minimalApiAksPoc.Features.Inventory
{
    public class InventoryModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/inventory", GetInventory).WithTags("Inventory");
        }

        private IList<MenuItem> GetInventory(HttpContext context)
        {
            return MenuItem.GetAll();
        }
    }
}