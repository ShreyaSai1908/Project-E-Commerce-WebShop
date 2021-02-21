using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.ViewModel
{
    public class ProductViewModel
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 1)]
        public string ProductName { get; set; }

        [Required]
        public string Price { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Description { get; set; }

        public List<Product> ProductList { get; set; }
    }
}
