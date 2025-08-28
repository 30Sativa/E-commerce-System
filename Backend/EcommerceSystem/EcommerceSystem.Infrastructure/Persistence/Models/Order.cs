using System;
using System.Collections.Generic;

namespace EcommerceSystem.Infrastructure.Persistence.Models;

public partial class Order
{
    public int Orderid { get; set; }

    public int Customerid { get; set; }

    public decimal Totalamount { get; set; }

    public string Status { get; set; } = null!;

    public string? Shippingaddress { get; set; }

    public string? Paymentmethod { get; set; }

    public string? Phonenumber { get; set; }

    public DateTime Createdat { get; set; }

    public int? Voucherid { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public virtual Voucher? Voucher { get; set; }
}
