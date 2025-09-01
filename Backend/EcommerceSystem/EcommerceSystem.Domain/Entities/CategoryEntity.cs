using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSystem.Domain.Entities
{
    public class CategoryEntity
    {
        public int Categoryid { get; set; }

        public string Name { get; set; } = null!;
    }
}
