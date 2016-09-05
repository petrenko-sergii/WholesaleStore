using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleStore.Models;

namespace WholesaleStore.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult ShowOrders()
        {
            #region ErrorMessages
            InsertError errorOrder = TempData["errorEmptyOrder"] as InsertError;
            if (errorOrder != null)
            {
                ViewData["ErrorMessageEmptyOrder"] = errorOrder.ErrorMessageEmptyOrder;
            }

            InsertError errorEmptyShowOrder = TempData["errorEmptyShowOrder"] as InsertError;
            if (errorEmptyShowOrder != null)
            {
                ViewData["ErrorMessageEmptyShowOrder"] = errorEmptyShowOrder.ErrorMessageEmptyShowOrder;
            }

            #endregion

            using (var context = new WHOLESALE_STOREEntities())
            {
                var model = new OrderModel();
                foreach (var orderDB in context.ORDER.ToList())
                {
                    if (orderDB.TOTALSUM == 0)
                    {
                        DeleteOrder(orderDB.ID);
                    }
                    else
                    {
                        var customer = new Customer() { Name = orderDB.CUSTOMER.NAME };
                        var order = new Order() { Id = orderDB.ID, OrderNumber = orderDB.ORDERNUMBER, Customer = customer, TotalSum = (decimal)orderDB.TOTALSUM, Date = orderDB.DATE };
                        model.Orders.Add(order);
                    }
                }
                var LoggedUserDB = context.LOGGEDUSER.First();

            var view = View("~/Views/Order/ShowOrders.cshtml", model);
            view.ViewBag.userId = LoggedUserDB.VALUE;
            return view;
            }
        }
        public ActionResult CheckShowOrders(int? orderId)
        {
            #region ErrorMessage
            if (orderId.HasValue == false)
            {
                InsertError error = new InsertError();
                
                error.ErrorMessageEmptyShowOrder = "Choose the order!";
                TempData["errorEmptyShowOrder"] = error;

                return RedirectToAction("ShowOrders", "Order");
            }
            #endregion

            return RedirectToAction("ShowOrderItems", "OrderItem", new {orderId}); 
        }

        public ActionResult AddOrder()
        {
            var model = new HomeModel();

            InsertError err = TempData["error"] as InsertError;
            if (err != null)
            {
                ViewData["ErrorMessageEmptyCustomer"] = err.ErrorMessageEmptyCustomer;
            }

            using (var context = new WHOLESALE_STOREEntities())
            {
                foreach (var ct in context.CUSTOMER.ToList())
                {
                    model.Customers.Add(new Customer() { Id = ct.ID, Name = ct.NAME });
                }
            }

            var view = View("~/Views/Order/AddOrder.cshtml", model);
            return view;
        }
        public ActionResult CheckOrderIsEmpty(int orderId)
        {
            using (var context = new WHOLESALE_STOREEntities())
            {
                var orderDB = context.ORDER.Find(orderId);

                if (orderDB.TOTALSUM == 0)
                {
                    DeleteOrder(orderId);
                    context.SaveChanges();
                    
                    return RedirectToAction("ShowOrders", "Order");
                }
            }

            return RedirectToAction("ShowOrderItems", "OrderItem", new { orderId }); 
        }

        public ActionResult NewOrder(int? customerId)
        {
            if (customerId.HasValue == false)
            {
                InsertError error = new InsertError();
                error.ErrorMessageEmptyCustomer = "Choose the customer or create new!";
                TempData["error"] = error;

                return RedirectToAction("AddOrder", "Order");
            }



            using (var context = new WHOLESALE_STOREEntities())
            {
                var LoggedUserDB = context.LOGGEDUSER.First();
                var order = context.ORDER.Add(new ORDER { ORDERNUMBER = DateTime.Now.ToString("ddMMyy_hhmm"), DATE = DateTime.Now, CUSTOMER_ID = Convert.ToInt32(customerId), USER_ID = Convert.ToInt16(LoggedUserDB.VALUE) });

                context.SaveChanges();
                return RedirectToAction("AddOrderItem","OrderItem", new { orderId = order.ID });
            }
        }

        public ActionResult DeleteOrder(int? orderId)
        {
            #region ErrorMessages
            if (orderId.HasValue == false)
            {
                InsertError error = new InsertError();
                error.ErrorMessageEmptyOrder = "Choose the order!";
                TempData["errorEmptyOrder"] = error;

                return RedirectToAction("ShowOrders", "Order");
            }
            #endregion

            using (var context = new WHOLESALE_STOREEntities())
            {

                foreach (var orderItemDB in context.ORDER_ITEM)
                {
                    if (orderItemDB.ORDER_ID == orderId)
                    {
                        var productDB = context.PRODUCT.Find(orderItemDB.PRODUCT_ID);

                        productDB.AMOUNT += orderItemDB.AMOUNT;
                        context.ORDER_ITEM.Remove(orderItemDB);
                    }
                }
                context.SaveChanges();


                foreach (var orderDB in context.ORDER)
                {
                    if (orderDB.ID == orderId)
                    {
                        context.ORDER.Remove(orderDB);
                    }
                }
                context.SaveChanges();
            }
            return RedirectToAction("ShowOrders", "Order");
        }

    }
}
