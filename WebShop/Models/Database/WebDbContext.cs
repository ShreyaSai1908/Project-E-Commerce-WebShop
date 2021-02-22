using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebShop.Models.Identity;

namespace WebShop.Models.Database
{
    public class WebDbContext : IdentityDbContext<UserIdentity>
    {
        //ctor
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options) { }
        //DbSet
        public DbSet<Product> GetProductList { get; set; } //Name of Database Table
        public DbSet<OrderHeader> OrderHeader { get; set; } //Name of Database Table
        public DbSet<OrderDetails> OrderDetails { get; set; } //Name of Database Table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}