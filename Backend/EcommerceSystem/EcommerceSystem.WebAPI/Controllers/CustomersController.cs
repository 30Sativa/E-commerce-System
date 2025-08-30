using EcommerceSystem.Application.Common;
using EcommerceSystem.Application.DTOs.Requests.Customer;
using EcommerceSystem.Application.DTOs.Responses.Customer;
using EcommerceSystem.Application.Features.Customer.Commands;
using EcommerceSystem.Application.Features.Customer.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(BaseResponse<IEnumerable<CustomerResponse>>.SuccessResponse(customers, "Get all customers success"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
            if (customer == null)
                return NotFound(BaseResponse<CustomerResponse>.FailResponse("Customer not found"));

            return Ok(BaseResponse<CustomerResponse>.SuccessResponse(customer, "Get customer success"));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            var customer = await _mediator.Send(new CreateCustomerCommand(request));
            return Ok(BaseResponse<CreateCustomerResponse>.SuccessResponse(customer, "Customer created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerRequest request)
        {
            var updatedCustomer = await _mediator.Send(new UpdateCustomerCommand(id, request));
            if (updatedCustomer == null)
                return NotFound(BaseResponse<CustomerResponse>.FailResponse("Customer not found"));

            return Ok(BaseResponse<CustomerResponse>.SuccessResponse(updatedCustomer, "Customer updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteCustomerCommand(id));
            if (!success)
                return NotFound(BaseResponse<bool>.FailResponse("Customer not found"));

            return Ok(BaseResponse<bool>.SuccessResponse(true, "Customer deleted successfully"));
        }
    }
}
