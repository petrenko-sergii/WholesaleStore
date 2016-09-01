using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleStore.Models;

namespace WholesaleStore.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult ShowCategories()
        {
            #region ErrorMessages
            InsertError errorCategory = TempData["errorEmptyCategory"] as InsertError;
            if (errorCategory != null)
            {
                ViewData["ErrorMessageEmptyCategory"] = errorCategory.ErrorMessageEmptyCategory;
            }

            InsertError errorEditCategory = TempData["errorEmptyEditCategory"] as InsertError;
            if (errorEditCategory != null)
            {
                ViewData["ErrorMessageEmptyEditCategory"] = errorEditCategory.ErrorMessageEmptyEditCategory;
            }

            InsertError errorProdCategory = TempData["errorEmptyProdCategory"] as InsertError;
            if (errorProdCategory != null)
            {
                ViewData["ErrorMessageEmptyProdCategory"] = errorProdCategory.ErrorMessageEmptyProdCategory;
            }
            #endregion

            var model = new CategoryModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                foreach (var ct in context.CATEGORY.ToList())
                {
                    var category = new Category() { Id = ct.ID, Name = ct.NAME };
                    model.Categories.Add(category);
                }
            }
            return View("~/Views/Category/ShowCategories.cshtml", model);
        }
        public ActionResult SaveCategory(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                InsertError error = new InsertError();
                error.ErrorMessageEmptyName = "Enter the name!";
                TempData["error"] = error;
                return RedirectToAction("AddCategory","Category");
            }

            using (var context = new WHOLESALE_STOREEntities())
            {
                context.CATEGORY.Add(new CATEGORY { NAME = name });
                context.SaveChanges();
            }

            return ShowCategories();
        }
        public ActionResult AddCategory()
        {
            InsertError err = TempData["error"] as InsertError;
            if (err != null)
            {
                ViewData["ErrorMessageEmptyName"] = err.ErrorMessageEmptyName;
            }

            return View("~/Views/Category/AddCategory.cshtml");
        }
        public ActionResult DeleteCategory(int? categoryId)
        { 
            #region ErrorMessages
            if (categoryId.HasValue == false)
            {
                InsertError error = new InsertError();
                error.ErrorMessageEmptyCategory = "Choose the category!";
                TempData["errorEmptyCategory"] = error;

                return RedirectToAction("ShowCategories", "Category");
            }
            #endregion

            using (var context = new WHOLESALE_STOREEntities())
            {
                var categoryDB = context.CATEGORY.Find(categoryId);
                               
                context.CATEGORY.Remove(categoryDB);
                context.SaveChanges();
            }
            return ShowCategories();
        }
        public ActionResult EditCategory(int? categoryId)
        {
            #region ErrorMessages
            if (categoryId.HasValue == false)
            {
                InsertError errorEdit = new InsertError();
                errorEdit.ErrorMessageEmptyEditCategory = "Choose the category!";
                TempData["errorEmptyEditCategory"] = errorEdit;

                return RedirectToAction("ShowCategories", "Category");
            }
            #endregion

            var model = new CategoryModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                var categoryDB = context.CATEGORY.Find(categoryId);

                model.CategoryObject.Id = categoryDB.ID;
                model.CategoryObject.Name = categoryDB.NAME;

                return View("~/Views/Category/EditCategory.cshtml", model);
            }
        }
        public ActionResult SaveEditedCategory(int categoryId, string name)
        {
            using (var context = new WHOLESALE_STOREEntities())
            {
                var categoryDB = context.CATEGORY.Find(categoryId);

                categoryDB.NAME = name;
                context.SaveChanges();
            }
            return ShowCategories();
        }
        public ActionResult ShowCategoryProducts(int? categoryId)
        {
            #region ErrorMessages
            if (categoryId.HasValue == false)
            {
                InsertError errorProd = new InsertError();
                errorProd.ErrorMessageEmptyProdCategory = "Choose the category!";
                TempData["errorEmptyProdCategory"] = errorProd;

                return RedirectToAction("ShowCategories", "Category");
            }
            #endregion

            var model = new ProductModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                var categoryDB = context.CATEGORY.Find(categoryId);
                model.CurrentCategoryName = categoryDB.NAME.ToString();

                foreach (var pr in context.PRODUCT.ToList())
                {
                    if (pr.CATEGORY_ID == categoryId)
                    {
                        var category = new Category() { Name = pr.CATEGORY.NAME };
                        var unit = new Unit() { Name = pr.UNIT.NAME };

                        model.Products.Add(new Product() { Id = pr.ID, Name = pr.NAME, Amount = pr.AMOUNT, BarCode = pr.BARCODE, Price = pr.PRICE, Category = category, Unit = unit });
                    }
                }
                
                context.SaveChanges();
            }
            return View("~/Views/Category/ShowCategoryProducts.cshtml", model);
        }       
    }
}
