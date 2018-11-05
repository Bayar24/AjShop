using AjShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AjShop.Models
{
    public class Card
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CardId { get; set; }

        [Required()]
        [StringLength(16)]
        public string CardNumber { get; set; }

        [Required()]
        [StringLength(50, MinimumLength =2)]
        public string CardHolderName { get; set; }

        public int ExpYear { get; set; }

        public byte ExpMonth { get; set; }

        [Required()]
        [StringLength(3)]
        public string CVV { get; set; }

        [Required()]
        [StringLength(5)]
        public string ZipCode { get; set; }

        [Required()]
        public string CardType { get; set; }

        [Required()]
        [StringLength(128)]
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }
    }
}