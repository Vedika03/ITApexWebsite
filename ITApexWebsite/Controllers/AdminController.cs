using ITApexWebsite.Models;
using ITApexWebsite.Models.Home;
using ITApexWebsite.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace ITApexWebsite.Controllers
{
    public class AdminController : Controller
    {
        DbITApexEntities db = new DbITApexEntities();

        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        
        //get category list
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
        //admin login
        [HttpGet]
        
        public ActionResult Login()
        {
            return View();
        }


        //admin login post
        [HttpPost]
        public ActionResult Login(tblAdmin admin)
        {
            Session["adminName"] = admin.a_email.ToString();
            if (Session["adminName"] != null)
            {
                Session["adminName"] = admin.a_email.ToString();
            }
            var verify = db.tblAdmins.Where(a => a.a_email.Equals(admin.a_email) && a.a_pass.Equals(admin.a_pass)).SingleOrDefault();
            if (verify == null)
            {
                ViewBag.error = "Email or password is incorrect";
                return View();

            }
            else
            {
                FormsAuthentication.SetAuthCookie(admin.a_email, true);
                //Session["adminName"] = admin.a_email.ToString();
                Session["adminId"] = verify.a_Id;
                return RedirectToAction("Dashboard");
            }
            
        }


        //admin dashboard
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


        //admin logout
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }


        //View UserInfo
        public ActionResult UserInfo()
        {
            List<tblUser> allUsers = _unitOfWork.GetRepositoryInstance<tblUser>().GetAllRecordsIQueryable().Where(i => i.IsActive == true).ToList();
            return View(allUsers);

        }
        //View Contact us 
        public ActionResult ContactUsInfo()
        {
            return View(_unitOfWork.GetRepositoryInstance<tblContactU>().GetContactUs());
           

        }

        //admin categories
        public ActionResult Categories()
        {
            List<tblCategory> allcategories = _unitOfWork.GetRepositoryInstance<tblCategory>().GetAllRecordsIQueryable().Where(i => i.isActive == true).ToList();
            return View(allcategories);
        
        }

        //admin add categories
        public ActionResult AddCategories()
        {
            return View();
        }

        //admin add categories post
        [HttpPost]
        public ActionResult AddCategories(tblCategory tbl)
        {
            
            _unitOfWork.GetRepositoryInstance<tblCategory>().Add(tbl);
           
            return RedirectToAction("Categories");
        }


        //admin edit categories
        public ActionResult CategoryEdit(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<tblCategory>().GetFirstorDefault(catId));
        }


        //admin edit categories post
        [HttpPost]
        public ActionResult CategoryEdit(tblCategory tbl)
        {

            _unitOfWork.GetRepositoryInstance<tblCategory>().Update(tbl);
            return RedirectToAction("Categories");
        }

        //admin products
        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<tblProduct>().GetProduct());
        }

        ////admin security products
        public ActionResult securityProduct()
        {
            return View(_unitOfWork.GetRepositoryInstance<securityProduct>().GetProduct());
        }

        //admin edit products
        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<tblProduct>().GetFirstorDefault(productId));
        }

       

        //admin edit products post
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

        //admin security Product Edit
        public ActionResult securityProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<securityProduct>().GetFirstorDefault(productId));
        }

        //admin security edit products post
        [HttpPost]
        public ActionResult securityProductEdit(securityProduct tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/SecurityProductImage/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.s_img = file != null ? pic : tbl.s_img;

            tbl.s_modifiedDate = DateTime.Now;

            _unitOfWork.GetRepositoryInstance<securityProduct>().Update(tbl);
            return RedirectToAction("securityProduct");
        }

        //admin add products
        public ActionResult ProductAdd()
        {

            ViewBag.CategoryList = GetCategory();
            return View();
        }


        //admin add products post
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
            tbl.isDelete = false;
            tbl.p_createdDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<tblProduct>().Add(tbl);
            return RedirectToAction("Product");
        }

      

        //admin add SecurityProducts
        public ActionResult securityProductAdd()
        {
            return View();
        }


        //admin add SecurityProducts post
        [HttpPost]
        public ActionResult securityProductAdd(securityProduct tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/SecurityProductImage/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.s_img = pic;
            tbl.isDelete = false;
            tbl.s_createdDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<securityProduct>().Add(tbl);
            return RedirectToAction("securityProduct");
        }


        //Get product Details
        public ViewResult GetProductDetail(int id, int? page)
        {
            var data = _unitOfWork.GetRepositoryInstance<tblProduct>().GetFirstorDefault(id);
            return View(data);
        }


    }
}