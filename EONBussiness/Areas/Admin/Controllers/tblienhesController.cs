using EONBussiness.App_Start;
using EONBussiness.DbContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EONBussiness.Areas.Admin.Controllers
{
    [Authorize]
    public class tblienhesController : Controller
    {
        private Lazy<EONBusinessEntities> db = new Lazy<EONBusinessEntities>();
        public ActionResult Index()
        {
            return View(db.Value.tbLienHes.OrderByDescending(s => s.ID).ThenByDescending(s=>s.ngaygui).ThenBy(s => s.xuly).ToList());
        }

        public ActionResult Edit(int id)
        {
            tbLienHe tb = new tbLienHe();
            try
            {
                tb = db.Value.tbLienHes.Find(id);
                return View(tb);
            }
            catch
            {
                tb = new tbLienHe();
                return View(tb);
            }
        }
        [HttpPost]
        public ActionResult Edit(tbLienHe tb, HttpPostedFileBase img)
        {
            try
            {
                tbLienHe item = new tbLienHe();
                item = db.Value.tbLienHes.Find(tb.ID);
                item.xuly = true;
                item.noidungxl = tb.noidungxl;
                item.ngayxuly = DateTime.Now;
                db.Value.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.Value.SaveChanges();
                return Redirect("/admin/tblienhes");
            }
            catch
            {
                return View(tb);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            tbLienHe tb = db.Value.tbLienHes.Find(id);
            if (tb.xuly == false)
            {
                tb.xuly = true;
                tb.ngayxuly = DateTime.Now;
            }    
            else
            {
                tb.xuly = false;
                tb.ngayxuly = null;
            }
            db.Value.Entry(tb).State = EntityState.Modified;
            db.Value.SaveChanges();
            return Redirect("/Admin/tblienhes");
        }
    }
}