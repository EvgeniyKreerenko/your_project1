using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class HomeController : Controller
    {
        RestEntities storeDB = new RestEntities();

        public ActionResult Index()
        {
            var restItems = GetTopSellingItems(6);

            return View(restItems);            
        }

        private List<RestItem> GetTopSellingItems(int count)
        {
            return storeDB.RestItems.OrderByDescending(a => a.OrderDetail.Count()).Take(count).ToList();
        }
    }
}