using EcommerceSystem.Application.Common;
using EcommerceSystem.Application.DTOs.Requests.ProductImage;
using EcommerceSystem.Application.DTOs.Responses.ProductImage;
using EcommerceSystem.Application.Features.ProductImage.Commands;
using EcommerceSystem.Application.Features.ProductImage.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductImagesController(IMediator mediator) => _mediator = mediator;

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            var data = await _mediator.Send(new GetImagesByProductQuery(productId));
            return Ok(BaseResponse<IEnumerable<ProductImageResponse>>.SuccessResponse(data));
        }

        [HttpPost("product/{productId}")]
        public async Task<IActionResult> Add(int productId, [FromBody] CreateProductImageRequest req)
        {
            var img = await _mediator.Send(new AddProductImageCommand(productId, req));
            return Ok(BaseResponse<ProductImageResponse>.SuccessResponse(img, "Image added"));
        }

        [HttpDelete("{imageId}")]
        public async Task<IActionResult> Delete(int imageId)
        {
            var ok = await _mediator.Send(new DeleteProductImageCommand(imageId));
            if (!ok) return NotFound(BaseResponse<bool>.FailResponse("Image not found"));
            return Ok(BaseResponse<bool>.SuccessResponse(true,"Deleted Successfully"));
        }

        [HttpPatch("product/{productId}/{imageId}/set-main")]
        public async Task<IActionResult> SetMain(int productId, int imageId)
        {
            var ok = await _mediator.Send(new SetMainProductImageCommand(productId, imageId));
            if (!ok) return NotFound(BaseResponse<bool>.FailResponse("Image not found"));
            return Ok(BaseResponse<bool>.SuccessResponse(true, "Set as main successfully"));
        }
    }
}
