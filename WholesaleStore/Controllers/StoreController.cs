using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleStore.Models;

namespace WholesaleStore.Controllers
{
    public class StoreController : Controller
    {
        public ActionResult ShowStore()
        {
            var model = new ProductModel();
            model.TotalStoreSum = 0;

            using (var context = new WHOLESALE_STOREEntities())
            {
                foreach (var pr in context.PRODUCT.OrderBy(p => p.NAME).ToList())
                {
                    if (pr.AMOUNT != 0)
                    {
                        var category = new Category() { Name = pr.CATEGORY.NAME };
                        var unit = new Unit() { Name = pr.UNIT.NAME };

                        model.Products.Add(new Product() { Id = pr.ID, Name = pr.NAME, BarCode = pr.BARCODE, Amount = pr.AMOUNT, Price = pr.PRICE, Category = category, Unit = unit });
                        model.TotalStoreSum += (decimal)pr.AMOUNT * pr.PRICE;
                    }
                }
            }
            return View("~/Views/Store/ShowStore.cshtml", model);
        }  
    }
}
