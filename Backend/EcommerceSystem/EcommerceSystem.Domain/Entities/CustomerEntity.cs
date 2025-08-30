using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Domain.Entities
{
    public class CustomerEntity
    {
        public int Id { get; set; }                 
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PasswordHash { get; set; }
        public string? GoogleId { get; set; }
        public string AuthProvider { get; set; } = "Local";
        public string Role { get; set; } = "Customer";
        public DateTime CreatedAt { get; set; }
    }
}
