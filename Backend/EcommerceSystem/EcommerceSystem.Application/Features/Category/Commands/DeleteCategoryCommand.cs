using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EcommerceSystem.Application.Features.Category.Commands
{
    public record DeleteCategoryCommand(int id) : IRequest<bool>;
    
}
