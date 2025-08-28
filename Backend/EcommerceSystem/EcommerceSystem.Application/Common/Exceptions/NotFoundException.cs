using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public int StatusCode { get; } = 404;

        public NotFoundException(string message) : base(message) { }
    }
}
