using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginUsingJwtV2.Models
{
    public class AppUserAuth
    {
        public AppUserAuth():base()
        {
            UserName = "Not Authorized";
            BearerToken = string.Empty;
        }

        public string UserName { get; set; }
        public string BearerToken { get; set; }
        public bool IsAuthenticated { get; set; }

        public List<AppUserClaims> Claims { get; set; }

        public bool CanAccessProducts { get; set; } 
              
        public bool CanAddProduct { get; set; }
               
        public bool CanSaveProduct { get; set; }
        public bool CanAccessCategories { get; set; }
            
        public bool CanAddCategory { get; set; }
               

    }
}
