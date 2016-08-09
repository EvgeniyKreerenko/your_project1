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
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;

namespace Restaurant.Controllers
{
    [Authorize(Roles = "admin")]
    public class StoreManagerController : Controller
    {
        private RestEntities db = new RestEntities();

        //
        // GET: /StoreManager/
        public async Task<ActionResult> Index()
        {
            var restitems = db.RestItems.Include(r => r.RestMenu);
            return View(await restitems.ToListAsync());
        }

        // GET: /StoreManager/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestItem restitem = await db.RestItems.FindAsync(id);
            if (restitem == null)
            {
                return HttpNotFound();
            }
            return View(restitem);
        }

        // GET: /StoreManager/Create
        public ActionResult Create()
        {
            ViewBag.RestMenuId = new SelectList(db.RestMenus, "RestMenuId", "Name");
            return View();
        }

        // POST: /StoreManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="RestItemId,RestMenuId,Title,Price,RestItemArtUrl")] RestItem restitem)
        {
            if (ModelState.IsValid)
            {
                db.RestItems.Add(restitem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RestMenuId = new SelectList(db.RestMenus, "RestMenuId", "Name", restitem.RestMenuId);
            return View(restitem);
        }

        // GET: /StoreManager/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestItem restitem = await db.RestItems.FindAsync(id);
            if (restitem == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestMenuId = new SelectList(db.RestMenus, "RestMenuId", "Name", restitem.RestMenuId);
            return View(restitem);           
        }

        // POST: /StoreManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="RestItemId,RestMenuId,Title,Price,RestItemArtUrl")] RestItem restitem)
        {            
            if (ModelState.IsValid)
            {
                if (restitem.RestItemId == 0)
                {
                    db.RestItems.Add(restitem);
                }
                else
                {
                    db.Entry(restitem).State = EntityState.Modified;
                    //db.Entry(restitem).State = restitem.RestItemId == 0 ?
                    //EntityState.Added : EntityState.Modified;
                }
            
                //db.Entry(restitem).State = EntityState.Modified;
                await db.SaveChangesAsync();                
                return RedirectToAction("Index");
            }
            ViewBag.RestMenuId = new SelectList(db.RestMenus, "RestMenuId", "Name", restitem.RestMenuId);
            return View(restitem);
        }

        // GET: /StoreManager/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestItem restitem = await db.RestItems.FindAsync(id);
            if (restitem == null)
            {
                return HttpNotFound();
            }
            return View(restitem);
        }

        // POST: /StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RestItem restitem = await db.RestItems.FindAsync(id);
            db.RestItems.Remove(restitem);
            await db.SaveChangesAsync();
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
