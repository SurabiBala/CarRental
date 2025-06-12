using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.BPO;

namespace CarRental.Controllers
{
    public class AvailableCarsController : Controller
    {
        // GET: AvailableCars
        public ActionResult AvailableCars()
        {
            return View();
        }

        public string GetAvailableCars()
        {
            try
            {
                var result = AvailableCarsBPO.GetAvailableCars();
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}