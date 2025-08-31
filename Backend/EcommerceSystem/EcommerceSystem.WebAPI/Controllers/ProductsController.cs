using EcommerceSystem.Application.Common;
using EcommerceSystem.Application.DTOs.Requests.Product;
using EcommerceSystem.Application.DTOs.Responses.Product;
using EcommerceSystem.Application.Features.Product.Commands;
using EcommerceSystem.Application.Features.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var product =await _mediator.Send(new GetAllProductsQuery());
            if (product == null) 
            {
                return NotFound(BaseResponse<ProductResponse>.FailResponse("Product not found"));
            }
            return Ok(BaseResponse<IEnumerable<ProductResponse>>.SuccessResponse(product));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null)
            {
                return NotFound(BaseResponse<ProductResponse>.FailResponse("Product not found"));
            }
            return Ok(BaseResponse<ProductResponse>.SuccessResponse(product));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var product = await _mediator.Send(new CreateProductCommand(request));
            if (product == null)
            {
                return BadRequest(BaseResponse<ProductResponse>.FailResponse("Failed to create product"));
            }
            return Ok(BaseResponse<ProductResponse>.SuccessResponse(product, "Product created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
        {
            var product = await _mediator.Send(new UpdateProductCommand(id, request));
            if (product == null)
            {
                return NotFound(BaseResponse<ProductResponse>.FailResponse("Product not found"));
            }
            return Ok(BaseResponse<ProductResponse>.SuccessResponse(product, "Product updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            if (!result)
            {
                return NotFound(BaseResponse<bool>.FailResponse("Product not found"));
            }
            return Ok(BaseResponse<bool>.SuccessResponse(true, "Product deleted successfully"));
        }
    }
}
