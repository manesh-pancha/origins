using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using project4.Models;
using System.IO;

namespace project4.Controllers
{
    public class stylesController : Controller
    {
        private project_4_RealEntities db = new project_4_RealEntities();

        // GET: styles
        public ActionResult Index()
        {
            var styles = db.styles.Include(s => s.Origin);
            return View(styles.ToList());
        }

        // GET: styles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            styles styles = db.styles.Find(id);
            if (styles == null)
            {
                return HttpNotFound();
            }
            return View(styles);
        }

        // GET: styles/Create
        public ActionResult Create()
        {
            ViewBag.OID = new SelectList(db.Origin, "OID", "about");
            return View();
        }

        // POST: styles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(styles styles)
        {
            if (styles.video == null)
            {
                styles.video = "";
            }
            if (ModelState.IsValid)
            {
                //To get the url of the image brought from the view
                string imagename = Path.GetFileNameWithoutExtension(styles.ImageFile.FileName);
                string extension = Path.GetExtension(styles.ImageFile.FileName);
                //DateTime below to avoid duplicate name of image    DateTime.Now.ToString("yymmssfff") 
                imagename = imagename + extension;
                styles.image = "/Content/Simages/" + imagename;
                //To save it to to the server 
                imagename = Path.Combine(Server.MapPath("/Content/Simages/"), imagename);
                styles.ImageFile.SaveAs(imagename);

                db.styles.Add(styles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //if (styles.image == null)
            //{
            //    styles.image = "~/Content/Style/stylesholder.png";
            //}
            //if (ModelState.IsValid)
            //{
            //    db.styles.Add(styles);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            ViewBag.OID = new SelectList(db.Origin, "OID", "about", styles.OID);
            return View(styles);
        }

        // GET: styles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            styles styles = db.styles.Find(id);
            if (styles == null)
            {
                return HttpNotFound();
            }
            ViewBag.OID = new SelectList(db.Origin, "OID", "about", styles.OID);
            return View(styles);
        }

        // POST: styles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(styles styles)
        {
            if (ModelState.IsValid)
            {
                //To get the url of the image brought from the view
                string imagename = Path.GetFileNameWithoutExtension(styles.ImageFile.FileName);
                string extension = Path.GetExtension(styles.ImageFile.FileName);
                //DateTime below to avoid duplicate name of image    DateTime.Now.ToString("yymmssfff") 
                imagename = imagename + extension;
                styles.image = "/Content/Simages/" + imagename;
                //To save it to to the server 
                imagename = Path.Combine(Server.MapPath("/Content/Simages/"), imagename);
                styles.ImageFile.SaveAs(imagename);
                db.Entry(styles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OID = new SelectList(db.Origin, "OID", "about", styles.OID);
            return View(styles);
        }

        // GET: styles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            styles styles = db.styles.Find(id);
            if (styles == null)
            {
                return HttpNotFound();
            }
            return View(styles);
        }

        // POST: styles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            styles styles = db.styles.Find(id);
            db.styles.Remove(styles);
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
