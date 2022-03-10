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
    public class OrderController : Controller
    {
        DbITApexEntities db = new DbITApexEntities();
        // GET: Order
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            var orderList = db.tblShippingDetails.OrderByDescending(x => x.shipping_Id).ToPagedList(pageNumber, pageSize);
            return View(orderList);
        }

        public ActionResult Details(int ?id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.tblShippingDetails.Find(id);
            if(order==null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
    }
}