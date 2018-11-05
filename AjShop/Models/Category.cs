using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AjShop.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte CategoryId { get; set; }

        [Required()]
        [StringLength(100, MinimumLength = 2)]
        public string CategoryName { get; set; }
    }
}