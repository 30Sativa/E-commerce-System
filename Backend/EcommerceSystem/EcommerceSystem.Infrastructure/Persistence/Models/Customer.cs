using System;
using System.Collections.Generic;

namespace EcommerceSystem.Infrastructure.Persistence.Models;

public partial class Customer
{
    public int Customerid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Passwordhash { get; set; }

    public string? Googleid { get; set; }

    public string Authprovider { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
