﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoginUsingJwtV2.Models
{
    [Table("Product", Schema = "dbo")]
    public partial class Product
    {
        public int ProductId { get; set; }

        [Required()]
        [StringLength(80)]
        public string ProductName { get; set; }

        public DateTime? IntroductionDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [Required()]
        [StringLength(255)]
        public string Url { get; set; }

        public int? CategoryId { get; set; }
    }
}
