using EONBussiness.App_Start;
using EONBussiness.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EONBussiness.Areas.Admin.Controllers
{
    [Authorize]
    public class SysAdminController : Controller
    {
        private Lazy<EONBusinessEntities> db = new Lazy<EONBusinessEntities>();

        public ActionResult Index(int? id)
        {
            tbSetup tb = new tbSetup();
            try
            {
                tb = db.Value.tbSetups.FirstOrDefault();
                return View(tb);
            }
            catch
            {
                tb = new tbSetup();
                return View(tb);
            }
        }
        public ActionResult Edit(int? id)
        {
            tbSetup tb = new tbSetup();
            try
            {
                tb = db.Value.tbSetups.FirstOrDefault();
                return View(tb);
            }
            catch
            {
                tb = new tbSetup();
                return View(tb);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbSetup tb)
        {
            try
            {
                db.Value.Entry(tb).State = System.Data.Entity.EntityState.Modified;
                db.Value.SaveChanges();
                return Redirect("/admin/sysadmin");
            }
            catch
            {
                return View(tb);
            }
        }
    }
}