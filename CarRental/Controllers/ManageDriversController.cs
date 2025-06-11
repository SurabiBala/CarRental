using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.BPO;

namespace CarRental.Controllers
{
    public class ManageDriversController : Controller
    {
        // GET: ManageDrivers
        public ActionResult ManageDrivers()
        {
            return View();
        }

        public string GetDriversList()
        {
            try
            {
                var result = ManageDriversBPO.GetDriversList();
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}