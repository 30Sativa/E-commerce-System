﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Application.DTOs.Responses.ProductImage
{
    public class ProductImageResponse
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsMain { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
