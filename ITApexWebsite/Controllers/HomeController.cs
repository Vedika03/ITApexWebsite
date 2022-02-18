using ITApexWebsite.Models;
using ITApexWebsite.Models.Home;
using ITApexWebsite.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace ITApexWebsite.Controllers
{


    public class HomeController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        DbITApexEntities ctx = new DbITApexEntities();

        //index
        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 4, page));
        }


        //Add to cart
        public ActionResult AddToCart(int productId)
        {
            Item model = new Item();

            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.tblProducts.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1,
                   


                }) ;
                Session["cart"] = cart;
                
            }
            else
            {

                List<Item> cartCount = (List<Item>)Session["cartCount"];
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.tblProducts.Find(productId);

                foreach (var item in cart.ToList())
                {

                    if (item.Product.p_Id == productId)
                    {
                        int prevQty = item.Quantity;
                        cart.Remove(item);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        }); ;
                        break;
                    }
                    else
                    {
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = 1
                        });
                    }
                }

             
                Session["cart"] = cart;
            }
            
            return Redirect("Index");
        }


        //Remove from cart
        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.p_Id == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }

        public ActionResult ITProducts(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 4, page));
            
        }

        //partial view for category in navbar
        public PartialViewResult CategoryPartial()
        {
            var categoryList = ctx.tblCategories.OrderBy(x => x.c_name).ToList();
            return PartialView(categoryList);
        }




        public ActionResult DisplayProduct(int? page, int? id)
        {
            int pageindex = 9, pagesize = 1;
            if (id!=null)
            {
                var list = ctx.tblProducts.Where(x => x.p_fk_c == id).OrderByDescending(x => x.p_Id).ToPagedList(pageindex, pagesize);
                //return View(model.CreateModel(search, 4, page));
                //IPagedList<tblProduct> data = list.ToPagedList(pageindex, pagesize);
                ViewBag.category = id;
                return View();
            }
            else
            {
                var list = ctx.tblProducts.OrderByDescending(x => x.p_Id).ToPagedList(pageindex, pagesize);
                //return View(model.CreateModel(search, 4, page));
                //IPagedList<tblProduct> data = list.ToPagedList(pageindex, pagesize);
                ViewBag.category = id;
                return View();
            }

          
            
            
        }

        public ActionResult CheckOut()
        {
            string path = Server.MapPath("~/ProductImage/");
            string folderPath = Path.Combine(Server.MapPath("~/ProductImage/"),path);
            string[] imagefiles = Directory.GetFiles(folderPath);
            ViewBag.image = imagefiles;
            return View();
        }

        public ActionResult CheckOutDetails()
        {
            return View();
        }

        //About
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}