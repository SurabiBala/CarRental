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
    public class InvoiceBPO 
    {
        public static string Conn = CommonConnection.Conn;
        public static string GetInvoiceDetails()
        {
            try
            {
                string query = @"
select im.c_invoice_id, im.d_invoice_date, im.c_customer_id, im.c_billing_address, im.n_total_amount,im.d_payment_due_date,
im.n_tax_amount, im.n_discount_amount, im.n_net_amount, im.c_booking_id, bm.c_customer_name, cm.c_email_id, cm.c_mobile, 'paid' as c_status,
bm.d_from_date, bm.d_to_date --rental period
from dba.invoice_mst im
left join dba.booking_details bm on bm.c_booking_id = im.c_booking_id
left join dba.customer_mst cm on cm.c_id = im.c_customer_id;";
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