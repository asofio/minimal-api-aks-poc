using FluentValidation;
using minimalApiAksPoc.Features.Orders.Models;
using minimalApiAksPoc.Shared.Models;

namespace minimalApiAksPoc.Features.Orders.Validators
{
    public class OrderItemValidator : AbstractValidator<CustomerOrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(orderItem => orderItem.MenuItemId).NotEmpty();
            RuleFor(orderItem => orderItem.Quantity).NotEmpty();
            RuleFor(orderItem => MenuItem.GetAll().
                Find(inventory => inventory.MenuItemId == orderItem.MenuItemId))
                .NotEmpty()
                .WithMessage((orderItem, context) =>
                    $"Menu Item with Id {orderItem.MenuItemId} does not exist.");
        }
    }
}