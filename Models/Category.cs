using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoginUsingJwtV2.Models
{
    [Table("Category", Schema = "dbo")]
    public partial class Category
    {
        public int CategoryId { get; set; }

        [Required()]
        [StringLength(30)]
        public string CategoryName { get; set; }
    }
}
