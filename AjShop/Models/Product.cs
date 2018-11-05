using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AjShop.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }

        [Required()]
        [StringLength(100, MinimumLength = 2)]
        public string ProductName { get; set; }

        [Required()]
        [StringLength(200, MinimumLength = 2)]
        public string ProductDescription { get; set; }

        public byte CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public decimal Price { get; set; }

        public int UnitInStock { get; set; }

        public DateTime Created { get; set; }
    }
}