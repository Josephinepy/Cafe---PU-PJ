using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Cafe.DAL;

namespace Cafe.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.OrderModel.Ship postback)
        {
            if (this.ModelState.IsValid)
            {   
                var currentcart = Models.Cart.Operation.GetCurrentCart();

                
                var userId = HttpContext.User.Identity.GetUserId();

                using (dbCafeEntities db = new dbCafeEntities())
                {
                    
                    var order = new Order()
                    {
                        
                        Table = postback.Table
                    };
                    
                    db.Orders.Add(order);
                    db.SaveChanges();
                        
                    
                    var orderDetails = currentcart.ToOrderDetailList(order.OrderId);

                    
                    db.OrderDetails.AddRange(orderDetails);
                    db.SaveChanges();
                }
                return Content("success");
            }
            return View();
        }

    }
}

