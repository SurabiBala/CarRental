using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Helpers;
using Newtonsoft.Json;
using static CarRental.Controllers.LoginController;

namespace CarRental.BPO
{
    public class LoginBPO 
    {
        public static string Conn = CommonConnection.Conn;
        public static string GetState()
        {
            try
            {
                string query = @"SELECT c_code, c_name from dba.state_mst;";
                DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, query).Tables[0];
                return JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { error = ex.Message, stack = ex.StackTrace });
            }
        }

        public static string GetDistrict()
        {
            try
            {
                string query = @"SELECT c_code, c_name from dba.district_mst;";
                DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, query).Tables[0];
                return JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { error = ex.Message, stack = ex.StackTrace });
            }
        }
        public static object SaveCustomer(CustomerDTO dto)
        {
            try
            {
                string name = $"{dto.FirstName} {dto.MiddleName} {dto.LastName}".Trim();

                // Get latest ID and generate next c_id
                string getIdQuery = "SELECT ISNULL(MAX(RIGHT(c_id, 6)), '000000') FROM dba.customer_mst";
                string maxId = SqlHelper.ExecuteScalar(Conn, CommandType.Text, getIdQuery).ToString();

                int nextIdNum = Convert.ToInt32(maxId) + 1;
                string newId = "C" + nextIdNum.ToString("D6"); // C000001 format

                string insertQuery = $@"
    INSERT INTO dba.customer_mst 
    (c_id, c_name, c_email_id, c_mobile, d_dob, c_area, c_district_code, c_state_code, c_country_code, c_pincode)
    VALUES ('{newId}', '{name}', '{dto.Email}', '{dto.Mobile}', '{dto.DOB}', 
            '{dto.Area}', '{dto.DistrictCode}', '{dto.StateCode}', '{dto.CountryCode}', '{dto.Pincode}')";
                SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, insertQuery);
                
                return new { success = true, message = "Customer registered successfully." };
            }
            catch (Exception ex)
            {
                return new { success = false, message = ex.Message };
            }
        }

    }
}