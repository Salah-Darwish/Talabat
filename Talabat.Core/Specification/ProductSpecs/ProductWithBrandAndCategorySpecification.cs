using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification.ProductSpecs
{
    public class ProductWithBrandAndCategorySpecification:BaseSpecifications<Product>
    {
        //  This Constractour will be used to Creating object to Get All Products
        public ProductWithBrandAndCategorySpecification():base()
        {
        Includes.Add(P=>P.Brand);
         Includes.Add(P=>P.Category);   
        }
    }
}
