using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models
{
    public partial class RestMenu
    {
        public int RestMenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RestItem> RestItems { get; set; }
    }
}