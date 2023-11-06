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

        public ProductsController(IgenericReposity<Product>productsRepo,IMapper mapper  )
        {
            _productsRepo = productsRepo;
           _mapper = mapper;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategorySpecification(); 
            var products=await _productsRepo.GetAllWithSpecAsync(spec);
                return Ok(_mapper.Map<IEnumerable<Product>,IEnumerable<ProductToReturnDto>>(products)); 
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
    }
}
