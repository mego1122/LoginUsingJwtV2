using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginUsingJwtV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IActionResult HandleException(Exception ex, string msg)
        {
            IActionResult ret;

            // Create new exception with generic message        
            ret = StatusCode(
                StatusCodes.Status500InternalServerError, new Exception(msg, ex));

            return ret;
        }
    }
}