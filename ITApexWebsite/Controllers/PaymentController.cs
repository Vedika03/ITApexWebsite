using ITApexWebsite.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITApexWebsite.Controllers
{
    public class PaymentController : Controller
    {
        DbITApexEntities db = new DbITApexEntities();
        // GET: Payment
        //public ActionResult PaymentWithPaypal(FormCollection frc)
        //{
        //    List<Models.tblShippingDetail> Cart = (List<Models.tblShippingDetail>)(Session["cart"]);
        //    tblShippingDetail dt = new tblShippingDetail()
        //    {
                
        //    CustomerName = frc["cusName"],
        //        CustomerNo=frc["cusNo"],
        //        CustomerEmail=frc["cusEMail"],
        //        CustomerAddress=frc["cusAdd"],
        //        OrderDate=DateTime.Now,
        //        payment_type="Cash",
        //        Status="Processing",
        //        pincode=frc["pin"]

        //};
        //    db.tblShippingDetails.Add(dt);
        //    db.SaveChanges();

        //    //Saving the order detail into orderDetails table

        //    foreach (tblShippingDetail item in Cart)
        //    {
        //        tblProduct Product = new tblProduct();
        //        OrderDetail orderDetail = new OrderDetail()
        //        {
        //            OrderId = dt.shipping_Id,
        //            ProductId=Product.p_Id,
        //            Quantity=Product.p_quantity,
        //            Price=Product.p_price

        //        };
        //        db.OrderDetails.Add(orderDetail);
        //        db.SaveChanges();
                
        //    }

        //    Session.Remove("cart");

        //    APIContext aPIContext = PaypalConfiguration.GetAPIContext();
        //    try
        //    {
        //        string PayerId = Request.Params["PayerID"];
        //        if(string.IsNullOrEmpty(PayerId))
        //        {
        //            string baseURi = Request.Url.Scheme + "://" + Request.Url.Authority + "PaymentWithPaypal/PaymentWithPaypal?";
        //            var Guid = Convert.ToString((new Random()).Next(10000000));
        //            var createPayment = this.createPayment(aPIContext,baseURi+"guid="+Guid);
        //            var links = createPayment.links.GetEnumerator();
        //            string paypalRedirectURL = null;

        //            while(links.MoveNext())
        //            {
        //                Links link = links.Current; 

        //                if (link.rel.ToLower().Trim().Equals("approval_url"))
        //                {
        //                    paypalRedirectURL = link.href;
        //                }

        //            }
        //        }
        //        else
        //        {
        //            var guid = Request.Params["guid"];
        //            var executePayment = ExecutePayment(aPIContext, PayerId, Session[guid] as string);

        //            if(executePayment.ToString().ToLower()!="approved")
        //            {
        //                return View("FailureView");
        //            }

        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //        return View("FailureView");
                
        //    }

            

        //    return View("SuccessView");
        //}

        //private object ExecutePayment(APIContext aPIContext, string payerId, string paymentID)
        //{
        //    var paymentExecution = new PaymentExecution()
        //    {
        //        payer_id = payerId,

        //    };
        //    this.payment = new Payment() { id = paymentID };
        //    return this.payment.Execute(aPIContext, paymentExecution);
        //}

        //private PayPal.Api.Payment payment;

        //private Payment createPayment(APIContext aPIContext, string redirectUrl)
        //{
        //    var ItemList = new ItemList()
        //    {
        //        items = new List<Item>()
        //    };
        //    if(Session["cart"]!=null)
        //    {
        //        List<Models.Home.Item> cart = (List<Models.Home.Item>)(Session["cart"]);
        //        foreach (var item in cart)
        //        {
        //            ItemList.items.Add(new Item()
        //            {
        //                name = item.Product.p_name.ToString(),
        //                currency = "USD",
        //                price = item.Product.p_price.ToString(),
        //                quantity = item.Product.p_quantity.ToString(),
        //                sku = "sku"
        //            });
                   
        //        }

        //        var payer = new Payer() { payment_method = "paypal" };

        //        var redirUrl = new RedirectUrls()
        //        {
        //            cancel_url = redirectUrl + "&Cancel=true",
        //            return_url = redirectUrl

        //        };

        //        var details = new Details()
        //        {
        //            tax = "1",
        //            shipping = "1",
        //            subtotal = "1"

        //        };

        //        var amount = new Amount()
        //        {
        //            currency = "USD",
        //            total = Session["sesTotal"].ToString(),
        //            details = details
        //        };

        //        var transactionList = new List<Transaction>();
        //        transactionList.Add(new Transaction()
        //        {
        //            description = "Transaction Description",
        //            invoice_number = "a1000",
        //            amount = amount,
        //            item_list = ItemList
        //        }) ;

        //        this.payment = new Payment()
        //        {
        //            intent="sale",
        //            payer=payer,
        //            transactions=transactionList,
        //            redirect_urls=redirUrl
        //        };
        //    }

        //    return this.payment.Create(aPIContext);

        //}


        // Work with Paypal Payment
        private Payment payment;
        

        //private Payment createPayment(APIContext apiContext, string redirectUrl)
        //{
        //    Item i = new Item();
        //    var listItem = new ItemList() { items = new List<i>() };
        //    List<i> Cart = (List<i>)Session["cart"];
        //    foreach (Item item in Cart)
        //    {
        //        listItem.items.Add(new Item()
        //        {
        //            name= item.name,
        //            currency="USD",
        //            price=item.price,
        //            quantity=item.,
        //            sku="sku"
        //        });
        //    }

        //    var payer = new Payer() { payment_method = "paypal" };
        //    // Do the configuration RedirectURLs here with redirectURLs object
        //    var redirUrls = new RedirectUrls()
        //    {
        //        cancel_url = redirectUrl,
        //        return_url = redirectUrl
        //    };

        //    // Create details object
        //    var details = new Details()
        //    {
        //        tax = "1",
        //        shipping = "2",
        //        subtotal = Cart.Sum(x => x.quantity * x.price).ToString()
        //    };

        //    // Create amount object
        //    var amount = new Amount()
        //    {
        //        currency = "USD",
        //        total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),// tax + shipping + subtotal
        //        details = details
        //    };
        //}

    }
}