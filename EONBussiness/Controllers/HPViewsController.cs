using EONBussiness.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EONBussiness.Controllers
{
    public class HPViewsController : Controller
    {
        private Lazy<EONBusinessEntities> db = new Lazy<EONBusinessEntities>();
        public ActionResult Index()
        {
            List<tbBanner> items = new List<tbBanner>();
            items = db.Value.tbBanners.Where(s=>s.hienthi == true).ToList();
            return PartialView(items);
        }
        public ActionResult cateproduct()
        {
            List<tbdanhmucsp> items = new List<tbdanhmucsp>();
            items = db.Value.tbdanhmucsps.Where(s=>s.hienthi == true && s.home == true).Take(6).OrderByDescending(s=>s.ID).ToList();
            return PartialView(items);
        }
    }
}