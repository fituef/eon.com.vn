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
    public class tbtmsanphamsController : Controller
    {
        public string UploadFile(HttpPostedFileBase upload)
        {
            string fileName = Path.GetFileName(upload.FileName);
            string pathFile = "/files/danh-muc-sanpham/" + DateTime.Now.Year + "/";
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
            return View(db.Value.tbdanhmucsps.OrderByDescending(s=>s.ID).ThenByDescending(s=>s.hienthi).ToList());
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
        public ActionResult Create()
        {
            tbdanhmucsp tb = new tbdanhmucsp();
            return View(tb);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbdanhmucsp tb, HttpPostedFileBase img)
        {
            try
            {
                if (img != null)
                {
                    tb.hinhanh = UploadFile(img);
                }
                tb.hienthi = true;
                tb.alias = Processing.ConvertToUnSign3(tb.tendanhmuc);
                db.Value.tbdanhmucsps.Add(tb);
                db.Value.SaveChanges();
                return Redirect("/admin/tbtmsanphams");
            }
            catch
            {
                return View(tb);
            }

        }

        public ActionResult Edit(int id)
        {
            tbdanhmucsp tb = new tbdanhmucsp();
            try
            {
                tb = db.Value.tbdanhmucsps.Find(id);
                return View(tb);
            }
            catch
            {
                tb = new tbdanhmucsp();
                return View(tb);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbdanhmucsp tb, HttpPostedFileBase img)
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
                return Redirect("/admin/tbtmsanphams");
            }
            catch
            {
                return View(tb);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            tbdanhmucsp tb = db.Value.tbdanhmucsps.Find(id);
            if (tb.hienthi == false)
                tb.hienthi = true;
            else tb.hienthi = false;
            db.Value.Entry(tb).State = EntityState.Modified;
            db.Value.SaveChanges();
            return Redirect("/Admin/tbtmsanphams");
        }
        [HttpPost]
        public ActionResult Home(int id)
        {
            tbdanhmucsp tb = db.Value.tbdanhmucsps.Find(id);
            if (tb.home == false)
                tb.home = true;
            else tb.home = false;
            db.Value.Entry(tb).State = EntityState.Modified;
            db.Value.SaveChanges();
            return Redirect("/Admin/tbtmsanphams");
        }
        public string CateNew(int? id, bool? check)
        {
            string html = "";
            List<tbdanhmucsp> lstCate = new List<tbdanhmucsp>();
            lstCate = db.Value.tbdanhmucsps.Where(s=>s.hienthi== true && s.idcha ==null).ToList();
            int total = lstCate.Count;
            if (id == null)
                html += "<option selected value=''>---Chọn danh mục---</option>";
            else
                html += "<option value=''>---Chọn danh mục---</option>";
            for (int i = 0; i < total; i++)
            {
                if (check == true && lstCate[i].ID == id.Value)
                    continue;
                if (lstCate[i].ID == id)
                    html += "<option selected value='" + lstCate[i].ID + "'>" + lstCate[i].tendanhmuc + "</option>";
                else
                    html += "<option value='" + lstCate[i].ID + "'>" + lstCate[i].tendanhmuc + "</option>";

            }
            return html;
        }

    }
}