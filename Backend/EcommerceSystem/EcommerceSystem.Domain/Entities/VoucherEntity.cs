using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Domain.Entities
{
    public class VoucherEntity 
    {
        public int VoucherId { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal Discount { get; set; }   // có thể là % (10 nghĩa là giảm 10%)
        public DateTime ExpiredAt { get; set; }
        public bool IsActive { get; set; }
    }
}
