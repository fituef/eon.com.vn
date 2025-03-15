using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EONBussiness.DbContext;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using EONBussiness.App_Start;

namespace EONBussiness.Areas.Admin.Controllers
{
    [Authorize]
    public class tbdmtintucsController : Controller
    {
        public string UploadFile(HttpPostedFileBase upload)
        {
            string fileName = Path.GetFileName(upload.FileName);
            string pathFile = "/files/DANHMUCTINTUC/" + DateTime.Now.Year + "/";
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
            return View(db.Value.tbdanhmuctts.OrderByDescending(s => s.ID).ThenBy(s => s.hienthi).ToList());
        }
        public string getdm(int? id)
        {
            return db.Value.tbdanhmuctts.Find(id).tendanhmuc;
        }
        public ActionResult Create()
        {
            tbdanhmuctt tb = new tbdanhmuctt();
            return View(tb);
        }
        [HttpPost]
        public ActionResult Create(tbdanhmuctt tb, HttpPostedFileBase img)
        {
            try
            {
                if (img != null)
                {
                    tb.hinhanh = UploadFile(img);
                }
                tb.hienthi = true;
                tb.alias = Processing.ConvertToUnSign3(tb.tendanhmuc);
                db.Value.tbdanhmuctts.Add(tb);
                db.Value.SaveChanges();
                return Redirect("/admin/tbdmtintucs");
            }
            catch
            {
                return View(tb);
            }

        }

        public ActionResult Edit(int id)
        {
            tbdanhmuctt tb = new tbdanhmuctt();
            try
            {
                tb = db.Value.tbdanhmuctts.Find(id);
                return View(tb);
            }
            catch
            {
                tb = new tbdanhmuctt();
                return View(tb);
            }
        }
        [HttpPost]
        public ActionResult Edit(tbdanhmuctt tb, HttpPostedFileBase img)
        {
            try
            {
                if (img != null)
                {
                    tb.hinhanh = UploadFile(img);
                }
                tb.alias = Processing.ConvertToUnSign3(tb.tendanhmuc);
                db.Value.Entry(tb).State = System.Data.Entity.EntityState.Modified;
                db.Value.SaveChanges();
                return Redirect("/admin/tbdmtintucs");
            }
            catch
            {
                return View(tb);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            tbdanhmuctt tb = db.Value.tbdanhmuctts.Find(id);
            if (tb.hienthi == false)
                tb.hienthi = true;
            else tb.hienthi = false;
            db.Value.Entry(tb).State = EntityState.Modified;
            db.Value.SaveChanges();
            return Redirect("/Admin/tbdmtintucs");
        }
    }
}