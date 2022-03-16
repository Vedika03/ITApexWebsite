using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITApexWebsite.Models;
using ITApexWebsite.Repository;
using Newtonsoft.Json;
using PagedList;
using System.Web.Security;
using ITApexWebsite.Models.Home;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web.Hosting;

namespace ITApexWebsite.Controllers
{
    public class UserController : Controller
    {


        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        DbITApexEntities db = new DbITApexEntities();
       

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<tblCategory>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.c_Id.ToString(), Text = item.c_name });
            }
            return list;
        }

        //user login
        public ActionResult Login()
        {
            return View();
        }

        //user login post
        [HttpPost]
        public ActionResult Login(tblUser user)
        {
            var verify = db.tblUsers.Where(u => u.u_email.Equals(user.u_email) && u.u_pass.Equals(user.u_pass)).SingleOrDefault();
            if (verify == null)
            {
                ViewBag.error = "Email or password is incorrect";
                return View();

            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.u_email, true);
                Session["userName"] = user.u_email.ToString();
                Session["userId"] = verify.u_Id;
                return RedirectToAction("UserDashboard");
            }
        }



        //user register
        public ActionResult Registration()
        {
            UserReg r = new UserReg();
            return View();
        }


        //user register post
        [HttpPost]
        public ActionResult Registration(UserReg r)
        {
            tblUser user = new tblUser();
            
            if(db.tblUsers.Any(u => u.u_email.Equals(r.u_email)))
                {
                ViewBag.DuplicateMessage = "Email-Id already exist.";
                return View("Registration", r);
            }
            r.IsActive = false;
            
            user.u_CreatedOn = DateTime.Now;
            user.u_name = r.u_name;
            user.u_email = r.u_email;
            user.u_contact = r.u_contact;
            user.u_pass = r.u_pass;
            user.IsActive = true;
            user.IsDelete = false;
            db.tblUsers.Add(user);
            db.SaveChanges();

            BuildEmailTemplate(user.u_Id);

            return RedirectToAction("Login");
        }

        //public JsonResult SaveData(tblUser r)
        //{
        //    r.IsActive = false;
        //    BuildEmailTemplate(r.u_Id);
        //    return Json("Registration succesful", JsonRequestBehavior.AllowGet);
        //}

        public JsonResult RegisterConfirm(int regID)
        {
            tblUser Data = db.tblUsers.Where(x => x.u_Id == regID).FirstOrDefault();
            Data.IsActive = true;
            db.SaveChanges();
            return Json("Your Account is successfully created", JsonRequestBehavior.AllowGet);
            
        }

        public void BuildEmailTemplate(int regID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var regInfo= db.tblUsers.Where(x => x.u_Id == regID).FirstOrDefault();
            BuildEmailTemplate("Your Account is successfully created", body, regInfo.u_email);
        }

        public void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "vedasc03@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if(!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("vedasc03@gmail.com", "Vedas@0301");
            try
            {
                client.Send(mail);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //forgot password 
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //forgot password post
        //[HttpPost]
        //public ActionResult ForgotPassword(string EmailId)
        //{
        //    //verify email id
        //    //Generate reset password link
        //    //send email

        //    string message = "";
        //    bool status = false;

        //    using (DbITApexEntities dbe = new DbITApexEntities())
        //    {
        //        var account = dbe.tblUsers.Where(x => x.u_email == EmailId).FirstOrDefault();
        //        if (account != null)
        //        {
        //            //send email for reset password
        //        }
        //        else
        //        {
        //            message = "Account not found";
        //        }
        //    }

        //    return View();
        //}

        //public JsonResult SendEmailToConfirm(tblUser r)
        //{
        //    r.IsActive = false;
        //    bool result = false;
        //    result = SendEmail("vedasc03@gmail.com", "IT-Apex email sending test", "<p>Hi !<br/>Thankyou for Registering in IT-Apex.</p>");
        //    return Json(result, JsonRequestBehavior.AllowGet);

        //}

        //public bool SendEmail(string toEmail,string subject,string emailBody)
        //{
        //    try
        //    {
        //        string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
        //        string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

        //        SmtpClient client = new SmtpClient("smtp.gmail.com",587);
        //        client.EnableSsl = true;
        //        client.Timeout = 1000000;
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = new NetworkCredential(senderEmail, senderPassword);

        //        MailMessage mailMessage = new MailMessage(senderEmail,toEmail,subject,emailBody);
        //        mailMessage.IsBodyHtml = true;
        //        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
        //        client.Send(mailMessage);

        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        return false;
        //    }

        //}

        //user logout
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }


        //user dashboard
        public ActionResult UserDashboard()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                return View();
            }
        }

        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<tblProduct_u>().GetProduct());
        }


        //User Product Edit
        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<tblProduct_u>().GetFirstorDefault(productId));
        }


        //User Product Edit post
        [HttpPost]
        public ActionResult ProductEdit(tblProduct_u tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/UserProductImage/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.p_img = file != null ? pic : tbl.p_img;
            tbl.p_fk_user = Convert.ToInt32(Session["userId"].ToString());
            tbl.p_modifiedDate = DateTime.Now;

            _unitOfWork.GetRepositoryInstance<tblProduct_u>().Update(tbl);
            return RedirectToAction("Product");
        }


        //User Product Add
        public ActionResult ProductAdd()
        {

            ViewBag.CategoryList = GetCategory();
            return View();
        }


        //User Product Add post
        [HttpPost]
        public ActionResult ProductAdd(tblProduct_u tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/UserProductImage/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.p_img = pic;

            tbl.p_createdDate = DateTime.Now;
            tbl.p_fk_user = Convert.ToInt32(Session["userId"].ToString());
            _unitOfWork.GetRepositoryInstance<tblProduct_u>().Add(tbl);
            return RedirectToAction("Product");
        }

        public ActionResult ViewProduct(int ? id)
        {
            UserProductDisplay pd = new UserProductDisplay();
                tblProduct_u p = db.tblProduct_u.Where(x => x.p_Id == id).SingleOrDefault();
                pd.p_Id = p.p_Id;
                pd.p_name = p.p_name;
                pd.p_img = p.p_img;
                pd.p_price = p.p_price;
                pd.p_desc = p.p_desc;
                tblCategory ct = db.tblCategories.Where(x => x.c_Id == p.p_fk_user).SingleOrDefault();
                pd.c_name = ct.c_name;

                tblUser u = db.tblUsers.Where(x => x.u_Id == p.p_fk_user).SingleOrDefault();
                pd.u_name = u.u_name;
            pd.u_contact = u.u_contact;

            return View(pd);

            
            //int pagesize = 9, pageindex = 1;
            //pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            //var list = db.tblProduct_u.ToList();
            ////var list = db.tblProduct_u.Where(Convert.ToInt32(isActive).ToString()=x => x.isActive == 1).OrderByDescending(x => x.p_id).ToList();
            //IPagedList<tblProduct_u> stu = list.ToPagedList(pageindex, pagesize);
            //return View(stu);
        }

        //Display Security Products
        public ActionResult SecondHandProducts(string search, int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.tblProduct_u.ToList();
            //var list = db.tblProduct_u.Where(Convert.ToInt32(isActive).ToString()=x => x.isActive == 1).OrderByDescending(x => x.p_id).ToList();
            IPagedList<tblProduct_u> stu = list.ToPagedList(pageindex, pagesize);
            return View(stu);

        }

       
        //getting second hand product detail to display
        public ViewResult GetProductDetail(int? id)
        {
            
            UserProductDisplay pd = new UserProductDisplay();
            tblProduct_u p = db.tblProduct_u.Where(x => x.p_Id == id).SingleOrDefault();
            pd.p_Id = p.p_Id;
            pd.p_name = p.p_name;
            pd.p_img = p.p_img;
            pd.p_price = p.p_price;
            pd.p_desc = p.p_desc;
            tblCategory ct = db.tblCategories.Where(x => x.c_Id == p.p_fk_Category).SingleOrDefault();

            pd.c_name = ct.c_name;

            tblUser u = db.tblUsers.Where(x => x.u_Id == p.p_fk_user).SingleOrDefault();
            pd.u_name = u.u_name;
            pd.u_contact = u.u_contact;
            Session["uID"] = u.u_Id.ToString();
            return View(pd);

           
        }


        //// GET: User/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: User/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}
    }
}