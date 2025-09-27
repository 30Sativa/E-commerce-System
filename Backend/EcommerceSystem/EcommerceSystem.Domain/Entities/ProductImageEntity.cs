using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Domain.Entities
{
    public class ProductImageEntity
    {
        public int Id { get; set; }          // map -> Imageid
        public int ProductId { get; set; }   // map -> Productid
        public string ImageUrl { get; set; } = null!;
        public bool IsMain { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
