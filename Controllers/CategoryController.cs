using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginUsingJwtV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginUsingJwtV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        public CategoryController(LoginDbContext _db)
        {
            db = _db;
        }

       private LoginDbContext db;
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<Category> list = new List<Category>();
            try
            {
                //using (var db = new LoginDbContext())
                //{
                    if (db.Categories.Count() > 0)
                    {
                        // NOTE: Declare 'list' outside the using to avoid 
                        // it being disposed before it is returned.
                        list = db.Categories.OrderBy(p => p.CategoryName).ToList();
                        ret = StatusCode(StatusCodes.Status200OK, list);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status404NotFound,
                            "Can't Find Categories");
                    }
                //}
            }
            catch (Exception ex)
            {
                ret = HandleException(ex,"Exception trying to get all Categories");
            }
            return ret;
        }
    }
}