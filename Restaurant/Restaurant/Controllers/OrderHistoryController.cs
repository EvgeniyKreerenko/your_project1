using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderHistoryController : Controller
    {
        private RestEntities db = new RestEntities();

        // GET: /OrderHistory/
        public ActionResult Index()
        {
            var orderdetails = db.OrderDetails.Include(o => o.Order).Include(o => o.RestItem); //.Include(o=>o.OrderId)
            return View(orderdetails.ToList());
        }

        // GET: /OrderHistory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderdetail = db.OrderDetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // GET: /OrderHistory/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username");
            //ViewBag.OrderDetailId = new SelectList(db.Orders, "OrderDate", "Username");
            ViewBag.RestItemId = new SelectList(db.RestItems, "RestItemId", "Title");
            return View();
        }

        // POST: /OrderHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="OrderDetailId,OrderId,RestItemId,Quantity,UnitPrice")] OrderDetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orderdetail.OrderId);
            //ViewBag.OrderDetailId = new SelectList(db.Orders, "OrderDate", "Username",orderdetail.OrderDetailId);
            ViewBag.RestItemId = new SelectList(db.RestItems, "RestItemId", "Title", orderdetail.RestItemId);
            return View(orderdetail);
        }

        // GET: /OrderHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderdetail = db.OrderDetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orderdetail.OrderId);            
            ViewBag.RestItemId = new SelectList(db.RestItems, "RestItemId", "Title", orderdetail.RestItemId);
            return View(orderdetail);
        }

        // POST: /OrderHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="OrderDetailId,OrderId,RestItemId,Quantity,UnitPrice")] OrderDetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orderdetail.OrderId);            
            ViewBag.RestItemId = new SelectList(db.RestItems, "RestItemId", "Title", orderdetail.RestItemId);
            return View(orderdetail);
        }

        // GET: /OrderHistory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderdetail = db.OrderDetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // POST: /OrderHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderdetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderdetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
