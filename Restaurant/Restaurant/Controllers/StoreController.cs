using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class StoreController : Controller
    {
        RestEntities storeDB = new RestEntities();
        //
        // GET: /Store/
        public ActionResult Index()
        {
            var restMenus = storeDB.RestMenus.ToList();
            
            return View(restMenus);
        }
        //
        // GET: /Store/Browse
        public ActionResult Browse(string restMenu)
        {
            // Retrieve RestMenu and its Associated RestItem from database
            var restMenuModel = storeDB.RestMenus.Include("RestItems").Single(g => g.Name == restMenu);

            return View(restMenuModel);
        }
        //
        // GET: /Store/Details
        public ActionResult Details(int id)
        {
            var restItem = storeDB.RestItems.Find(id);

            return View(restItem);
        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var restMenus = storeDB.RestMenus.ToList();
            return PartialView(restMenus);
        }
	}
}