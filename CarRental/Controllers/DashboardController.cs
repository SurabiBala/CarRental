using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.BPO;

namespace CarRental.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }
        public string GetCarStatusCount()
        {
            try
            {
                var result = DashboardBPO.GetCarStatusCount();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public string GetDriverStatusCount()
        {
            try
            {
                var result = DashboardBPO.GetDriverStatusCount();
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}