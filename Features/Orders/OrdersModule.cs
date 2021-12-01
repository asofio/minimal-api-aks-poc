using Carter;
using FluentValidation;
using FluentValidation.Results;
using minimalApiAksPoc.Features.Orders.Models;
using minimalApiAksPoc.Shared.Extensions;
using Newtonsoft.Json;

namespace minimalApiAksPoc.Features.Orders
{
    public class OrdersModule : ICarterModule
    {
        private const string ORDER_TAG = "Orders";
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/order/currentorders", CurrentOrders).WithName("currentorders").WithTags(ORDER_TAG);
            app.MapGet("/order/currentorders/{id}" , GetOrder).WithName("getorder").WithTags(ORDER_TAG);
            app.MapPost("/order/neworder", NewOrder).WithTags(ORDER_TAG);
            app.MapPut("/order/updateorder/{id}", UpdateOrder).WithTags(ORDER_TAG);
            app.MapDelete("/order/completeorder/{id}", CompleteOrder).WithTags(ORDER_TAG);
        }

        private static IResult NewOrder(IValidator<CustomerOrder> validator, CustomerOrder order, IOrderService orderService, HttpContext httpContext, LinkGenerator lg, ILogger<OrdersModule> log)
        {
            ValidationResult validationResult = validator.Validate(order);

            if(!validationResult.IsValid) 
            {
                log.LogWarning($"Invalid order: {JsonConvert.SerializeObject(order)}; Validation error: {JsonConvert.SerializeObject(validationResult.ToDictionary())}");
                return Results.ValidationProblem(validationResult.ToDictionary());
            }                

            Guid orderId = orderService.AddOrder(order);
            return Results.Created(lg.GetUriByName(httpContext, "getorder", new { id = orderId })!, $"You posted: {JsonConvert.SerializeObject(order)}");
        }        

        private static IList<CustomerOrder> CurrentOrders(IOrderService orderService, ILogger<OrdersModule> log)
        {
            log.LogInformation($"Getting current orders.  {orderService.Orders.Count} exist.");
            return orderService.Orders;
        }
        
        private static IResult GetOrder(Guid id, IOrderService orderService)
        {
            return Results.Ok(orderService.GetOrder(id)) ?? Results.NotFound($"Order {id} not found.");
        }

        private static IResult CompleteOrder(Guid id, IOrderService orderService)
        {
            orderService.RemoveOrder(id);
            return Results.Ok($"Order {id} completed.");
        }

        private static IResult UpdateOrder(IValidator<CustomerOrder> validator, Guid id, CustomerOrder order, IOrderService orderService, ILogger<OrdersModule> log)
        {
            ValidationResult validationResult = validator.Validate(order);

            if(!validationResult.IsValid) {
                log.LogWarning($"Invalid order: {JsonConvert.SerializeObject(order)}; Validation error: {JsonConvert.SerializeObject(validationResult.ToDictionary())}");
                return Results.ValidationProblem(validationResult.ToDictionary());
            }                
            
            if(!orderService.OrderExists(id))
                return Results.NotFound($"Order {id} not found.");

            orderService.RemoveOrder(id);
            orderService.AddOrder(order);
            return Results.Ok($"Order {id} updated.");
        }
    }
}