using EcommerceSystem.Application.Common;
using EcommerceSystem.Application.DTOs.Requests.Category;
using EcommerceSystem.Application.DTOs.Responses.Category;
using EcommerceSystem.Application.Features.Category.Commands;
using EcommerceSystem.Application.Features.Category.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(BaseResponse<IEnumerable<CategoryResponse>>.SuccessResponse(categories, "Get all categories success"));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (category == null)
            {
                return NotFound(BaseResponse<CategoryResponse>.FailResponse("Category not found"));
            }
            return Ok(BaseResponse<CategoryResponse>.SuccessResponse(category, "Get category by id success"));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            if (!result)
            {
                return NotFound(BaseResponse<bool>.FailResponse("Category not found"));
            }
            return Ok(BaseResponse<bool>.SuccessResponse(result, "Delete category success"));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryRequest category)
        {
            var updatedCategory = await _mediator.Send(new UpdateCategoryCommand(id, category));
            if (updatedCategory == null)
            {
                return NotFound(BaseResponse<CategoryResponse>.FailResponse("Category not found"));
            }
            return Ok(BaseResponse<CategoryResponse>.SuccessResponse(updatedCategory, "Update category success"));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequest category)
        {
            var createdCategory = await _mediator.Send(new CreateCategoryCommand(category));
            return Ok(BaseResponse<CategoryResponse>.SuccessResponse(createdCategory, "Create category success"));
        }
    }
}
