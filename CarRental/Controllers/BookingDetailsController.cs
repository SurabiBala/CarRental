using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.BPO;

namespace CarRental.Controllers
{
    public class BookingDetailsController : Controller
    {
        // GET: BookingDetails
        public ActionResult BookingDetails()
        {
            return View();
        }

        public string GetBookingDetails()
        {
            try
            {
                var result = BookingDetailsBPO.GetBookingDetails();
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}