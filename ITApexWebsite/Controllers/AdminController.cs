using ITApexWebsite.Models;
using ITApexWebsite.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ITApexWebsite.Controllers
{
    public class AdminController : Controller
    {
        DbITApexEntities db = new DbITApexEntities();

        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCategory() 
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<tblCategory>().GetAllRecords();
            foreach(var item in cat)
            {
                list.Add(new SelectListItem { Value = item.c_Id.ToString(), Text = item.c_name }); 
            }
            return list;
        }

        // GET: Admin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tblAdmin admin)
        {
            var verify = db.tblAdmins.Where(a => a.a_email.Equals(admin.a_email) && a.a_pass.Equals(admin.a_pass)).SingleOrDefault();
            if (verify == null)
            {
                ViewBag.error = "Email or password is incorrect";
                return View();

            }
            else
            {
                Session["adminId"] = verify.a_Id;
                return RedirectToAction("Dashboard");
            }
            
        }

        public ActionResult Dashboard()
        {
            if (Session["adminId"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult Categories()
        {
            List<tblCategory> allcategories = _unitOfWork.GetRepositoryInstance<tblCategory>().GetAllRecordsIQueryable().Where(i => i.isdelete == false).ToList();
            return View(allcategories);
        
        }

        public ActionResult AddCategories()
        {
            return UpdateCategories(0);
        }

        public ActionResult CategoryEdit(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<tblCategory>().GetFirstorDefault(catId));
        }

        [HttpPost]
        public ActionResult CategoryEdit(tblCategory tbl)
        {
            _unitOfWork.GetRepositoryInstance<tblCategory>().Update(tbl);
            return RedirectToAction("Categories");
        }


        public ActionResult UpdateCategories(int categoryId)
        {
            CategoryDetail cd;
            if (categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<tblCategory>().GetFirstorDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategories", cd);

        }

        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<tblProduct>().GetProduct());
        }

        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<tblProduct>().GetFirstorDefault(productId));
        }

        [HttpPost]
        public ActionResult ProductEdit(tblProduct tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImage/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.p_image = file != null ? pic : tbl.p_image;

            tbl.p_modifiedDate = DateTime.Now;

            _unitOfWork.GetRepositoryInstance<tblProduct>().Update(tbl);
            return RedirectToAction("Product");
        }

        public ActionResult ProductAdd()
        {

            ViewBag.CategoryList = GetCategory();
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(tblProduct tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImage/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.p_image = pic;

            tbl.p_createdDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<tblProduct>().Add(tbl);
            return RedirectToAction("Product");
        }

        

    }
}