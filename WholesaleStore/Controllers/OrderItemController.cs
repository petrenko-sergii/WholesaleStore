using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleStore.Models;
using WholesaleStore.Controllers;

namespace WholesaleStore.Controllers
{
    public class OrderItemController : Controller
    {
        public ActionResult AddOrderItem(int orderId)
        {
            #region ErrorMessages
            InsertError errEmptyAmount = TempData["errorEmptyAmount"] as InsertError;
            if (errEmptyAmount != null)
            {
                ViewData["ErrorMessageEmptyAmount"] = errEmptyAmount.ErrorMessageEmptyAmount;
            }

            InsertError errWrongAmount = TempData["errorWrongAmount"] as InsertError;
            if (errWrongAmount != null)
            {
                ViewData["ErrorMessageWrongAmount"] = errWrongAmount.ErrorMessageWrongAmount;
            }

            InsertError errTooBigAmount = TempData["error"] as InsertError;
            if (errTooBigAmount != null)
            {
                ViewData["ErrorMessageTooBigAmount"] = errTooBigAmount.ErrorMessageTooBigAmount;
            }
            #endregion

            var model = new OrderItemModel();
            model.Products = new List<Product>();
            model.Order = new Order();
            model.Order.Items = new List<OrderItem>();

            using (var context = new WHOLESALE_STOREEntities())
            {
                var orderDB = context.ORDER.Find(orderId);
                var customerDB = orderDB.CUSTOMER;
                var userDB = orderDB.USER;
                var userTypeDB = orderDB.USER.USERTYPE;
                var customer = new Customer() { Name = customerDB.NAME, Email = customerDB.EMAIL };
                var user = new User() { Name = userDB.NAME };
                var userType = new UserType() { Name = userTypeDB.NAME };

                model.Order.Id = orderDB.ID;
                model.Order.Date = orderDB.DATE;
                model.Order.Customer = customer;
                model.Order.OrderNumber = orderDB.ORDERNUMBER;
                model.Order.User = user;
                model.Order.User.Usertype = userType;
                model.Order.TotalSum = 0;

                foreach (var orderItemDB in orderDB.ORDER_ITEM)
                {
                    var productDB = orderItemDB.PRODUCT;
                    var unitDB = new Unit() { Name = orderItemDB.PRODUCT.UNIT.NAME, Id = orderItemDB.PRODUCT.UNIT.ID };
                    var product = new Product() { Id = productDB.ID, Name = productDB.NAME, Price = productDB.PRICE, BarCode = productDB.BARCODE, Unit = unitDB };
                    model.Order.Items.Add(new OrderItem() { Id = orderItemDB.ID, Price = orderItemDB.PRICE, Amount = orderItemDB.AMOUNT, Product = product });
                    model.Order.TotalSum += (decimal)orderItemDB.AMOUNT * orderItemDB.PRICE;
                }

                var productIds = new List<int>();

                foreach (var item in model.Order.Items)
                {
                    productIds.Add(item.Product.Id);
                }

                foreach (var pr in context.PRODUCT.OrderBy(item => item.NAME).ToList())
                {
                    if (pr.AMOUNT != 0 && productIds.Contains(pr.ID) == false)
                    {
                        var category = new Category() { Name = pr.CATEGORY.NAME };
                        var unit = new Unit() { Name = pr.UNIT.NAME };
                        model.Products.Add(new Product() { Id = pr.ID, Name = pr.NAME, BarCode = pr.BARCODE, Price = pr.PRICE, Amount = pr.AMOUNT, Category = category, Unit = unit });
                    }
                }
                orderDB.TOTALSUM = model.Order.TotalSum;
                context.SaveChanges();
            }

            var view = View("~/Views/OrderItem/AddOrderItem.cshtml", model);
            view.ViewBag.orderId = orderId;
            return view;
        }
        public ActionResult SaveOrderItem(int orderId, int productId, float? amount)
        {
            #region ErrorMessages
            if (amount == null)
            {
                InsertError errorEmptyAmount = new InsertError();
                errorEmptyAmount.ErrorMessageEmptyAmount = "Enter the Amount!";
                TempData["errorEmptyAmount"] = errorEmptyAmount;
            }

            else if (amount <= 0)
            {
                InsertError errorWrongAmount = new InsertError();
                errorWrongAmount.ErrorMessageWrongAmount = "Wrong Amount! Can not be negative!";
                TempData["errorWrongAmount"] = errorWrongAmount;
            }

            if (TempData["errorEmptyAmount"] != null || TempData["errorWrongAmount"] != null)
            {
                return RedirectToAction("AddOrderItem", "OrderItem", new { orderId });
            }

            #endregion

            using (var context = new WHOLESALE_STOREEntities())
            {
                var productDB = context.PRODUCT.Find(productId);

                #region Errormessage
                if (productDB.AMOUNT < amount)
                {
                    InsertError error = new InsertError();
                    error.ErrorMessageTooBigAmount = "Too big amount! Not enough at the store!";
                    TempData["error"] = error;

                    return RedirectToAction("AddOrderItem", "OrderItem", new { orderId });
                }
                #endregion

                var order = context.ORDER.Find(orderId);
                var product = new Product() { Price = productDB.PRICE };
                var orderItem = context.ORDER_ITEM.Add(new ORDER_ITEM { PRODUCT_ID = productId, AMOUNT = Convert.ToSingle(amount), ORDER_ID = orderId, PRICE = product.Price });

                productDB.AMOUNT -= Convert.ToSingle(amount);
                context.SaveChanges();

                return RedirectToAction("AddOrderItem", "OrderItem", new { orderId });
            }
        }
        public ActionResult ShowOrderItems(int orderId)
        {
            #region ErrorMessage
            InsertError errorItem = TempData["errorEmptyItem"] as InsertError;
            if (errorItem != null)
            {
                ViewData["ErrorMessageEmptyItem"] = errorItem.ErrorMessageEmptyItem;
            }

            #region ErrorsChangeItemAmount

            InsertError errorEmptyAmountChangeItem = TempData["errorEmptyChangeAmountItem"] as InsertError;
            if (errorEmptyAmountChangeItem != null)
            {
                ViewData["ErrorMessageEmptyChangeAmountItem"] = errorEmptyAmountChangeItem.ErrorMessageEmptyChangeAmountItem;
            }

            InsertError errEmptyAmount = TempData["errorEmptyAmount"] as InsertError;
            if (errEmptyAmount != null)
            {
                ViewData["ErrorMessageEmptyAmount"] = errEmptyAmount.ErrorMessageEmptyAmount;
            }

            InsertError errWrongAmount = TempData["errorWrongAmount"] as InsertError;
            if (errWrongAmount != null)
            {
                ViewData["ErrorMessageWrongAmount"] = errWrongAmount.ErrorMessageWrongAmount;
            }
            #endregion

            #endregion

           

            
                var model = new OrderItemModel();
                model.Order = new Order();
                model.Items = new List<OrderItem>();

                InsertError err = TempData["error"] as InsertError;
                if (err != null)
                {
                    ViewData["ErrorMessageTooBigAmount"] = err.ErrorMessageTooBigAmount;
                }

                using (var context = new WHOLESALE_STOREEntities())
                {
                    var orderDB = context.ORDER.Find(orderId);
                    var customerDB = orderDB.CUSTOMER;
                    var userDB = orderDB.USER;
                    var userTypeDB = orderDB.USER.USERTYPE;
                    var customer = new Customer() { Name = customerDB.NAME, Email = customerDB.EMAIL };
                    var user = new User() { Name = userDB.NAME };
                    var userType = new UserType() { Name = userTypeDB.NAME };

                    model.Order.Id = orderDB.ID;
                    model.Order.Date = orderDB.DATE;
                    model.Order.Customer = customer;
                    model.Order.OrderNumber = orderDB.ORDERNUMBER;
                    model.Order.User = user;
                    model.Order.User.Usertype = userType;
                    model.Order.TotalSum = orderDB.TOTALSUM.Value;

                    foreach (var orderItemDB in context.ORDER_ITEM)
                    {
                        if (orderItemDB.ORDER_ID == orderId)
                        {
                            var unitDB = new Unit() { Name = orderItemDB.PRODUCT.UNIT.NAME, Id = orderItemDB.PRODUCT.UNIT.ID };
                            var productDB = new Product() { Id = orderItemDB.PRODUCT_ID, Name = orderItemDB.PRODUCT.NAME, BarCode = orderItemDB.PRODUCT.BARCODE, Unit = unitDB };
                            model.Items.Add(new OrderItem() { Id = orderItemDB.ID, Amount = orderItemDB.AMOUNT, Price = orderItemDB.PRICE, Product = productDB });
                        }
                    }
                }
                return View("~/Views/OrderItem/ShowOrderItems.cshtml", model);
            
           
        }
        public ActionResult ChangeOrderItemAmount(int? itemId, float? newAmount, int selectedOrderId)
        {
            #region ErrorMessages
            if (itemId.HasValue == false)
            {
                InsertError errorItemId = new InsertError();
                errorItemId.ErrorMessageEmptyChangeAmountItem = "Choose the item!";
                TempData["errorEmptyChangeAmountItem"] = errorItemId;
            }

            if (newAmount == null)
            {
                InsertError errorEmptyAmount = new InsertError();
                errorEmptyAmount.ErrorMessageEmptyAmount = "-----Enter the Amount!";
                TempData["errorEmptyAmount"] = errorEmptyAmount;
            }

            else if (newAmount <= 0)
            {
                InsertError errorWrongAmount = new InsertError();
                errorWrongAmount.ErrorMessageWrongAmount = "-----Wrong Amount! Can not be negative!";
                TempData["errorWrongAmount"] = errorWrongAmount;
            }
          
            if (TempData["errorEmptyChangeAmountItem"] != null ||
               TempData["errorEmptyAmount"] != null ||
               TempData["errorWrongAmount"] != null
               )
            {
                return RedirectToAction("ShowOrderItems", "OrderItem", new { orderId = selectedOrderId });
            }
            #endregion

            var currentOrderId = 0;

            using (var context = new WHOLESALE_STOREEntities())
            {
                var orderItemDB = context.ORDER_ITEM.Find(itemId);
                var orderId = context.ORDER_ITEM.Find(itemId).ORDER_ID;
                var orderDB = context.ORDER.Find(orderId);
                var productDB = context.PRODUCT.Find(orderItemDB.PRODUCT_ID);

                currentOrderId = orderId;
                orderDB.TOTALSUM = 0;

                #region ErrorMessage
                if (productDB.AMOUNT < (newAmount - orderItemDB.AMOUNT))
                {
                    InsertError error = new InsertError();
                    error.ErrorMessageTooBigAmount = "Too big amount! Not enough at the store!";
                    TempData["error"] = error;

                    return RedirectToAction("ShowOrderItems", new { orderId = currentOrderId });
                }
                #endregion

                productDB.AMOUNT -= (Convert.ToSingle(newAmount) - orderItemDB.AMOUNT);
                orderItemDB.AMOUNT = Convert.ToSingle(newAmount);
                context.SaveChanges();

                foreach (var tempOrder in orderDB.ORDER_ITEM)
                {
                    orderDB.TOTALSUM += (decimal)tempOrder.AMOUNT * tempOrder.PRICE;
                }
                context.SaveChanges();
            }
            return RedirectToAction("ShowOrderItems", "OrderItem", new { orderId = currentOrderId });
        }
        public ActionResult DeleteOrderItem(int? itemId, int orderId)
        {
            #region ErrorMessage
            if (itemId.HasValue == false)
            {
                InsertError error = new InsertError();
                error.ErrorMessageEmptyItem = "Choose the item!";
                TempData["errorEmptyItem"] = error;

                return RedirectToAction("ShowOrderItems", "OrderItem", new { orderId });
            }
            #endregion

            using (var context = new WHOLESALE_STOREEntities())
            {
                var orderItemDB = context.ORDER_ITEM.Find(itemId);
                var orderDB = context.ORDER.Find(orderItemDB.ORDER_ID);
                var productDB = context.PRODUCT.Find(orderItemDB.PRODUCT_ID);

                orderDB.TOTALSUM -= (decimal)orderItemDB.AMOUNT * orderItemDB.PRICE;
                productDB.AMOUNT += orderItemDB.AMOUNT;
                context.ORDER_ITEM.Remove(orderItemDB);
                context.SaveChanges();

                return ShowOrderItems(orderId);
            }
        }
    }
}
