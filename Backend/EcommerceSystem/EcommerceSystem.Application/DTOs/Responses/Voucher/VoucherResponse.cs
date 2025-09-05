using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Application.DTOs.Responses.Voucher
{
    public class VoucherResponse
    {
        public int VoucherId { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal Discount { get; set; }
        public DateTime ExpiredAt { get; set; }
        public bool IsActive { get; set; }
    }
}
