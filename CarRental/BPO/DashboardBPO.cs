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
    public class DashboardBPO 
    {
        public static string Conn = CommonConnection.Conn;
        public static string GetCarStatusCount()
        {
            try
            {
                string query = @"SELECT 
    CASE 
        WHEN n_status = 0 THEN 'Booked'
        WHEN n_status = 1 THEN 'Available'
        WHEN n_status = 2 THEN 'Maintenance'
        ELSE 'Unknown'
    END AS status_description,
    COUNT(*) AS total_count
FROM dba.car_mst
GROUP BY 
    CASE 
        WHEN n_status = 0 THEN 'Booked'
        WHEN n_status = 1 THEN 'Available'
        WHEN n_status = 2 THEN 'Maintenance'
        ELSE 'Unknown'
    END;
;";
                DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, query).Tables[0];
                return JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { error = ex.Message, stack = ex.StackTrace });
            }
        }

        public static string GetDriverStatusCount()
        {
            try
            {
                string query = @"SELECT 
    CASE 
        WHEN n_status = 0 THEN 'Assigned'
        WHEN n_status = 1 THEN 'Available'
        WHEN n_status = 2 THEN 'Off Duty'
        ELSE 'Unknown'
    END AS status_description,
    COUNT(*) AS total_count
FROM dba.driver_mst
GROUP BY 
    CASE 
        WHEN n_status = 0 THEN 'Assigned'
        WHEN n_status = 1 THEN 'Available'
        WHEN n_status = 2 THEN 'Off Duty'
        ELSE 'Unknown'
    END;
;";
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