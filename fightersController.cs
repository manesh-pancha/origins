using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using project4.Models;
using System.Data.Entity.Validation;

namespace project4.Controllers
{
    public class fightersController : Controller
    {
        private project_4_RealEntities db = new project_4_RealEntities();

        // GET: fighters
        public ActionResult Index()
        {
            //var fighters = db.fighters.Include(f => f.styles);
            var fighters = from m in db.fighters select m;

            return View(fighters);
        }
       



            //old
        //[HttpPost]
        //public ActionResult Likes(int? fighterID)
        //{


        //    if (fighterID == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    fighters fighters = db.fighters.Find(fighterID);

        //    if (fighters == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    int currentlike = (int)fighters.likes;

        //    fighters.likes = currentlike + 1;
        //    if (ModelState.IsValid)
        //        //{
        //        //    db.Entry(fighters).State = EntityState.Modified;

        //        //    db.SaveChanges();
        //        //}

        //        try
        //        {
        //            db.Entry(fighters).State = EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //        catch (DbEntityValidationException ex)
        //        {
        //            foreach (var entityValidationErrors in ex.EntityValidationErrors)
        //            {
        //                foreach (var validationError in entityValidationErrors.ValidationErrors)
        //                {
        //                    Console.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
        //                }
        //            }
        //        }
        
        //fighters = db.fighters.Find(fighterID);

        //    return PartialView("_Indexpartial", fighters);
        //    //return View("index", fighters);
        //    //RedirectToAction("index");

        //}


        ////partial view
        //[HttpGet]
        //public ActionResult _Indexpartial(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    fighters fighters = db.fighters.Find(id);
        //    if (fighters == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return PartialView(fighters);
        //}

        // GET: fighters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fighters fighters = db.fighters.Find(id);
            if (fighters == null)
            {
                return HttpNotFound();
            }
            return View(fighters);
        }

        // GET: fighters/Create
        public ActionResult Create()
        {
            ViewBag.StylesID = new SelectList(db.styles, "StylesID", "about");
            return View();
        }

        // POST: fighters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StylesID,fighterID,about,image,video,records,style,weightclass,recent,ImageFile")] fighters fighters)
        {
            if(fighters.video == null)
            {
                fighters.video = "Id0TSvwMCzg";
            }
      
            if (ModelState.IsValid)
            {
                //To get the url of the image brought from the view
            string imagename = Path.GetFileNameWithoutExtension(fighters.ImageFile.FileName);
            string extension = Path.GetExtension(fighters.ImageFile.FileName);
            //DateTime below to avoid duplicate name of image    DateTime.Now.ToString("yymmssfff") 
            imagename = imagename + extension;
            fighters.image = "/Content/Fimages/" + imagename;
            //To save it to to the server 
            imagename = Path.Combine(Server.MapPath("/Content/Fimages/"), imagename);
            fighters.ImageFile.SaveAs(imagename);

                fighters.likes = 0;
            db.fighters.Add(fighters);
            db.SaveChanges();
            return RedirectToAction("Index");
            }

            ViewBag.StylesID = new SelectList(db.styles, "StylesID", "about", fighters.StylesID);
            return View(fighters);
        }
        //likes function
        //new

            public int? Like(int id)
        {
            fighters updateFighter = db.fighters.Find(id);
            updateFighter.likes += 1;
            db.SaveChanges();
            return updateFighter.likes;
        }

        // GET: fighters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fighters fighters = db.fighters.Find(id);
            if (fighters == null)
            {
                return HttpNotFound();
            }
            ViewBag.StylesID = new SelectList(db.styles, "StylesID", "about", fighters.StylesID);
            return View(fighters);
        }

        // POST: fighters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(fighters fighters)
        {
            if (ModelState.IsValid)
            {
                //To get the url of the image brought from the view
                string imagename = Path.GetFileNameWithoutExtension(fighters.ImageFile.FileName);
                string extension = Path.GetExtension(fighters.ImageFile.FileName);
                //DateTime below to avoid duplicate name of image    DateTime.Now.ToString("yymmssfff") 
                imagename = imagename + extension;
                fighters.image = "/Content/Fimages/" + imagename;
                //To save it to to the server 
                imagename = Path.Combine(Server.MapPath("/Content/Fimages/"), imagename);
                fighters.ImageFile.SaveAs(imagename);

                db.Entry(fighters).State = EntityState.Modified; ;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //{
            //    db.Entry(fighters).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            ViewBag.StylesID = new SelectList(db.styles, "StylesID", "about", fighters.StylesID);
            return View(fighters);
        }

        // GET: fighters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fighters fighters = db.fighters.Find(id);
            if (fighters == null)
            {
                return HttpNotFound();
            }
            return View(fighters);
        }

        // POST: fighters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fighters fighters = db.fighters.Find(id);
            db.fighters.Remove(fighters);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //AJAX TESTING
        public String Test()
        {
            return ("Ajax is working !");
        }
        //END AJAX TESTING

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
