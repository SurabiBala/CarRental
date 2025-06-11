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
    public class ManageDriversBPO
    {
        public static string Conn = CommonConnection.Conn;
        public static string GetDriversList()
        {
            try
            {
                string query = @"SELECT c_driver_id, c_name, c_licence_no, c_mobile, n_experiance, n_rating, n_status, c_image_path from dba.driver_mst;";
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