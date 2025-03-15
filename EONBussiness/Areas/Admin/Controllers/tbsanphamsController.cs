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
    public class tbsanphamsController : Controller
    {
        public string UploadFile(HttpPostedFileBase upload)
        {
            string fileName = Path.GetFileName(upload.FileName);
            string pathFile = "/files/sanphams/" + DateTime.Now.Year + "/";
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
            return View(db.Value.tbsanphams.OrderByDescending(s => s.ghim).ThenByDescending(s => s.ID).ThenBy(s => s.hienthi).ToList());
        }
        public string getdm(int? id)
        {
            try
            {
                tbdanhmucsp item = db.Value.tbdanhmucsps.Find(id);
                if (item == null)
                {
                    return "";
                }
                return item.tendanhmuc;
            }
            catch
            {
                return "";
            }
        }
        public string CateNew(int? id)
        {
            string html = "";
            List<tbdanhmucsp> lstCate = new List<tbdanhmucsp>();
            lstCate = db.Value.tbdanhmucsps.Where(s => s.hienthi == true && s.idcha != null).ToList();
            int total = lstCate.Count;
            if (id == null)
                html += "<option selected value=''>---Chọn danh mục---</option>";
            else
                html += "<option value=''>---Chọn danh mục---</option>";
            for (int i = 0; i < total; i++)
            {
                if (lstCate[i].ID == id)
                    html += "<option selected value='" + lstCate[i].ID + "'>" + lstCate[i].tendanhmuc + "</option>";
                else
                    html += "<option value='" + lstCate[i].ID + "'>" + lstCate[i].tendanhmuc + "</option>";

            }
            return html;
        }
        public ActionResult Create()
        {
            tbsanpham tb = new tbsanpham();
            return View(tb);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbsanpham tb, HttpPostedFileBase img, HttpPostedFileBase img1)
        {
            try
            {
                if (img != null)
                {
                    tb.hinhanh = UploadFile(img);
                }
                if (img1 != null)
                {
                    tb.hinhanh1 = UploadFile(img1);
                }
                tb.hienthi = true;
                tb.ghim = false;
                tb.viewus = 0;
                tb.ngaycapnhat = DateTime.Now;
                tb.alias = Processing.ConvertToUnSign3(tb.tensp);
                db.Value.tbsanphams.Add(tb);
                db.Value.SaveChanges();
                return Redirect("/admin/tbsanphams");
            }
            catch
            {
                return View(tb);
            }

        }

        public ActionResult Edit(int id)
        {
            tbsanpham tb = new tbsanpham();
            try
            {
                tb = db.Value.tbsanphams.Find(id);
                return View(tb);
            }
            catch
            {
                tb = new tbsanpham();
                return View(tb);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbsanpham tb, HttpPostedFileBase img, HttpPostedFileBase img1)
        {
            try
            {
                if (img != null)
                {
                    tb.hinhanh = UploadFile(img);
                }
                if (img1 != null)
                {
                    tb.hinhanh1 = UploadFile(img1);
                }
                tb.alias = Processing.ConvertToUnSign3(tb.tensp);
                tb.ngaycapnhat = DateTime.Now;
                db.Value.Entry(tb).State = System.Data.Entity.EntityState.Modified;
                db.Value.SaveChanges();
                return Redirect("/admin/tbsanphams");
            }
            catch
            {
                return View(tb);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            tbsanpham tb = db.Value.tbsanphams.Find(id);
            if (tb.hienthi == false)
                tb.hienthi = true;
            else tb.hienthi = false;
            db.Value.Entry(tb).State = EntityState.Modified;
            db.Value.SaveChanges();
            return Redirect("/Admin/tbsanphams");
        }
        [HttpPost]
        public ActionResult Ghim(int id)
        {
            tbsanpham tb = db.Value.tbsanphams.Find(id);
            if (tb.ghim == false)
                tb.ghim = true;
            else tb.ghim = false;
            db.Value.Entry(tb).State = EntityState.Modified;
            db.Value.SaveChanges();
            return Redirect("/Admin/tbsanphams");
        }
    }
}