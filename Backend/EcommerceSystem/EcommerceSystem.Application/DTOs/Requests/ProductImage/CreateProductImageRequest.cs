using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Application.DTOs.Requests.ProductImage
{
    public class CreateProductImageRequest
    {
        public string ImageUrl { get; set; } = null!;
        public bool IsMain { get; set; }
    }
}
