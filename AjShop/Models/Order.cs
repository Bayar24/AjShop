using AjShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AjShop.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderId { get; set; }

        [Required()]
        public string Status { get; set; }

        public DateTime OrderDate { get; set; }

        [StringLength(5)]
        public string ZipCode { get; set; }

        public decimal TotalAmount { get; set; }

        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
        
        public long? CardId { get; set; }
        [ForeignKey("CardId")]
        public virtual Card Card { get; set; }

        [Required()]
        [StringLength(128)]
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}