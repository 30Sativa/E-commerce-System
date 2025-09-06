using EcommerceSystem.Application.Common;
using EcommerceSystem.Application.DTOs.Requests.Order;
using EcommerceSystem.Application.DTOs.Responses.Order;
using EcommerceSystem.Application.Features.Order.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var create = await _mediator.Send(new CreateOrderCommand(request));
            return Ok(BaseResponse<OrderResponse>.SuccessResponse(create, "Order create succesfully"));
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] UpdateOrderRequest request)
        {
            var update = await _mediator.Send(new UpdateOrderCommand(request));
            return Ok(BaseResponse<OrderResponse>.SuccessResponse(update, "Order update succesfully"));
        }
    }
}
