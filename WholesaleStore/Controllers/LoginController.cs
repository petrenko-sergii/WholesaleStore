using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleStore.Models;

namespace WholesaleStore.Controllers
{
    public class LoginController : Controller
    {
        void CheckPasswordIsNull(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                InsertError errorPassword = new InsertError();
                errorPassword.ErrorMessagePassword = "Enter the password!";
                TempData["errorPassword"] = errorPassword;
            }
        }
         public ActionResult LoginPage(int? user)
        {
            #region ErrorMessages
            InsertError errLog = TempData["errorLog"] as InsertError;
            if (errLog != null)
            {
                ViewData["ErrorMessageLog"] = errLog.ErrorMessageLog;
            }

            InsertError errPass = TempData["errorPassword"] as InsertError;
            if (errPass != null)
            {
                ViewData["ErrorMessagePassword"] = errPass.ErrorMessagePassword;
            }
            #endregion

            using (var context = new WHOLESALE_STOREEntities())
            {
               var LoggedUserDB = context.LOGGEDUSER.First();
               LoggedUserDB.VALUE = 0;
            }

            var model = new UsersModel();          
           
             model.CurrentUserId = 0;
            
            if (user.HasValue)
            {
                model.CurrentUserId = Convert.ToInt32(user.Value);               
            }

            using (var context = new WHOLESALE_STOREEntities())
            {
                foreach (var userItem in context.USER.ToList())
                {
                    var usertype = new UserType() { Name = userItem.USERTYPE.NAME };
                    model.Users.Add(new User() { Id = userItem.ID, Name = userItem.NAME, Usertype = usertype });                    
                }
            }
            model.Users = model.Users.OrderBy(item => item.Usertype.Name).ToList();

            return View(model);        
        }
        public ActionResult CheckLogin(int? user, string password)
        {
            #region ErrorMessagesRedirectToAction
            if (user == null)
            {
                InsertError errorLog = new InsertError();
                errorLog.ErrorMessageLog = "Choose user!";
                TempData["errorLog"] = errorLog;
                CheckPasswordIsNull(password);

                return RedirectToAction("LoginPage", "Login");
            }
           
            if (String.IsNullOrEmpty(password))
            {
                InsertError errorPassword = new InsertError();
                errorPassword.ErrorMessagePassword = "Enter the password!";
                TempData["errorPassword"] = errorPassword;

                return RedirectToAction("LoginPage", "Login", new { User = user });
            }
            #endregion

            using (var context = new WHOLESALE_STOREEntities())
            {
                var userDB = context.USER.Find(user);

                if (userDB.PASSWORD.ToString() == password)
                {
                    var LoggedUserDB = context.LOGGEDUSER.First();
                    LoggedUserDB.VALUE = userDB.ID;                    
                    context.SaveChanges();

                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    InsertError errorPassword = new InsertError();
                    errorPassword.ErrorMessagePassword = "Wrong password! Try again!";
                    TempData["errorPassword"] = errorPassword;

                    return RedirectToAction("LoginPage", "Login", new { User = user });
                }
            }
        }
        public ActionResult NewUser(UserTypeModel model)
        {
            #region ErrorMessages
            InsertError errLog = TempData["errorLog"] as InsertError;
            if (errLog != null)
            {
                ViewData["ErrorMessageLog"] = errLog.ErrorMessageLog;
            }

            InsertError errPass = TempData["errorPassword"] as InsertError;
            if (errPass != null)
            {
                ViewData["ErrorMessagePassword"] = errPass.ErrorMessagePassword;
            }

            InsertError errEmptyConfirmPass = TempData["errorEmptyConfirmPassword"] as InsertError;
            if (errEmptyConfirmPass != null)
            {
                ViewData["ErrorMessageEmptyConfirmPassword"] = errEmptyConfirmPass.ErrorMessageEmptyConfirmPassword;
            }

            InsertError errConfirmPass = TempData["errorConfirmPassword"] as InsertError;
            if (errConfirmPass != null)
            {
                ViewData["ErrorMessageConfirmPassword"] = errConfirmPass.ErrorMessageConfirmPassword;
            }


            InsertError errEmptyUserType = TempData["errorEmptyUserType"] as InsertError;
            if (errEmptyUserType != null)
            {
                ViewData["ErrorMessageEmptyUserType"] = errEmptyUserType.ErrorMessageEmptyUserType;
            }
            #endregion

            using (var context = new WHOLESALE_STOREEntities())
            {
                foreach (var usertype in context.USERTYPE.ToList())
                {
                    model.UserTypes.Add(new UserType() { Name = usertype.NAME, Id = usertype.ID });
                }
            }
            
            return View("~/Views/Login/NewUser.cshtml",model);
        }
        public ActionResult SaveUser(string name, string password, string confirmPassword, int? userType)
        {
            var model = new UserTypeModel();

            #region ErrorMessagesRedirectToAction
            if (String.IsNullOrEmpty(name))
            {
                InsertError errorLog = new InsertError();
                errorLog.ErrorMessageLog = "Enter new user surname/name!";
                TempData["errorLog"] = errorLog;               
            }

            if (String.IsNullOrEmpty(password))
            {
                InsertError errorPassword = new InsertError();
                errorPassword.ErrorMessagePassword = "Enter the password!";
                TempData["errorPassword"] = errorPassword;
            }

            if (String.IsNullOrEmpty(confirmPassword))
            {
                InsertError errorEmptyConfirmPassword = new InsertError();
                errorEmptyConfirmPassword.ErrorMessageEmptyConfirmPassword = "Enter the confirm password!";
                TempData["errorEmptyConfirmPassword"] = errorEmptyConfirmPassword;
            }

            if (password != confirmPassword)
            {
                InsertError errorConfirmPassword = new InsertError();
                errorConfirmPassword.ErrorMessageConfirmPassword = "Confirm password is not right!";
                TempData["errorConfirmPassword"] = errorConfirmPassword;
            }

            if (userType.HasValue == false)
            {
                InsertError errorEmptyUserType = new InsertError();
                errorEmptyUserType.ErrorMessageEmptyUserType = "Choose the UserType!";
                TempData["errorEmptyUserType"] = errorEmptyUserType;
            }
            #endregion


            if (TempData["errorLog"] != null ||
               TempData["errorPassword"] != null ||
               TempData["errorConfirmPassword"] != null ||
               TempData["errorEmptyConfirmPassword"] != null ||
               TempData["errorEmptyUserType"] != null
               )
            {
                model.NewUserName = name;
                if (userType.HasValue)
                {
                    model.CurrentUserTypeId = Convert.ToInt32(userType.Value);
                }
                return RedirectToAction("NewUser", "Login", model);
            }

            else
            {
                using (var context = new WHOLESALE_STOREEntities())
                {
                    context.USER.Add(new USER { NAME = name, PASSWORD = password, USERTYPE_ID = Convert.ToInt16(userType.Value) });
                    context.SaveChanges();
                }
                return View("~/Views/ItemSavedTemp.cshtml");
            }
        }
    }
}
