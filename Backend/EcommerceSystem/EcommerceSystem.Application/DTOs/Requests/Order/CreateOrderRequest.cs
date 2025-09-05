using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Application.DTOs.Requests.Order
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int? VoucherId { get; set; }

        public List<CreateOrderItemDto> Items { get; set; } = new();
    }


    public class CreateOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
