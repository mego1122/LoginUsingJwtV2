using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoginUsingJwtV2.Models
{
    [Table("UserClaim", Schema = "Security")]
    public class AppUserClaims
    {
        [Required()]
        [Key()]
        public Guid ClaimId { get; set; }

        [Required()]
         public Guid UserId { get; set; }


        [Required()]
          public string ClaimType { get; set; }


          [Required()]
          public string ClaimValue { get; set; }
    }
}
