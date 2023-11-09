using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repoisteries.Contract;
using Talabat.Core.Specification;
using Talabat.Core.Specification.ProductSpecs;
using Talbat.Dtos;
using Talbat.Errors;

namespace Talbat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : APIBasedController
    {
        private readonly IgenericReposity<Product> _productsRepo;
        private readonly IMapper _mapper;
        private readonly IgenericReposity<ProductBrand> _brandsRepo;
        private readonly IgenericReposity<ProductCategory> _categoriesRepo;

        public ProductsController(IgenericReposity<Product>productsRepo,IMapper mapper,IgenericReposity<ProductBrand>brandsRepo,IgenericReposity<ProductCategory>categoriesRepo  )
        {
            _productsRepo = productsRepo;
           _mapper = mapper;
           _brandsRepo = brandsRepo;
            _categoriesRepo = categoriesRepo;
        }
        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategorySpecification(); 
            var products=await _productsRepo.GetAllWithSpecAsync(spec);
                return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products)); 
        }
        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProducts(int id)
        {
            var spec=new ProductWithBrandAndCategorySpecification(id);
            var product =await _productsRepo.GetWithSpecAsync(spec);
            if (product is null)
                return NotFound(new ResponsiApi(404));
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product)); 
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var brands = await _brandsRepo.GetAllAsync();
            return Ok(brands); 
        }
        [HttpGet("Categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategories()
        {
            var categories= await _categoriesRepo.GetAllAsync();
            return Ok(categories);
        }
    }
}
