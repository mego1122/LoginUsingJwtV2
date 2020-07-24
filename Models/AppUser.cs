using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace LoginUsingJwtV2.Models
{
    [Table("User", Schema = "Security")]
    public partial class AppUser
    {
       [Required()]
       [Key()]
        public Guid UserId { get; set; }

        [Required()]
        [StringLength(255)]
        public string UserNme { get; set; }
        [Required()]
        [StringLength(255)]
        public string UserPasswordNme { get; set; }
    }
}
