using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Application.DTOs.Requests.Order
{
    public class UpdateOrderRequest
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;   // client gửi string
        public string? ShippingAddress { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
