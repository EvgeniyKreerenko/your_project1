using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    [Bind(Exclude="RestItemId")]
    public class RestItem
    {
        [ScaffoldColumn(false)]
        public int RestItemId { get; set; }
        [DisplayName("RestMenu")]
        public int RestMenuId { get; set; }
        [Required(ErrorMessage = "Menu Item is required")]
        [StringLength(160)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000.00, ErrorMessage = "Price must be between 0.01 and 1000.00")]
        public decimal Price { get; set; }
        [DisplayName("Menu Item Art URL")]
        [StringLength(1024)]
        public string RestItemArtUrl { get; set; }
        public virtual RestMenu RestMenu { get; set; }
        public virtual List<OrderDetail> OrderDetail { get; set; }
    }
}