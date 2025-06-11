using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.BPO;

namespace CarRental.Controllers
{
    public class ManageCarsController : Controller
    {
        // GET: ManageCars
        public ActionResult ManageCars()
        {
            return View();
        }

        public string GetCarsList()
        {
            try
            {
                var result = ManageCarsBPO.GetCarsList();
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}