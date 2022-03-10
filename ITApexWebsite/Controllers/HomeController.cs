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
using System.Net;



namespace ITApexWebsite.Controllers
{


    public class HomeController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        DbITApexEntities ctx = new DbITApexEntities();
        //private List<Cart> listOfCartModels;

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



                });
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

        //Display IT Products 
        public ActionResult ITProducts(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();

            return View(model.CreateModel(search, 8, page));

        }


        //Display Security Products
        public ActionResult SecurityProducts(string search, int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = ctx.securityProducts.ToList();
            //var list = db.tblProduct_u.Where(Convert.ToInt32(isActive).ToString()=x => x.isActive == 1).OrderByDescending(x => x.p_id).ToList();
            IPagedList<securityProduct> stu = list.ToPagedList(pageindex, pagesize);
            return View(stu);

        }

        //Get Security Products details
        public ViewResult GetSecurityProductDetail(int? id)
        {

            securityProduct pd = new securityProduct();
            securityProduct p = ctx.securityProducts.Where(x => x.s_Id == id).SingleOrDefault();
            pd.s_Id = p.s_Id;
            pd.s_name = p.s_name;
            pd.s_img = p.s_img;
            pd.s_price = p.s_price;
            pd.s_desc = p.s_desc;
            pd.s_quan = p.s_quan;

            return View(pd);

        }

        //partial view for category in navbar
        public PartialViewResult CategoryPartial()
        {
            var categoryList = ctx.tblCategories.OrderBy(x => x.c_name).ToList();
            return PartialView(categoryList);
        }


        
        //Display IT-Products
        public ActionResult DisplayProduct(int? page, int ?id)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;

            if (id != null)
            {
                ViewBag.id =id ;
                var productList = ctx.tblProducts.OrderByDescending(x => x.p_Id).Where(x => x.p_fk_c == id).ToPagedList(pageNumber, pageSize);
                //List<tblProduct> productList = _unitOfWork.GetRepositoryInstance<tblProduct>().GetFirstorDefault((int)id).Where(i => i.p_fk_c == id).ToList();

                return View(productList);
            }
            else
            {
                var productList = ctx.tblProducts.OrderByDescending(x => x.p_Id).ToPagedList(pageNumber, pageSize);
                return View(productList);
            }

        }

      
        //Checkout details
        public ActionResult CheckOut()
        {
            string path = Server.MapPath("~/ProductImage/");
            string folderPath = Path.Combine(Server.MapPath("~/ProductImage/"), path);
            string[] imagefiles = Directory.GetFiles(folderPath);
            ViewBag.image = imagefiles;
            return View();
        }

        //Checkout throgh UPI
        public ActionResult CheckoutUPI()
        {
            return View();
        }

        //Process order through UPI
        public ActionResult UPIProcessOrder(FormCollection frc, int? id)
        {


            List<Item> Cart = (List<Item>)Session["cart"];

            tblShippingDetail dt = new tblShippingDetail()
            {
                CustomerName = frc["cusName"],
                CustomerNo = frc["cusNo"],
                CustomerEmail = frc["cusEMail"],
                CustomerAddress = frc["cusAdd"],
                OrderDate = DateTime.Now,
                payment_type = "UPI",
                Status = "Processing",

                pincode = frc["pin"]

            };
            ctx.tblShippingDetails.Add(dt);
            ctx.SaveChanges();

            //Saving the order detail into orderDetails table

            foreach (Item item in Cart)
            {

                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = dt.shipping_Id,
                    ProductId = item.Product.p_Id,
                    Quantity = item.Quantity,
                    Price = item.Product.p_price

                };
                ctx.OrderDetails.Add(orderDetail);
                ctx.SaveChanges();

            }

            Session.Remove("cart");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = ctx.tblShippingDetails.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);


        }

        //Process order
   public ActionResult ProcessOrder(FormCollection frc, int? id)
        {


            List<Item> Cart = (List<Item>)Session["cart"];

            tblShippingDetail dt = new tblShippingDetail()
            {
                CustomerName = frc["cusName"],
                CustomerNo = frc["cusNo"],
                CustomerEmail = frc["cusEMail"],
                CustomerAddress = frc["cusAdd"],
                OrderDate = DateTime.Now,
                payment_type = "Cash",
                Status = "Processing",

                pincode = frc["pin"]

            };
            ctx.tblShippingDetails.Add(dt);
            ctx.SaveChanges();

            //Saving the order detail into orderDetails table

            foreach (Item item in Cart)
            {

                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = dt.shipping_Id,
                    ProductId = item.Product.p_Id,
                    Quantity = item.Quantity,
                    Price = item.Product.p_price

                };
                ctx.OrderDetails.Add(orderDetail);
                ctx.SaveChanges();

            }



            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var order = ctx.tblShippingDetails.Find(id);
            //if (order == null)
            //{
            //    return HttpNotFound();
            //}
            //Session.Remove("cart");
            return View(order);


        }

        //get shipping details
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

            return View();
        }
        
        //contact post
        [HttpPost]
        public ActionResult Contact(tblContactU tbl)
        {
            _unitOfWork.GetRepositoryInstance<tblContactU>().Add(tbl);
            return RedirectToAction("ContactSuccess");
        }

        //Contact us succes page
         public ActionResult ContactSuccess()
        {
            return View();
        }


  
        public ActionResult SAddToCart(int productId)
        {
            Item model = new Item();

            if (Session["Scart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.securityProducts.Find(productId);
                cart.Add(new Item()
                {
                    SProduct = product,
                    Quantity = 1,



                });
                Session["Scart"] = cart;

            }
            else
            {

                List<Item> cartCount = (List<Item>)Session["cartCount"];
                List<Item> cart = (List<Item>)Session["Scart"];
                var product = ctx.securityProducts.Find(productId);

                foreach (var item in cart.ToList())
                {

                    if (item.SProduct.s_Id == productId)
                    {
                        int prevQty = item.Quantity;
                        cart.Remove(item);
                        cart.Add(new Item()
                        {
                            SProduct = product,
                            Quantity = prevQty + 1
                        }); ;
                        break;
                    }
                    else
                    {
                        cart.Add(new Item()
                        {
                            SProduct = product,
                            Quantity = 1
                        });
                    }
                }


                Session["Scart"] = cart;
            }

            return Redirect("SecurityProducts");
        }


        //Remove from cart
        public ActionResult SRemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["Scart"];
            foreach (var item in cart)
            {
                if (item.SProduct.s_Id == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["Scart"] = cart;
            return Redirect("SecurityProducts");
        }

    }



}

