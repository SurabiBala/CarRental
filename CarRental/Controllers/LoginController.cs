using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.BPO;

namespace CarRental.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        public string GetState()
        {
            try
            {
                var result = LoginBPO.GetState();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public string GetDistrict()
        {
            try
            {
                var result = LoginBPO.GetDistrict();
                return result;
            }
            catch
            {
                return null;
            }
        }
        [HttpPost]
        public JsonResult RegisterCustomer(CustomerDTO data)
        {
            try
            {
                var result = LoginBPO.SaveCustomer(data);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public class CustomerDTO
        {
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string DOB { get; set; }
            public string Area { get; set; }
            public string StateCode { get; set; }
            public string DistrictCode { get; set; }
            public string CountryCode { get; set; }
            public string Pincode { get; set; }
        }


    }
}