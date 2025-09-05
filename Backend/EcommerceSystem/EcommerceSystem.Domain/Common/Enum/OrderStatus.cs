﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Domain.Common.Enum
{
    public enum OrderStatus
    {
        Pending = 0,
        Paid = 1,
        Shipping = 2,
        Completed = 3,
        Cancelled = 4
    }
}
