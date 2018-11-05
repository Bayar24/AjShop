using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AjShop.Models
{
    public class Subscription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public double CompPercentage { get; set; }

        public double VendorPercentage { get; set; }

        public double TaxPercentage { get; set; }

        public DateTime DateCreated { get; set; }

        [Required()]
        public String Status { get; set; }

    }
}