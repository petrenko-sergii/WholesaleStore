using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleStore.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WholesaleStore.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult ShowCustomers()
        {
            #region ErrorMessages
            InsertError errorCustomer = TempData["errorEmptyCustomer"] as InsertError;
            if (errorCustomer != null)
            {
                ViewData["ErrorMessageEmptyCustomer"] = errorCustomer.ErrorMessageEmptyCustomer;
            }

            InsertError errorEditCustomer = TempData["errorEmptyEditCustomer"] as InsertError;
            if (errorEditCustomer != null)
            {
                ViewData["ErrorMessageEmptyEditCustomer"] = errorEditCustomer.ErrorMessageEmptyEditCustomer;
            }

            InsertError errorCustomerOrders = TempData["errorEmptyOrdersCategory"] as InsertError;
            if (errorCustomerOrders != null)
            {
                ViewData["ErrorMessageEmptyOrdersCustomer"] = errorCustomerOrders.ErrorMessageEmptyOrdersCustomer;
            }
            #endregion

            var model = new CustomerModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                foreach (var cs in context.CUSTOMER.OrderBy(c => c.NAME).ToList())
                {
                    var customer = new Customer() { Id = cs.ID, Name = cs.NAME, Address = cs.C_ADDRESS, PhoneNumber = cs.PHONE_NUMBER, Email = cs.EMAIL };
                    model.Customers.Add(customer);
                }
            }
            return View("~/Views/Customer/ShowCustomers.cshtml", model);
        }
        public ActionResult AddCustomer()
        {            
            #region ErrorMessages   
                InsertError errName = TempData["errorName"] as InsertError;
                if (errName != null)
                {
                    ViewData["ErrorMessageEmptyName"] = errName.ErrorMessageEmptyName;
                }

                InsertError errEmptyPhone = TempData["errorEmptyPhone"] as InsertError;
                if (errEmptyPhone != null)
                {
                    ViewData["ErrorMessageEmptyPhoneNumber"] = errEmptyPhone.ErrorMessageEmptyPhoneNumber;
                }

                InsertError errWrongPhone = TempData["errorWrongPhone"] as InsertError;
                if (errWrongPhone != null)
                {
                    ViewData["ErrorMessageWrongPhone"] = errWrongPhone.ErrorMessageWrongPhone;
                }

                InsertError errEmail = TempData["errorEmail"] as InsertError;
                if (errEmail != null)
                {
                    ViewData["ErrorMessageEmptyEmail"] = errEmail.ErrorMessageEmptyEmail;
                }

                InsertError errWrongEmail = TempData["errorWrongEmail"] as InsertError;
                if (errWrongEmail != null)
                {
                    ViewData["ErrorMessageWrongEmail"] = errWrongEmail.ErrorMessageWrongEmail;
                }

                InsertError errAddress = TempData["errorAddress"] as InsertError;
                if (errAddress != null)
                {
                    ViewData["ErrorMessageEmptyAddress"] = errAddress.ErrorMessageEmptyAddress;
                }
            #endregion

            var model = new CustomerModel();

            return View("~/Views/Customer/AddCustomer.cshtml",model);
        }
        public ActionResult AddCustomerWithParams(CustomerModel model)
        {
            #region ErrorMessages
            InsertError errName = TempData["errorName"] as InsertError;
            if (errName != null)
            {
                ViewData["ErrorMessageEmptyName"] = errName.ErrorMessageEmptyName;
            }

            InsertError errEmptyPhone = TempData["errorEmptyPhone"] as InsertError;
            if (errEmptyPhone != null)
            {
                ViewData["ErrorMessageEmptyPhoneNumber"] = errEmptyPhone.ErrorMessageEmptyPhoneNumber;
            }

            InsertError errWrongPhone = TempData["errorWrongPhone"] as InsertError;
            if (errWrongPhone != null)
            {
                ViewData["ErrorMessageWrongPhone"] = errWrongPhone.ErrorMessageWrongPhone;
            }

            InsertError errEmail = TempData["errorEmail"] as InsertError;
            if (errEmail != null)
            {
                ViewData["ErrorMessageEmptyEmail"] = errEmail.ErrorMessageEmptyEmail;
            }

            InsertError errWrongEmail = TempData["errorWrongEmail"] as InsertError;
            if (errWrongEmail != null)
            {
                ViewData["ErrorMessageWrongEmail"] = errWrongEmail.ErrorMessageWrongEmail;
            }

            InsertError errAddress = TempData["errorAddress"] as InsertError;
            if (errAddress != null)
            {
                ViewData["ErrorMessageEmptyAddress"] = errAddress.ErrorMessageEmptyAddress;
            }
            #endregion

            return View("~/Views/Customer/AddCustomer.cshtml", model);
        }
        public ActionResult SaveCustomer(string name, string phoneNumber, string email, string address)
        {
            var model = new CustomerModel();
            
            #region ErrorMessages
                if (String.IsNullOrEmpty(name))
                {
                    InsertError errorName = new InsertError();
                    errorName.ErrorMessageEmptyName = "Enter the name!";
                    TempData["errorName"] = errorName;         
                }
            
                if (String.IsNullOrEmpty(phoneNumber))
                {
                    InsertError errorEmptyPhone = new InsertError();
                    errorEmptyPhone.ErrorMessageEmptyPhoneNumber = "Enter the phone number!";
                    TempData["errorEmptyPhone"] = errorEmptyPhone;              
                }

                else if (Regex.Match(phoneNumber, @"^(\+[0-9]{12})$").Success == false)
                {
                    InsertError errorWrongPhone = new InsertError();
                    errorWrongPhone.ErrorMessageWrongPhone = "Wrong phone number!";
                    TempData["errorWrongPhone"] = errorWrongPhone;
                }

                if (String.IsNullOrEmpty(email))
                {
                    InsertError errorEmail = new InsertError();
                    errorEmail.ErrorMessageEmptyEmail = "Enter Email!";
                    TempData["errorEmail"] = errorEmail;
                }

                else if(new EmailAddressAttribute().IsValid(email) == false)
                {
                    InsertError errorWrongEmail = new InsertError();
                    errorWrongEmail.ErrorMessageWrongEmail = "Wrong Email!";
                    TempData["errorWrongEmail"] = errorWrongEmail;                
                }
               
                if (String.IsNullOrEmpty(address))
                {
                    InsertError errorAddress = new InsertError();
                    errorAddress.ErrorMessageEmptyAddress = "Enter the address!";
                    TempData["errorAddress"] = errorAddress;
                }
            #endregion

            if (TempData["error"] != null ||
                TempData["errorEmptyPhone"] != null ||
                TempData["errorWrongPhone"] != null || 
                TempData["errorEmail"] != null || 
                TempData["errorAddress"] != null || 
                TempData["errorWrongEmail"] != null)
            {
                model.CustomerObject.Name = name;
                model.CustomerObject.Email = email;
                model.CustomerObject.Address = address;
                model.CustomerObject.PhoneNumber = phoneNumber;

                return AddCustomerWithParams(model);
            }

            using (var context = new WHOLESALE_STOREEntities())
            {
                context.CUSTOMER.Add(new CUSTOMER { NAME = name, PHONE_NUMBER = phoneNumber, EMAIL = email, C_ADDRESS = address });
                context.SaveChanges();
            }
            return ShowCustomers();
        }
        public ActionResult DeleteCustomer(int? customerId)
        {
            #region ErrorMessages
            if (customerId.HasValue == false)
            {
                InsertError error = new InsertError();
                error.ErrorMessageEmptyCustomer = "Choose the customer!";
                TempData["errorEmptyCustomer"] = error;

                return RedirectToAction("ShowCustomers", "Customer");
            }
            #endregion

            using (var context = new WHOLESALE_STOREEntities())
            {
                var customerDB = context.CUSTOMER.Find(customerId);

                context.CUSTOMER.Remove(customerDB);
                context.SaveChanges();
            }
            return ShowCustomers();
        }
        public ActionResult EditCustomer(int? customerId)
        {
            #region ErrorMessages
            if (customerId.HasValue == false)
            {
                InsertError errorEdit = new InsertError();
                errorEdit.ErrorMessageEmptyEditCustomer = "Choose the customer!";
                TempData["errorEmptyEditCustomer"] = errorEdit;

                return RedirectToAction("ShowCustomers", "Customer");
            }
            #endregion

            var model = new CustomerModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                var customerDB = context.CUSTOMER.Find(customerId);
                
                model.CustomerObject.Id = customerDB.ID;
                model.CustomerObject.Name = customerDB.NAME;
                model.CustomerObject.Address = customerDB.C_ADDRESS;
                model.CustomerObject.Email = customerDB.EMAIL;
                model.CustomerObject.PhoneNumber = customerDB.PHONE_NUMBER;               

                return View("~/Views/Customer/EditCustomer.cshtml", model);
            }
        }
        public ActionResult SaveEditedCustomer(int customerId, string name, string phoneNumber, string email, string address)
        {
            using (var context = new WHOLESALE_STOREEntities())
            {
                var customerDB = context.CUSTOMER.Find(customerId);

                customerDB.NAME = name;
                customerDB.PHONE_NUMBER = phoneNumber;
                customerDB.EMAIL = email;
                customerDB.C_ADDRESS = address;
                context.SaveChanges();
            }
            return ShowCustomers();
        }
        public ActionResult ShowCustomerOrders(int? customerId)
        {
            #region ErrorMessages
            if (customerId.HasValue == false)
            {
                InsertError errorOrders = new InsertError();
                errorOrders.ErrorMessageEmptyOrdersCustomer = "Choose the customer!";
                TempData["errorEmptyOrdersCategory"] = errorOrders;

                return RedirectToAction("ShowCustomers", "Customer");
            }
            #endregion

            var model = new OrderModel();
            model.CurrentCustomerName = "";

            using (var context = new WHOLESALE_STOREEntities())
            {
                var customerDB = context.CUSTOMER.Find(customerId);
                model.CurrentCustomerName = customerDB.NAME.ToString();

                foreach (var or in context.ORDER.ToList())
                {
                    if (or.CUSTOMER_ID == customerId)
                    {
                        var customer = new Customer() { Name = or.CUSTOMER.NAME, Id = or.ID };

                        model.Orders.Add(new Order() { Id = or.ID, OrderNumber = or.ORDERNUMBER, Customer = customer, TotalSum = (decimal)or.TOTALSUM, Date = or.DATE });
                    }
                }
                context.SaveChanges();
            }
            return View("~/Views/Customer/ShowCustomerOrders.cshtml", model);
        }
    }
}
