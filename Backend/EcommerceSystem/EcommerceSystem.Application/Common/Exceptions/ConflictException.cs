using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Application.Common.Exceptions
{
    public class ConflictException : Exception
    {
        public int StatusCode { get; } = 409;

        public ConflictException(string message) : base(message) { }
    }
}
