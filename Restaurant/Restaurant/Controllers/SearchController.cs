using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class SearchController : Controller
    {
        private RestEntities db = new RestEntities();

        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string name)
        {
            var restItems = db.RestItems.Where(a => a.Title.Contains(name)).ToList();
            if (restItems.Count <= 0)
            {
                return HttpNotFound();
            }
            return View(restItems);
        }
    }
}
