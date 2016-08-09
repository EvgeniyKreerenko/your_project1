using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Restaurant.Models
{
    public class RestEntities:DbContext
    {
        public RestEntities()
            : base("RestEntities")
        {

        }

        public DbSet<RestItem> RestItems { get; set; }
        public DbSet<RestMenu> RestMenus { get; set; }        
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}