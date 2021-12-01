using FluentValidation;
using minimalApiAksPoc.Features.Orders.Models;

namespace minimalApiAksPoc.Features.Orders.Validators
{
    public class OrderValidator : AbstractValidator<CustomerOrder>
    {
        public OrderValidator()
        {
            RuleFor(order => order.CustomerName).NotEmpty();
            RuleFor(order => order.OrderItems).NotEmpty();
            RuleForEach(order => order.OrderItems).SetValidator(new OrderItemValidator());
        }        
    }
}