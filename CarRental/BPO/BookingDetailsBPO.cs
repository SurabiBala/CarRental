using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Helpers;
using Newtonsoft.Json;

namespace CarRental.BPO
{
    public class BookingDetailsBPO 
    {
        public static string Conn = CommonConnection.Conn;
        public static string GetBookingDetails()
        {
            try
            {
                string query = @"select bd.c_booking_id as bookingid, bd.c_customer_name as customer,cm.c_model_name as assigned_car, bd.c_pickup_datetime as pickuptime, 
bd.c_pickup_location as pickuploc, bd.c_drop_location as droploc, bd.c_assign_status, dm.c_name as assigned_driver 
from dba.booking_details bd
left join dba.car_mst cm on cm.c_car_id = bd.c_car_id
left join dba.driver_mst dm on dm.c_driver_id =  bd.c_assigned_driver;";
                DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, query).Tables[0];
                return JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { error = ex.Message, stack = ex.StackTrace });
            }
        }
    }
}