using EONBussiness.App_Start;
using EONBussiness.DbContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EONBussiness.Areas.Admin.Controllers
{
    [Authorize]
    public class tbtuyendungsController : Controller
    {
        public string UploadFile(HttpPostedFileBase upload)
        {
            string fileName = Path.GetFileName(upload.FileName);
            string pathFile = "/files/tuyendung/" + DateTime.Now.Year + "/";
            var versions = new Dictionary<string, string>();
            fileName = Processing.UrlImages(fileName);
            bool exsits = System.IO.Directory.Exists(Server.MapPath(pathFile));
            if (!exsits)
                System.IO.Directory.CreateDirectory(Server.MapPath(pathFile));
            var path = Path.Combine(Server.MapPath(pathFile), fileName);
            upload.SaveAs(path);
            return pathFile + fileName;
        }
        private Lazy<EONBusinessEntities> db = new Lazy<EONBusinessEntities>();
        public ActionResult Index()
        {
            return View(db.Value.tbtuyendungs.OrderByDescending(s => s.ghim).ThenByDescending(s=>s.ID).ThenBy(s => s.hienthi).ToList());
        } 
        public ActionResult Create()
        {
            tbtuyendung tb = new tbtuyendung();
            return View(tb);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbtuyendung tb, HttpPostedFileBase img)
        {
            try
            {
                if (img != null)
                {
                    tb.hinhanh = UploadFile(img);
                }
                tb.hienthi = true;
                tb.ghim = false;
                tb.viewus = 0;
                tb.ngaycapnhat = DateTime.Now;
                tb.alias = Processing.ConvertToUnSign3(tb.tentd);
                db.Value.tbtuyendungs.Add(tb);
                db.Value.SaveChanges();
                return Redirect("/admin/tbtuyendungs");
            }
            catch
            {
                return View(tb);
            }

        }

        public ActionResult Edit(int id)
        {
            tbtuyendung tb = new tbtuyendung();
            try
            {
                tb = db.Value.tbtuyendungs.Find(id);
                return View(tb);
            }
            catch
            {
                tb = new tbtuyendung();
                return View(tb);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbtuyendung tb, HttpPostedFileBase img)
        {
            try
            {
                if (img != null)
                {
                    tb.hinhanh = UploadFile(img);
                }
                tb.alias = Processing.ConvertToUnSign3(tb.tentd);
                tb.ngaycapnhat = DateTime.Now;
                db.Value.Entry(tb).State = System.Data.Entity.EntityState.Modified;
                db.Value.SaveChanges();
                return Redirect("/admin/tbtuyendungs");
            }
            catch
            {
                return View(tb);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            tbtuyendung tb = db.Value.tbtuyendungs.Find(id);
            if (tb.hienthi == false)
                tb.hienthi = true;
            else tb.hienthi = false;
            db.Value.Entry(tb).State = EntityState.Modified;
            db.Value.SaveChanges();
            return Redirect("/Admin/tbtuyendungs");
        }
        [HttpPost]
        public ActionResult Ghim(int id)
        {
            tbtuyendung tb = db.Value.tbtuyendungs.Find(id);
            if (tb.ghim == false)
                tb.ghim = true;
            else tb.ghim = false;
            db.Value.Entry(tb).State = EntityState.Modified;
            db.Value.SaveChanges();
            return Redirect("/Admin/tbtuyendungs");
        }
    }
}