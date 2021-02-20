using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class OrderHeader
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public string CustomerID { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        [Required]
        public bool PaymentStatus { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
    }
}
