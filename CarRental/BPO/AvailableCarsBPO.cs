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
    public class AvailableCarsBPO 
    {
        public static string Conn = CommonConnection.Conn;
        public static string GetAvailableCars()
        {
            try
            {
                string query = @"SELECT c_car_id,c_model_name, c_type, n_year, n_rate_per_day, 'Available' as status,c_licence_plate, c_image_path FROM dba.car_mst;";
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