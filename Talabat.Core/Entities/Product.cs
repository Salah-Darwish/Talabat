using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public  decimal Price { get; set; }
        public  int BrandId { get; set; } // Forgien Key Column  => ProductBrand
      public  int CategoryId { get; set; } // Forgien Key Column  => ProductCategory
        // Navigational Properity -> ProductBrand
        public ProductBrand Brand { get; set; }

        // Navigational Properity -> ProductCategory
        public ProductCategory Category { get; set; }
    }
}
