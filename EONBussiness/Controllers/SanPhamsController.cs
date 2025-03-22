using EONBussiness.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EONBussiness.Controllers
{
    public class SanPhamsController : Controller
    {
        private Lazy<EONBusinessEntities> db = new Lazy<EONBusinessEntities>();
        public ActionResult Index()
        {
            return View(db.Value.tbsanphams.Where(s => s.hienthi == true).OrderByDescending(s => s.ghim).ThenByDescending(s => s.ID).ThenBy(s => s.hienthi).ToList());
        }
        public ActionResult Details(int? id)
        {
            tbsanpham item = new tbsanpham();
            try
            {
                if (id == null)
                {
                    item = db.Value.tbsanphams.Where(s => s.hienthi == true).FirstOrDefault();
                    return View(item);
                }
                else
                {
                    item = db.Value.tbsanphams.Where(s => s.hienthi == true && s.ID == id).FirstOrDefault();
                    if (item == null)
                        return View(db.Value.tbsanphams.Where(s => s.hienthi == true).FirstOrDefault());
                    return View(item);
                }
            }
            catch
            {
                return Redirect("/error");
            }
        }
        public ActionResult GetSPTT(int dm, int sp)
        {
            List<tbsanpham> lst = new List<tbsanpham>();
            try
            {
               
                lst = db.Value.tbsanphams.Where(s=>s.danhmucid ==dm && s.ID !=sp).Take(4).ToList();
                return PartialView(lst);
            }
            catch
            {
                return PartialView(lst = new List<tbsanpham>());
            }
        }
    }
}