using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.BPO;
using CarRental.Helpers;

namespace CarRental.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice Page
        public ActionResult Invoice()
        {
            return View();
        }

        // Return JSON string of all invoice data
        public string GetInvoiceDetails()
        {
            try
            {
                var result = InvoiceBPO.GetInvoiceDetails();  // returns JSON
                return result;
            }
            catch
            {
                return null;
            }
        }

        // View PDF in iframe (preview in modal)
        public ActionResult ViewInvoice(string invoiceId)
        {
            string query = $@"
            SELECT im.c_invoice_id, im.d_invoice_date, im.c_customer_id, im.c_billing_address, im.n_total_amount,
                   im.n_tax_amount, im.n_discount_amount, im.n_net_amount, im.c_booking_id,
                   bm.c_customer_name, cm.c_email_id, cm.c_mobile,
                   bm.d_from_date, bm.d_to_date
            FROM dba.invoice_mst im
            LEFT JOIN dba.booking_details bm ON bm.c_booking_id = im.c_booking_id
            LEFT JOIN dba.customer_mst cm ON cm.c_id = im.c_customer_id
            WHERE im.c_invoice_id = '{invoiceId}'";

            DataTable dt = SqlHelper.ExecuteDataset(InvoiceBPO.Conn, CommandType.Text, query).Tables[0];

            if (dt.Rows.Count > 0)
            {
                byte[] pdfBytes = InvoicePdfGenerator.GenerateInvoicePDF(dt.Rows[0]);
                return File(pdfBytes, "application/pdf"); // No filename = open in browser
            }

            return HttpNotFound();
        }

        // Download Invoice PDF
        public ActionResult DownloadInvoice(string invoiceId)
        {
            string query = $@"
            SELECT im.c_invoice_id, im.d_invoice_date, im.c_customer_id, im.c_billing_address, im.n_total_amount,
                   im.n_tax_amount, im.n_discount_amount, im.n_net_amount, im.c_booking_id,
                   bm.c_customer_name, cm.c_email_id, cm.c_mobile,
                   bm.d_from_date, bm.d_to_date
            FROM dba.invoice_mst im
            LEFT JOIN dba.booking_details bm ON bm.c_booking_id = im.c_booking_id
            LEFT JOIN dba.customer_mst cm ON cm.c_id = im.c_customer_id
            WHERE im.c_invoice_id = '{invoiceId}'";

            DataTable dt = SqlHelper.ExecuteDataset(InvoiceBPO.Conn, CommandType.Text, query).Tables[0];

            if (dt.Rows.Count > 0)
            {
                byte[] pdfBytes = InvoicePdfGenerator.GenerateInvoicePDF(dt.Rows[0]);
                return File(pdfBytes, "application/pdf", $"Invoice_{invoiceId}.pdf"); // Forces download
            }

            return HttpNotFound();
        }
    }

}