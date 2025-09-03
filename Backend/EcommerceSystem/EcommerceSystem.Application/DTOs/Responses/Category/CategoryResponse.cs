using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Application.DTOs.Responses.Category
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
    }
}
