using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleStore.Models;

namespace WholesaleStore.Controllers
{
    public class UnitController : Controller
    {
        public ActionResult ShowUnits()
        {
            #region ErrorMessages
            InsertError errorUnit = TempData["errorEmptyUnit"] as InsertError;
            if (errorUnit != null)
            {
                ViewData["ErrorMessageEmptyUnit"] = errorUnit.ErrorMessageEmptyUnit;
            }

            InsertError errorEditUnit = TempData["errorEmptyEditUnit"] as InsertError;
            if (errorEditUnit != null)
            {
                ViewData["ErrorMessageEmptyEditUnit"] = errorEditUnit.ErrorMessageEmptyEditUnit;
            }
            #endregion

            var model = new UnitModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                foreach (var unitDB in context.UNIT.ToList())
                {
                    model.Units.Add(new Unit() { Id = unitDB.ID, Name = unitDB.NAME });
                }
            }
            return View("~/Views/Unit/ShowUnits.cshtml", model);

        }
        public ActionResult AddUnit()
        {
            InsertError err = TempData["error"] as InsertError;
            if (err != null)
            {
                ViewData["ErrorMessageEmptyName"] = err.ErrorMessageEmptyName;
            }

            return View("~/Views/Unit/AddUnit.cshtml");
        }
        public ActionResult SaveUnit(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                InsertError error = new InsertError();
                error.ErrorMessageEmptyName = "Enter the name!";
                TempData["error"] = error;
                return RedirectToAction("AddUnit", "Unit");
            }

            using (var context = new WHOLESALE_STOREEntities())
            {
                context.UNIT.Add(new UNIT { NAME = name });
                context.SaveChanges();
            }
            return ShowUnits();
        }
        public ActionResult DeleteUnit(int? unitId)
        {
            #region ErrorMessages
            if (unitId.HasValue == false)
            {
                InsertError error = new InsertError();
                error.ErrorMessageEmptyUnit = "Choose the unit!";
                TempData["errorEmptyUnit"] = error;

                return RedirectToAction("ShowUnits", "Unit");
            }
            #endregion

            using (var context = new WHOLESALE_STOREEntities())
            {
                var UnitDB = context.UNIT.Find(unitId);

                context.UNIT.Remove(UnitDB);
                context.SaveChanges();
            }
            return ShowUnits();
        }
        public ActionResult EditUnit(int? unitId)
        {
            #region ErrorMessages
            if (unitId.HasValue == false)
            {
                InsertError errorEdit = new InsertError();
                errorEdit.ErrorMessageEmptyEditUnit = "Choose the unit!";
                TempData["errorEmptyEditUnit"] = errorEdit;

                return RedirectToAction("ShowUnits", "Unit");
            }

            InsertError err = TempData["error"] as InsertError;
            if (err != null)
            {
                ViewData["ErrorMessageEmptyName"] = err.ErrorMessageEmptyName;
            }
            #endregion

            var model = new UnitModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                var UnitDB = context.UNIT.Find(unitId);

                model.UnitObject.Id = UnitDB.ID;
                model.UnitObject.Name = UnitDB.NAME;

                return View("~/Views/Unit/EditUnit.cshtml", model);
            }
        }
        public ActionResult SaveEditedUnit(int unitId, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                InsertError error = new InsertError();
                error.ErrorMessageEmptyName = "Enter the name!";
                TempData["error"] = error;

                return RedirectToAction("EditUnit", "Unit", new { unitId });
            }


            using (var context = new WHOLESALE_STOREEntities())
            {
                var UnitDB = context.UNIT.Find(unitId);

                UnitDB.NAME = name;
                context.SaveChanges();
            }
            return ShowUnits();
        }
    }
}
