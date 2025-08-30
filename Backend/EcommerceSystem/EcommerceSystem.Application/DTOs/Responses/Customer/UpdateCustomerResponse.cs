using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Application.DTOs.Responses.Customer
{
    public class UpdateCustomerResponse
    {
        public int Customerid { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = "Customer";
    }
}
