using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.DTOs.Requests.Category;
using EcommerceSystem.Application.DTOs.Responses.Category;
using MediatR;

namespace EcommerceSystem.Application.Features.Category.Commands
{
    public record UpdateCategoryCommand(int id, CategoryRequest Category) : IRequest<CategoryResponse>;

}
