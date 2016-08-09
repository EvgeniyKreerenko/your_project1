using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Restaurant.Models;

namespace Restaurant.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int RestItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        //public System.DateTime DateCreated { get; set; }
        public virtual RestItem RestItem { get; set; }
        public virtual Order Order { get; set; }
    }
}