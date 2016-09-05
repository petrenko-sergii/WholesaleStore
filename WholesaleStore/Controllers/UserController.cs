using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleStore.Models;
using System.Text.RegularExpressions;

namespace WholesaleStore.Controllers
{
    public class UserController : Controller
    {
        public ActionResult ShowUsers()
        {
            #region ErrorMessages
            InsertError errorUser = TempData["errorEmptyUser"] as InsertError;
            if (errorUser != null)
            {
                ViewData["ErrorMessageEmptyUser"] = errorUser.ErrorMessageEmptyUser;
            }

            InsertError errorNotAdminUser = TempData["errorNotAdminUser"] as InsertError;
            if (errorNotAdminUser != null)
            {
                ViewData["ErrorMessageNotAdminUser"] = errorNotAdminUser.ErrorMessageNotAdminUser;
            }
            #endregion

            var model = new UsersModel();
            int CurrentUserId;

            using (var context = new WHOLESALE_STOREEntities())
            {
                foreach (var temp in context.USER.ToList())
                {
                    var usertype = new UserType() { Name = temp.USERTYPE.NAME };

                    model.Users.Add(new User() { Id = temp.ID, Name = temp.NAME, Usertype = usertype });
                }
                model.Users = model.Users.OrderBy(u => u.Usertype.Name).ToList();

                var LoggedUserDB = context.LOGGEDUSER.First();

                CurrentUserId = Convert.ToInt16(LoggedUserDB.VALUE);
            }

            var view = View("~/Views/User/ShowUsers.cshtml", model); ;
            view.ViewBag.CurrentUserId = CurrentUserId;
            return view;
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

            InsertError errNotStrongPass = TempData["errorNotStrongPassword"] as InsertError;
            if (errNotStrongPass != null)
            {
                ViewData["ErrorMessageNotStrongPassword"] = errNotStrongPass.ErrorMessageNotStrongPassword;
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

            return View("~/Views/User/NewUser.cshtml", model);
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

            else if (Regex.Match(password, @"^[A-Za-z]\w{7,14}$").Success == false)
            {
                InsertError errorNotStrongPassword = new InsertError();
                errorNotStrongPassword.ErrorMessageNotStrongPassword = "Not Strong Password! ";
                TempData["errorNotStrongPassword"] = errorNotStrongPassword;
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
               TempData["errorNotStrongPassword"] != null ||
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
                return RedirectToAction("NewUser", "User", model);
            }

            else
            {
                using (var context = new WHOLESALE_STOREEntities())
                {
                    context.USER.Add(new USER { NAME = name, PASSWORD = password, USERTYPE_ID = Convert.ToInt16(userType.Value) });
                    context.SaveChanges();
                }
                return RedirectToAction("LoginPage", "Login");
            }
        }

        public ActionResult DeleteUser(int? userId)
        {
            #region ErrorMessages
            if (userId.HasValue == false)
            {
                InsertError error = new InsertError();
                error.ErrorMessageEmptyUser = "Choose the user!";
                TempData["errorEmptyUser"] = error;

                return RedirectToAction("ShowUsers", "User");
            }

            #endregion

            using (var context = new WHOLESALE_STOREEntities())
            {
                var userDB = context.USER.Find(userId);
                var LoggedUserDB = context.LOGGEDUSER.First();

                if (LoggedUserDB.VALUE != 1)
                {
                    InsertError errorNotAdmin = new InsertError();
                    errorNotAdmin.ErrorMessageNotAdminUser = "Delete Button is available only for Admin!";
                    TempData["errorNotAdminUser"] = errorNotAdmin;

                    return RedirectToAction("ShowUsers", "User");
                }

                context.USER.Remove(userDB);
                context.SaveChanges();
            }
            return RedirectToAction("ShowUsers", "User");
        }
    }
}
