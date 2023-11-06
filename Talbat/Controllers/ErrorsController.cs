using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talbat.Errors;

namespace Talbat.Controllers
{
    [Route("Errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)] 
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            return NotFound(new ResponsiApi(code));
        }
    }
}
