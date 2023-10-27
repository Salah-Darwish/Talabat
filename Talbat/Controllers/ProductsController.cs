﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repoisteries.Contract;

namespace Talbat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : APIBasedController
    {
        private readonly IgenericReposity<Product> _productsRepo;

        public ProductsController(IgenericReposity<Product>productsRepo)
        {
            _productsRepo = productsRepo;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products=await _productsRepo.GetAllAsync();
                return Ok(products); 
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            var product =await _productsRepo.GetAsync(id);
            if (product is null)
                return NotFound(new
                {
                  Message="Not found", 
                  StatusCode=404
                }); 
            return Ok(product); 
        }
    }
}