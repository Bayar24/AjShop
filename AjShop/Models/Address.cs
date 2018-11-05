using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AjShop.Models
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        [Required()]
        [StringLength(100, MinimumLength =2)]
        public string Street { get; set; }

        [Required()]
        [StringLength(30, MinimumLength = 2)]
        public string City { get; set; }

        [Required()]
        [StringLength(2)]
        public string State { get; set; }

        [Required()]
        [StringLength(5)]
        public string ZipCode { get; set; }

        [Required()]
        [StringLength(30, MinimumLength = 3)]
        public string Country { get; set; }
}
}