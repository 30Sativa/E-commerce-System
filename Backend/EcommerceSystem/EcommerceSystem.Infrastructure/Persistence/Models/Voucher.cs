using System;
using System.Collections.Generic;

namespace EcommerceSystem.Infrastructure.Persistence.Models;

public partial class Voucher
{
    public int Voucherid { get; set; }

    public string Code { get; set; } = null!;

    public decimal Discount { get; set; }

    public DateTime Expiredat { get; set; }

    public bool Isactive { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
