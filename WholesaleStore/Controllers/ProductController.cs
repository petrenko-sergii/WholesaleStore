using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleStore.Models;
using System.Text.RegularExpressions;

namespace WholesaleStore.Controllers
{
    public class ProductController : Controller
    {
        //public ActionResult ShowProducts(int userId)
        public ActionResult ShowProducts()
        {
            #region ErrorMessages
            InsertError errorEmptyProductId = TempData["errorEmptyProductId"] as InsertError;
            if (errorEmptyProductId != null)
            {
                ViewData["ErrorMessageEmptyProductId"] = errorEmptyProductId.ErrorMessageEmptyProductId;
            }

            InsertError errorEmptyEditProductId = TempData["errorEmptyEditProductId"] as InsertError;
            if (errorEmptyEditProductId != null)
            {
                ViewData["ErrorMessageEmptyEditProductId"] = errorEmptyEditProductId.ErrorMessageEmptyEditProductId;
            }

            InsertError errorEmptyAmountZeroProductId = TempData["errorEmptyAmountZeroProductId"] as InsertError;
            if (errorEmptyAmountZeroProductId != null)
            {
                ViewData["ErrorMessageEmptyAmountZeroProductId"] = errorEmptyAmountZeroProductId.ErrorMessageEmptyAmountZeroProductId;
            }

            #region ErrorsChangeProductAmount

            InsertError errorEmptyAmountChangeProductId = TempData["errorEmptyAmountChangeProductId"] as InsertError;
            if (errorEmptyAmountChangeProductId != null)
            {
                ViewData["ErrorMessageEmptyAmountChangeProductId"] = errorEmptyAmountChangeProductId.ErrorMessageEmptyAmountChangeProductId;
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
            #region ErrorsAddProductAmount
            InsertError errorEmptyAmountAddProductId = TempData["errorEmptyAmountAddProductId"] as InsertError;
            if (errorEmptyAmountAddProductId != null)
            {
                ViewData["ErrorMessageEmptyAmountAddProductId"] = errorEmptyAmountAddProductId.ErrorMessageEmptyAmountAddProductId;
            }

            InsertError errEmptyAddAmount = TempData["errorEmptyAddAmount"] as InsertError;
            if (errEmptyAddAmount != null)
            {
                ViewData["ErrorMessageEmptyAddAmount"] = errEmptyAddAmount.ErrorMessageEmptyAddAmount;
            }

            InsertError errWrongAddAmount = TempData["errorWrongAddAmount"] as InsertError;
            if (errWrongAddAmount != null)
            {
                ViewData["ErrorMessageWrongAddAmount"] = errWrongAddAmount.ErrorMessageWrongAddAmount;
            }
            #endregion
            #region ErrorsSetNewProductPrice
            InsertError errorEmptyPriceProductId = TempData["errorEmptyPriceProductId"] as InsertError;
            if (errorEmptyPriceProductId != null)
            {
                ViewData["ErrorMessageEmptyPriceProductId"] = errorEmptyPriceProductId.ErrorMessageEmptyPriceProductId;
            }

            InsertError errEmptyPrice = TempData["errorEmptyPrice"] as InsertError;
            if (errEmptyPrice != null)
            {
                ViewData["ErrorMessageEmptyPrice"] = errEmptyPrice.ErrorMessageEmptyPrice;
            }

            InsertError errWrongPrice = TempData["errorWrongPrice"] as InsertError;
            if (errWrongPrice != null)
            {
                ViewData["ErrorMessageWrongPrice"] = errWrongPrice.ErrorMessageWrongPrice;
            }
            #endregion

            #endregion

            var model = new ProductModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                foreach (var pr in context.PRODUCT.OrderBy(p => p.NAME).ToList())
                {
                    var category = new Category() { Name = pr.CATEGORY.NAME };
                    var unit = new Unit() { Name = pr.UNIT.NAME };

                    model.Products.Add(new Product() { Id = pr.ID, Name = pr.NAME, BarCode = pr.BARCODE, Amount = pr.AMOUNT, Price = pr.PRICE, Category = category, Unit = unit });
                }
            }
           
            var view = View("~/Views/Product/ShowProducts.cshtml", model);

           // view.ViewBag.userId = userId;

            return view;

        }
        public ActionResult AddProduct(int? category)
        {
            #region ErrorMessages
            InsertError errName = TempData["errorName"] as InsertError;
            if (errName != null)
            {
                ViewData["ErrorMessageEmptyName"] = errName.ErrorMessageEmptyName;
            }

            InsertError errorEmptyBarCode = TempData["errorEmptyBarCode"] as InsertError;
            if (errorEmptyBarCode != null)
            {
                ViewData["ErrorMessageEmptyBarCode"] = errorEmptyBarCode.ErrorMessageEmptyBarCode;
            }

            InsertError errWrongBarCode = TempData["errorWrongBarCode"] as InsertError;
            if (errWrongBarCode != null)
            {
                ViewData["ErrorMessageWrongBarCode"] = errWrongBarCode.ErrorMessageWrongBarCode;
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

            InsertError errEmptyPrice = TempData["errorEmptyPrice"] as InsertError;
            if (errEmptyPrice != null)
            {
                ViewData["ErrorMessageEmptyPrice"] = errEmptyPrice.ErrorMessageEmptyPrice;
            }

            InsertError errWrongPrice = TempData["errorWrongPrice"] as InsertError;
            if (errWrongPrice != null)
            {
                ViewData["ErrorMessageWrongPrice"] = errWrongPrice.ErrorMessageWrongPrice;
            }

            InsertError errEmptyCategory = TempData["errorEmptyCategory"] as InsertError;
            if (errEmptyCategory != null)
            {
                ViewData["ErrorMessageEmptyCategory"] = errEmptyCategory.ErrorMessageEmptyCategory;
            }

            InsertError errEmptyUnit = TempData["errorEmptyUnit"] as InsertError;
            if (errEmptyUnit != null)
            {
                ViewData["ErrorMessageEmptyUnit"] = errEmptyUnit.ErrorMessageEmptyUnit;
            }
            #endregion
            
            var model = new ProductModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                foreach (var ct in context.CATEGORY.ToList())
                {
                    model.Categories.Add(new Category() { Name = ct.NAME, Id = ct.ID });
                }
                foreach (var unit in context.UNIT.ToList())
                {
                    model.Units.Add(new Unit() { Name = unit.NAME, Id = unit.ID });
                }

            }
            return View("~/Views/Product/AddProduct.cshtml", model);
        }
        public ActionResult AddProductWithParams(ProductModel model)
        {
            #region ErrorMessages
            InsertError errName = TempData["errorName"] as InsertError;
            if (errName != null)
            {
                ViewData["ErrorMessageEmptyName"] = errName.ErrorMessageEmptyName;
            }

            InsertError errorEmptyBarCode = TempData["errorEmptyBarCode"] as InsertError;
            if (errorEmptyBarCode != null)
            {
                ViewData["ErrorMessageEmptyBarCode"] = errorEmptyBarCode.ErrorMessageEmptyBarCode;
            }

            InsertError errWrongBarCode = TempData["errorWrongBarCode"] as InsertError;
            if (errWrongBarCode != null)
            {
                ViewData["ErrorMessageWrongBarCode"] = errWrongBarCode.ErrorMessageWrongBarCode;
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

            InsertError errEmptyPrice = TempData["errorEmptyPrice"] as InsertError;
            if (errEmptyPrice != null)
            {
                ViewData["ErrorMessageEmptyPrice"] = errEmptyPrice.ErrorMessageEmptyPrice;
            }

            InsertError errWrongPrice = TempData["errorWrongPrice"] as InsertError;
            if (errWrongPrice != null)
            {
                ViewData["ErrorMessageWrongPrice"] = errWrongPrice.ErrorMessageWrongPrice;
            }

            InsertError errEmptyCategory = TempData["errorEmptyCategory"] as InsertError;
            if (errEmptyCategory != null)
            {
                ViewData["ErrorMessageEmptyCategory"] = errEmptyCategory.ErrorMessageEmptyCategory;
            }

            InsertError errEmptyUnit = TempData["errorEmptyUnit"] as InsertError;
            if (errEmptyUnit != null)
            {
                ViewData["ErrorMessageEmptyUnit"] = errEmptyUnit.ErrorMessageEmptyUnit;
            }
            #endregion

            return View("~/Views/Product/AddProduct.cshtml", model);
        }
        public ActionResult SaveProduct(string name, string barcode, float? amount, decimal? price, int? category, int? unit)
        {
            var model = new ProductModel();

            #region ErrorMessages
            if (String.IsNullOrEmpty(name))
            {
                InsertError errorName = new InsertError();
                errorName.ErrorMessageEmptyName = "Enter the name!";
                TempData["errorName"] = errorName;
            }

            if (String.IsNullOrEmpty(barcode))
            {
                InsertError errorEmptyBarCode = new InsertError();
                errorEmptyBarCode.ErrorMessageEmptyBarCode = "Enter the Barcode!";
                TempData["errorEmptyBarCode"] = errorEmptyBarCode;
            }

            else if (Regex.Match(barcode, @"^([0-9]{13})$").Success == false)
            {
                InsertError errorWrongBarCode = new InsertError();
                errorWrongBarCode.ErrorMessageWrongBarCode = "Wrong Barcode (must be 13 digits)! ";
                TempData["errorWrongBarCode"] = errorWrongBarCode;
            }

            if (amount == null)
            {
                InsertError errorEmptyAmount = new InsertError();
                errorEmptyAmount.ErrorMessageEmptyAmount = "Enter the Amount!";
                TempData["errorEmptyAmount"] = errorEmptyAmount;
            }

            else if (amount < 0)
            {
                InsertError errorWrongAmount = new InsertError();
                errorWrongAmount.ErrorMessageWrongAmount = "Wrong Amount! Can not be negative!";
                TempData["errorWrongAmount"] = errorWrongAmount;
            }

            if (price == null)
            {
                InsertError errorEmptyPrice = new InsertError();
                errorEmptyPrice.ErrorMessageEmptyPrice = "Enter the Price!";
                TempData["errorEmptyPrice"] = errorEmptyPrice;
            }

            else if (price <= 0)
            {
                InsertError errorWrongPrice = new InsertError();
                errorWrongPrice.ErrorMessageWrongPrice = "Wrong Price! Must be more than 0!";
                TempData["errorWrongPrice"] = errorWrongPrice;
            }

            if (category.HasValue == false)
            {
                InsertError errorEmptyCategory = new InsertError();
                errorEmptyCategory.ErrorMessageEmptyCategory = "Choose the category or create new!";
                TempData["errorEmptyCategory"] = errorEmptyCategory;
            }

            if (unit.HasValue == false)
            {
                InsertError errorEmptyUnit = new InsertError();
                errorEmptyUnit.ErrorMessageEmptyUnit = "Choose the unit or create new!";
                TempData["errorEmptyUnit"] = errorEmptyUnit;
            }           
            #endregion

            if (TempData["errorName"] != null ||
               TempData["errorEmptyBarCode"] != null ||
               TempData["errorWrongBarCode"] != null ||
               TempData["errorEmptyAmount"] != null ||
               TempData["errorWrongAmount"] != null ||
               TempData["errorEmptyPrice"] != null ||
               TempData["errorWrongPrice"] != null ||
               TempData["errorEmptyCategory"] != null ||
               TempData["errorEmptyUnit"] != null
               )
            {
                using (var context = new WHOLESALE_STOREEntities())
                {
                    model.ProductObject.Name = name;
                    model.ProductObject.BarCode = barcode;
                    model.ProductObject.Amount = Convert.ToSingle(amount);
                    model.ProductObject.Price = Convert.ToDecimal(price);                    

                    if (category.HasValue)
                    {
                        model.ProductObject.Category = new Category();
                        var categoryDB = context.CATEGORY.Find(category);
                        
                        model.ProductObject.Category.Id = categoryDB.ID;                        
                        model.CurrentCategoryId = Convert.ToInt32(category.Value);
                    }
                    
                    if (unit.HasValue)
                    {
                        model.ProductObject.Unit = new Unit();
                        var unitDB = context.UNIT.Find(unit);
                       
                        model.ProductObject.Unit.Id = unitDB.ID;
                        model.CurrentUnitId = Convert.ToInt32(unit.Value);
                    }

                    foreach (var ct in context.CATEGORY.ToList())
                    {
                        model.Categories.Add(new Category() { Name = ct.NAME, Id = ct.ID });
                    }
                    foreach (var _unit in context.UNIT.ToList())
                    {
                        model.Units.Add(new Unit() { Name = _unit.NAME, Id = _unit.ID });
                    }
                }

                return AddProductWithParams(model);
            }
            
            using (var context = new WHOLESALE_STOREEntities())
            {
                context.PRODUCT.Add(new PRODUCT { NAME = name, BARCODE = barcode, AMOUNT = Convert.ToSingle(amount), PRICE = Convert.ToDecimal(price), CATEGORY_ID = Convert.ToInt16(category), UNIT_ID = Convert.ToInt16(unit) });
                context.SaveChanges();
            }
            return ShowProducts();
        }
        public ActionResult SaveEditedProduct(int productId, string name, string barcode, float amount, decimal price, int category, int unit)
        {
            using (var context = new WHOLESALE_STOREEntities())
            {
                var productDB = context.PRODUCT.Find(productId);
                var categoryDB = context.CATEGORY.Find(category);
                var unitDB = context.UNIT.Find(unit);

                productDB.NAME = name;
                productDB.BARCODE = barcode;
                productDB.AMOUNT = amount;
                productDB.PRICE = price;
                productDB.CATEGORY = categoryDB;
                productDB.UNIT = unitDB;
                context.SaveChanges();
            }
            return ShowProducts();
        }
        public ActionResult DeleteProduct(int? productId)
        {
            #region ErrorMessages
            if (productId.HasValue == false)
            {
                InsertError errorEmptyProductId = new InsertError();
                errorEmptyProductId.ErrorMessageEmptyProductId = "Choose the product!";
                TempData["errorEmptyProductId"] = errorEmptyProductId;

                return RedirectToAction("ShowProducts", "Product");
            }
            #endregion

            using (var context = new WHOLESALE_STOREEntities())
            {
                var productDB = context.PRODUCT.Find(productId);

                context.PRODUCT.Remove(productDB);
                context.SaveChanges();
            }
            return ShowProducts();
        }
        public ActionResult EditProduct(int? productId)
        {
            #region ErrorMessages
            if (productId.HasValue == false)
            {
                InsertError errorEmptyEditProductId = new InsertError();
                errorEmptyEditProductId.ErrorMessageEmptyEditProductId = "Choose the product!";
                TempData["errorEmptyEditProductId"] = errorEmptyEditProductId;

                return RedirectToAction("ShowProducts", "Product");
            }
            #endregion

            var model = new ProductModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                var productDB = context.PRODUCT.Find(productId);
                var categoryDB = new Category() { Name = productDB.CATEGORY.NAME, Id = productDB.CATEGORY.ID };
                var unitDB = new Unit() { Name = productDB.UNIT.NAME, Id = productDB.UNIT.ID };

                model.ProductObject.Id = productDB.ID;
                model.ProductObject.Name = productDB.NAME;
                model.ProductObject.BarCode = productDB.BARCODE;
                model.ProductObject.Amount = productDB.AMOUNT;
                model.ProductObject.Unit = unitDB;
                model.ProductObject.Price = productDB.PRICE;
                model.ProductObject.Category = categoryDB;

                foreach (var ct in context.CATEGORY.ToList())
                {
                    model.Categories.Add(new Category() { Name = ct.NAME, Id = ct.ID });
                }
                foreach (var unit in context.UNIT.ToList())
                {
                    model.Units.Add(new Unit() { Name = unit.NAME, Id = unit.ID });
                }

                return View("~/Views/Product/EditProduct.cshtml", model);
            }
        }
        public ActionResult ProductAmountToZero(int? productId)
        {
            #region ErrorMessages
            if (productId.HasValue == false)
            {
                InsertError errorEmptyAmountZeroProductId = new InsertError();
                errorEmptyAmountZeroProductId.ErrorMessageEmptyAmountZeroProductId = "Choose the product!";
                TempData["errorEmptyAmountZeroProductId"] = errorEmptyAmountZeroProductId;

                return RedirectToAction("ShowProducts", "Product");
            }
            #endregion

            var model = new ProductModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                var productDB = context.PRODUCT.Find(productId);

                productDB.AMOUNT = 0;
                context.SaveChanges();
            }
            return RedirectToAction("ShowProducts");
        }
        public ActionResult ChangeProductAmount(int? productId, float? newAmount)
        {
            #region ErrorMessages
            if (productId.HasValue == false)
            {
                InsertError errorProductId = new InsertError();
                errorProductId.ErrorMessageEmptyAmountChangeProductId = "Choose the product!";
                TempData["errorEmptyAmountChangeProductId"] = errorProductId;  
            }

            if (newAmount == null)
            {
                InsertError errorEmptyAmount = new InsertError();
                errorEmptyAmount.ErrorMessageEmptyAmount = "-----Enter the Amount!";
                TempData["errorEmptyAmount"] = errorEmptyAmount;
            }

            else if (newAmount < 0)
            {
                InsertError errorWrongAmount = new InsertError();
                errorWrongAmount.ErrorMessageWrongAmount = "-----Wrong Amount! Can not be negative!";
                TempData["errorWrongAmount"] = errorWrongAmount;
            }
            #endregion

            if (TempData["errorEmptyAmountChangeProductId"] != null ||
               TempData["errorEmptyAmount"] != null ||
               TempData["errorWrongAmount"] != null
               )
            {
                return RedirectToAction("ShowProducts", "Product");
            }

            var model = new ProductModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                var productDB = context.PRODUCT.Find(productId);

                productDB.AMOUNT = Convert.ToSingle(newAmount);
                context.SaveChanges();
            }
            return RedirectToAction("ShowProducts");
        }
        public ActionResult AddProductAmount(int? productId, float? addAmount)
        {
            #region ErrorMessages
            if (productId.HasValue == false)
            {
                InsertError errorProductId = new InsertError();
                errorProductId.ErrorMessageEmptyAmountAddProductId = "Choose the product!";
                TempData["errorEmptyAmountAddProductId"] = errorProductId;
            }

            if (addAmount == null)
            {
                InsertError errorEmptyAmount = new InsertError();
                errorEmptyAmount.ErrorMessageEmptyAddAmount = "-----Enter the Amount!";
                TempData["errorEmptyAddAmount"] = errorEmptyAmount;
            }

            else if (addAmount < 0)
            {
                InsertError errorWrongAmount = new InsertError();
                errorWrongAmount.ErrorMessageWrongAddAmount = "-----Wrong Amount! Can not be negative!";
                TempData["errorWrongAddAmount"] = errorWrongAmount;
            }
            #endregion

            if (TempData["errorEmptyAmountAddProductId"] != null ||
               TempData["errorEmptyAddAmount"] != null ||
               TempData["errorWrongAddAmount"] != null
               )
            {
                return RedirectToAction("ShowProducts", "Product");
            }

            var model = new ProductModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                var productDB = context.PRODUCT.Find(productId);

                productDB.AMOUNT += Convert.ToSingle(addAmount);
                context.SaveChanges();
            }
            return RedirectToAction("ShowProducts");
        }
        public ActionResult SetNewProductPrice(int? productId, decimal? newPrice)
        {
            #region ErrorMessages
            if (productId.HasValue == false)
            {
                InsertError errorProductPriceId = new InsertError();
                errorProductPriceId.ErrorMessageEmptyPriceProductId = "Choose the product!";
                TempData["errorEmptyPriceProductId"] = errorProductPriceId;
            }

            if (newPrice == null)
            {
                InsertError errorEmptyPrice = new InsertError();
                errorEmptyPrice.ErrorMessageEmptyPrice = "-----Enter the price!";
                TempData["errorEmptyPrice"] = errorEmptyPrice;
            }

            else if (newPrice < 0)
            {
                InsertError errorWrongPrice = new InsertError();
                errorWrongPrice.ErrorMessageWrongPrice = "-----Wrong Price! Can not be negative!";
                TempData["errorWrongPrice"] = errorWrongPrice;
            }
            #endregion

            if (TempData["errorEmptyPriceProductId"] != null ||
               TempData["errorEmptyPrice"] != null ||
               TempData["errorWrongPrice"] != null
               )
            {
                return RedirectToAction("ShowProducts", "Product");
            }
            var model = new ProductModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                var productDB = context.PRODUCT.Find(productId);

                productDB.PRICE = Convert.ToDecimal(newPrice);
                context.SaveChanges();
            }
            return RedirectToAction("ShowProducts");
        }
        public ActionResult FindProducts(string searchString)
        {
            var model = new ProductModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                foreach (var pr in context.PRODUCT.Where(s => s.NAME.Contains(searchString)).ToList())
                {
                    var category = new Category() { Name = pr.CATEGORY.NAME };
                    var unit = new Unit() { Name = pr.UNIT.NAME };

                    model.Products.Add(new Product() { Id = pr.ID, Name = pr.NAME, BarCode = pr.BARCODE, Amount = pr.AMOUNT, Price = pr.PRICE, Category = category, Unit = unit });
                }
                model.Products = model.Products.OrderBy(p => p.Name).ToList();
            }
            return View("~/Views/Product/ShowProducts.cshtml", model);
        }
    }
}
