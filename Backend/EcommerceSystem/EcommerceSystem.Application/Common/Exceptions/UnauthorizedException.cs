using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public int StatusCode { get; } = 401;

        public UnauthorizedException(string message) : base(message) { }
    }
}
