using EcommerceSystem.Application.Common;
using EcommerceSystem.Application.DTOs.Requests.Order;
using EcommerceSystem.Application.DTOs.Responses.Order;
using EcommerceSystem.Application.Features.Order.Commands;
using EcommerceSystem.Application.Features.Order.Queries;
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

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(BaseResponse<IEnumerable<OrderResponse>>.SuccessResponse(orders, "Get all orders successfully"));
        }

        [HttpGet("customer/{customerid}")]
        public async Task<IActionResult> GetOrdersByCustomerId(int customerid)
        {
            var orders = await _mediator.Send(new GetCustomerByOrdersQuery(customerid));
            return Ok(BaseResponse<List<OrderResponse>>.SuccessResponse(orders, "Get orders by customer id successfully"));
        }
    }
}
