using System;
using System.Collections.Generic;

namespace EcommerceSystem.Infrastructure.Persistence.Models;

public partial class Productimage
{
    public int Imageid { get; set; }

    public int Productid { get; set; }

    public string Imageurl { get; set; } = null!;

    public bool Ismain { get; set; }

    public DateTime Createdat { get; set; }

    public virtual Product Product { get; set; } = null!;
}
