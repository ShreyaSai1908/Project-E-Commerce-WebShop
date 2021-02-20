using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailID { get; set; }

        [Required]
        public OrderHeader OrderHeader { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public int ProductQuantity { get; set; }

    }
}
