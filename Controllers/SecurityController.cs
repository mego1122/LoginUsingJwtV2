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
    public class SecurityController : BaseApiController
    {
        public SecurityController(LoginDbContext _db, JwtSettings settings)
        {
            db = _db;
            _settings = settings;
        }

        private JwtSettings _settings=null;
        private  LoginDbContext db;
     

        [HttpPost("login")]
        public IActionResult Login([FromBody]AppUser user)
        {
            IActionResult ret = null;
            AppUserAuth Auth=new AppUserAuth();
            SecurityManager mgr=new SecurityManager(db,_settings);

           Auth= mgr.ValidateUser(user);
           if (Auth.IsAuthenticated)
           {
               ret = StatusCode(StatusCodes.Status200OK, Auth);
           }
           else
           {
               ret = StatusCode(StatusCodes.Status404NotFound, "Invalid User Name/Password");
           }

           return ret;
        }




    }
}