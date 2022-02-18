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
        // GET: Payment
        public ActionResult PaymentWithPaypal()
        {
            APIContext aPIContext = PaypalConfiguration.GetAPIContext();
            try
            {
                string PayerId = Request.Params["PayerID"];
                if(string.IsNullOrEmpty(PayerId))
                {
                    string baseURi = Request.Url.Scheme + "://" + Request.Url.Authority + "PaymentWithPaypal/PaymentWithPaypal?";
                    var Guid = Convert.ToString((new Random()).Next(10000000));
                    var createPayment = this.createPayment(aPIContext,baseURi+"guid="+Guid);
                    var links = createPayment.links.GetEnumerator();
                    string paypalRedirectURL = null;

                    while(links.MoveNext())
                    {
                        Links link = links.Current; 

                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectURL = link.href;
                        }

                    }
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executePayment = ExecutePayment(aPIContext, PayerId, Session[guid] as string);

                    if(executePayment.ToString().ToLower()!="approved")
                    {
                        return View("FailureView");
                    }

                }
            }
            catch(Exception ex)
            {
                throw ex;
                return View("FailureView");
                
            }
            return View("SuccessView");
        }

        private object ExecutePayment(APIContext aPIContext, string payerId, string paymentID)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId,

            };
            this.payment = new Payment() { id = paymentID };
            return this.payment.Execute(aPIContext, paymentExecution);
        }

        private PayPal.Api.Payment payment;

        private Payment createPayment(APIContext aPIContext, string redirectUrl)
        {
            var ItemList = new ItemList()
            {
                items = new List<Item>()
            };
            if(Session["cart"]!=null)
            {
                List<Models.Home.Item> cart = (List<Models.Home.Item>)(Session["cart"]);
                foreach (var item in cart)
                {
                    ItemList.items.Add(new Item()
                    {
                        name = item.Product.p_name.ToString(),
                        currency = "USD",
                        price = item.Product.p_price.ToString(),
                        quantity = item.Product.p_quantity.ToString(),
                        sku = "sku"
                    });
                   
                }

                var payer = new Payer() { payment_method = "paypal" };

                var redirUrl = new RedirectUrls()
                {
                    cancel_url = redirectUrl + "&Cancel=true",
                    return_url = redirectUrl

                };

                var details = new Details()
                {
                    tax = "1",
                    shipping = "1",
                    subtotal = "1"

                };

                var amount = new Amount()
                {
                    currency = "USD",
                    total = Session["sesTotal"].ToString(),
                    details = details
                };

                var transactionList = new List<Transaction>();
                transactionList.Add(new Transaction()
                {
                    description = "Transaction Description",
                    invoice_number = "a1000",
                    amount = amount,
                    item_list = ItemList
                }) ;

                this.payment = new Payment()
                {
                    intent="sale",
                    payer=payer,
                    transactions=transactionList,
                    redirect_urls=redirUrl
                };
            }

            return this.payment.Create(aPIContext);

        }
    }
}