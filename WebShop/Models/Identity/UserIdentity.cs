using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.Identity
{
    public class UserIdentity : IdentityUser
    {
        [Column(TypeName = "NVARCHAR (100)")]
        public string FirstName { get; set; }

        [Column(TypeName = "NVARCHAR (100)")]
        public string LastName { get; set; }

        [Column(TypeName = "NVARCHAR (100)")]
        public string Address { get; set; }

        [Column(TypeName = "NVARCHAR (100)")]
        public string CustomerID { get; set; }
    }
}
