using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;
using Talbat.Errors;

namespace Talbat.Controllers
{

    public class BuggyController : APIBasedController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _context.Products.Find(100);
            if (product is null)
                return NotFound(new ResponsiApi(404));
            return Ok(product);
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = _context.Products.Find(100);
            var productToReturn = product.ToString(); //Will Throw Exception [NullRefernceException]
            return Ok(productToReturn);
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ResponsiApi(400));
        }
        [HttpGet("badrequest/{id}")]// Get :api/Buggy/badrequest/five
        public ActionResult GetBadRequest(int id) // Validation Error
        {
            return Ok(); 
        }
        [HttpGet("unauthotized")]
        public ActionResult GetUnauthorizedError()
        {
            return Unauthorized(new ResponsiApi(401));
        }
    }
}
