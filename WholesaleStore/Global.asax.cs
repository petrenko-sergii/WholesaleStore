using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WholesaleStore
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FillData();
        }

        public void FillData()
        {
            FillUnits();
            FillDataCategories();
            FillDataCustomers();
            FillDataProducts();
            FillDataUserType();
            FillUsers();
            StartLoggedUser();
        }

        public void FillUnits()
        {
            using (var context = new WHOLESALE_STOREEntities())
            {
                if (!context.UNIT.Any())
                {
                    context.UNIT.Add(new UNIT() { NAME = "unit" });
                    context.UNIT.Add(new UNIT() { NAME = "pack" });
                    context.UNIT.Add(new UNIT() { NAME = "set" });
                    context.UNIT.Add(new UNIT() { NAME = "kg" });
                    context.SaveChanges();
                }
            }
        }
        public void FillDataProducts()
        {
            using (var context = new WHOLESALE_STOREEntities())
            {
                if (!context.PRODUCT.Any())
                {
                    context.PRODUCT.Add(new PRODUCT() { NAME = "Olive with seed \"Premia\", tin, 400g", BARCODE = "8411995003907", AMOUNT = 240, PRICE = 26.95m, CATEGORY_ID = 7,UNIT_ID = 1});
                    context.PRODUCT.Add(new PRODUCT() { NAME = "Chocolate black\"Korona\", 90g", BARCODE = "7622210354419", AMOUNT = 300, PRICE = 17.95m, CATEGORY_ID = 6, UNIT_ID = 1});
                    context.PRODUCT.Add(new PRODUCT() { NAME = "Oat flakes \"De Luxe\", 500g", BARCODE = "4299200001901", AMOUNT = 80, PRICE = 16.40m, CATEGORY_ID = 3, UNIT_ID = 1 });
                    context.PRODUCT.Add(new PRODUCT() { NAME = "Oat flakes \"Gulliver\", 450g", BARCODE = "4826895478205", AMOUNT = 120, PRICE = 12.95m, CATEGORY_ID = 3, UNIT_ID = 1 });
                    context.PRODUCT.Add(new PRODUCT() { NAME = "Tomato paste \"Gospodarochka\", 500g", BARCODE = "4828597547766", AMOUNT = 540, PRICE = 18.40m, CATEGORY_ID = 8, UNIT_ID = 1 });
                    context.PRODUCT.Add(new PRODUCT() { NAME = "Tomato paste \"Gospodarochka\", 3 x 125g", BARCODE = "4828597547769", AMOUNT = 200, PRICE = 14.10m, CATEGORY_ID = 8, UNIT_ID = 2 });
                    context.PRODUCT.Add(new PRODUCT() { NAME = "Water heavily gassed \"Myrgorodska\", 1L", BARCODE = "4820026589701", AMOUNT = 300, PRICE = 6.80m, CATEGORY_ID = 9, UNIT_ID = 1 });
                    context.PRODUCT.Add(new PRODUCT() { NAME = "Water heavily gassed \"Myrgorodska\", 0.5L", BARCODE = "4820026589702", AMOUNT = 360, PRICE = 5.20m, CATEGORY_ID = 9, UNIT_ID = 1 });
                    context.PRODUCT.Add(new PRODUCT() { NAME = "Sweet water heavily gassed \"Coca-cola\", 2L", BARCODE = "4826500458977", AMOUNT = 240, PRICE = 12.80m, CATEGORY_ID = 9, UNIT_ID = 1 });
                    context.PRODUCT.Add(new PRODUCT() { NAME = "Candies \"Lion\", packed, 250g", BARCODE = "7625871583871", AMOUNT = 70, PRICE = 16.25m, CATEGORY_ID = 4, UNIT_ID = 2 });
                    context.PRODUCT.Add(new PRODUCT() { NAME = "Chocolate bar \"Nuts\", 49g", BARCODE = "7625891254652", AMOUNT = 320, PRICE = 6.85m, CATEGORY_ID = 4, UNIT_ID = 1 });
                    context.PRODUCT.Add(new PRODUCT() { NAME = "Spaghetti \"Italian sun\", 6 types (6 x 0.4kg), ", BARCODE = "8157954468772", AMOUNT = 80, PRICE = 172.50m, CATEGORY_ID = 3, UNIT_ID = 3 });
                    context.SaveChanges();
                }
            }
        }
        public void FillDataCategories()
        {
            using (var context = new WHOLESALE_STOREEntities())
            {
                if (!context.CATEGORY.Any())
                {                    
                    context.CATEGORY.Add(new CATEGORY() { NAME = "Fruits" });
                    context.CATEGORY.Add(new CATEGORY() { NAME = "Vegetables" });
                    context.CATEGORY.Add(new CATEGORY() { NAME = "Crops" });
                    context.CATEGORY.Add(new CATEGORY() { NAME = "Sweets" });
                    context.CATEGORY.Add(new CATEGORY() { NAME = "Juice" });
                    context.CATEGORY.Add(new CATEGORY() { NAME = "Chocolate" });
                    context.CATEGORY.Add(new CATEGORY() { NAME = "Tins" });
                    context.CATEGORY.Add(new CATEGORY() { NAME = "Cans" });
                    context.CATEGORY.Add(new CATEGORY() { NAME = "Drinks" });
                    context.CATEGORY.Add(new CATEGORY() { NAME = "Alcohol" });
                    context.SaveChanges();
                }
            }
        }
        public void FillDataCustomers()
        {
            using (var context = new WHOLESALE_STOREEntities())
            {
                if (!context.CUSTOMER.Any())
                {
                    context.CUSTOMER.Add(new CUSTOMER() { NAME = "Kapynus Dmytro", C_ADDRESS = "Office 405, 9, Vasylkivska str, Kyiv", PHONE_NUMBER = "+380972563698", EMAIL = "kapynus@gmail.com" });
                    context.CUSTOMER.Add(new CUSTOMER() { NAME = "Marchenko Andrii", C_ADDRESS = "Office 203, 45-B, Velyka str, Kyiv", PHONE_NUMBER = "+380984879658", EMAIL = "marchenko@gmail.com" });
                    context.CUSTOMER.Add(new CUSTOMER() { NAME = "Bilyk Anatolii", C_ADDRESS = "Office 04, 48, Lomonosova str, Kyiv", PHONE_NUMBER = "+380506589655", EMAIL = "bilyk_av@ukr.net" });
                    context.CUSTOMER.Add(new CUSTOMER() { NAME = "Buzhyn Vasyl", C_ADDRESS = "63, Poshtova str, Kyiv", PHONE_NUMBER = "+380635425874", EMAIL = "buzhyn@foodsale.com.ua" });
                    context.SaveChanges();
                }
            }
        }
        public void FillDataUserType()
        {
            using (var context = new WHOLESALE_STOREEntities())
            {
                if (!context.USERTYPE.Any())
                {
                    context.USERTYPE.Add(new USERTYPE() { NAME = "Admin" });
                    context.USERTYPE.Add(new USERTYPE() { NAME = "Cashier" });
                    context.USERTYPE.Add(new USERTYPE() { NAME = "Bookkeeper" });
                    context.USERTYPE.Add(new USERTYPE() { NAME = "Director" });
                    context.SaveChanges();
                }
            }
        }
        public void FillUsers()
        {
            using (var context = new WHOLESALE_STOREEntities())
            {
                if (!context.USER.Any())
                {
                    context.USER.Add(new USER() { NAME = "Admin", PASSWORD = "111", USERTYPE_ID = 1 });
                    context.USER.Add(new USER() { NAME = "Klymenko Maryna", PASSWORD = "211", USERTYPE_ID = 2 });
                    context.USER.Add(new USER() { NAME = "Abramova Svitlana", PASSWORD = "212", USERTYPE_ID = 2 });
                    context.USER.Add(new USER() { NAME = "Sutulova Yulia", PASSWORD = "213", USERTYPE_ID = 2 });
                    context.USER.Add(new USER() { NAME = "Gorobyna Tetiana", PASSWORD = "311", USERTYPE_ID = 3 });
                    context.USER.Add(new USER() { NAME = "Mykolaienko Ivan", PASSWORD = "411", USERTYPE_ID = 4 });
                    context.SaveChanges();
                }
            }
        }
        public void StartLoggedUser()
        {
            using (var context = new WHOLESALE_STOREEntities())
            {
                if (!context.LOGGEDUSER.Any())
                {
                    context.LOGGEDUSER.Add(new LOGGEDUSER() { ID = 1, VALUE = null });
                    context.SaveChanges();
                }
            }
        }
    }
}