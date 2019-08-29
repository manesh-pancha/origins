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
// needed for path

namespace project4.Controllers
{
    public class OriginsController : Controller
    {
        private project_4_RealEntities db = new project_4_RealEntities();

        // GET: Origins
        public ActionResult Index()
        {
            return View(db.Origin.ToList());
        }

        // GET: Origins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Origin origin = db.Origin.Find(id);
            if (origin == null)
            {
                return HttpNotFound();
            }
            return View(origin);
        }

        // GET: Origins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Origins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "OID,about,image,country")] Origin origin)
        //{
        //    if (origin.image == null)
        //    {
        //        origin.image = "~/Content/Origin/world.jpg";
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        db.Origin.Add(origin);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(origin);
        //}
        public ActionResult Create([Bind(Include = "about,image,country,ImageFile")]Origin origin)
        {


            if (ModelState.IsValid)
            {
                //To get the url of the image brought from the view
                string imagename = Path.GetFileNameWithoutExtension(origin.ImageFile.FileName);
                string extension = Path.GetExtension(origin.ImageFile.FileName);
                //DateTime below to avoid duplicate name of image    DateTime.Now.ToString("yymmssfff") 
                imagename = imagename + extension;
                origin.image = "/Content/Oimages/" + imagename;
                //To save it to to the server 
                imagename = Path.Combine(Server.MapPath("/Content/Oimages/"), imagename);
                origin.ImageFile.SaveAs(imagename);

                db.Origin.Add(origin);
                db.SaveChanges();
                return RedirectToAction("Index");
                ;
            }

            return View(origin);
        }

        // GET: Origins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Origin origin = db.Origin.Find(id);
            if (origin == null)
            {
                return HttpNotFound();
            }
            return View(origin);
        }

        // POST: Origins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Origin origin)
        {
            if (ModelState.IsValid)
            {
               
                    //To get the url of the image brought from the view
                    string imagename = Path.GetFileNameWithoutExtension(origin.ImageFile.FileName);
                    string extension = Path.GetExtension(origin.ImageFile.FileName);
                    //DateTime below to avoid duplicate name of image    DateTime.Now.ToString("yymmssfff") 
                    imagename = imagename + extension;
                    origin.image = "/Content/Oimages/" + imagename;
                    //To save it to to the server 
                    imagename = Path.Combine(Server.MapPath("/Content/Oimages/"), imagename);
                    origin.ImageFile.SaveAs(imagename);

                db.Entry(origin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(origin);
        }

        // GET: Origins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Origin origin = db.Origin.Find(id);
            if (origin == null)
            {
                return HttpNotFound();
            }
            return View(origin);
        }

        // POST: Origins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Origin origin = db.Origin.Find(id);
            db.Origin.Remove(origin);
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
