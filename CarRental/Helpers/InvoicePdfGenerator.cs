using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using QuestPDF.Elements;
using System;
using System.Data;
using System.Globalization;

public class InvoicePdfGenerator
{
    public static byte[] GenerateInvoicePDF(DataRow invoice)
    {
        var companyAddress = "Flyway Rent A Car LLP\nKochi, Muvattupuzha,\nKerala 682030, India\nPhone: +91-XXXXXXXXXX\nEmail: flywayrentacar@gmail.com";

        decimal netAmount = Convert.ToDecimal(invoice["n_net_amount"]);
        string amountInWords = NumberToWords((long)netAmount).ToUpper() + " RUPEES ONLY";

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text("INVOICE")
                    .FontSize(20)
                    .Bold()
                    .AlignCenter();

                page.Content().Column(col =>
                {
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text(text =>
                        {
                            text.Span("From:\n").SemiBold();
                            text.Span(companyAddress);
                        });

                        row.RelativeItem().Text(text =>
                        {
                            text.Span("To:\n").SemiBold();
                            text.Span($"{invoice["c_customer_name"]}\n{invoice["c_billing_address"]}");
                        });
                    });

                    col.Item().PaddingTop(10).Text(text =>
                    {
                        text.Span("Invoice ID: ").SemiBold();
                        text.Span($"{invoice["c_invoice_id"]}");
                    });

                    col.Item().Text(text =>
                    {
                        text.Span("Invoice Date: ").SemiBold();
                        text.Span($"{Convert.ToDateTime(invoice["d_invoice_date"]).ToString("dd-MM-yyyy")}");
                    });

                    col.Item().Text(text =>
                    {
                        text.Span("Booking ID: ").SemiBold();
                        text.Span($"{invoice["c_booking_id"]}");
                    });

                    col.Item().Text(text =>
                    {
                        text.Span("Rental Period: ").SemiBold();
                        text.Span($"{invoice["d_from_date"]} to {invoice["d_to_date"]}");
                    });

                    col.Item().PaddingTop(15).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1);
                            columns.ConstantColumn(120);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderCellStyle).Text("Description");
                            header.Cell().Element(HeaderCellStyle).Text("Amount (₹)");

                            IContainer HeaderCellStyle(IContainer element) =>
                                element.Border(1).Background(Colors.Grey.Lighten2).Padding(5).DefaultTextStyle(x => x.SemiBold());
                        });

                        void AddRow(string desc, object value, bool isBold = false)
                        {
                            table.Cell().Element(CellStyle).Text(text =>
                            {
                                if (isBold) text.Span(desc).Bold();
                                else text.Span(desc);
                            });

                            table.Cell().Element(CellStyle).Text(text =>
                            {
                                if (isBold) text.Span($"{value}").Bold();
                                else text.Span($"{value}");
                            });
                        }

                        AddRow("Total Amount", invoice["n_total_amount"]);
                        AddRow("Tax", invoice["n_tax_amount"]);
                        AddRow("Discount", invoice["n_discount_amount"]);
                        AddRow("Net Amount", invoice["n_net_amount"], isBold: true);

                        IContainer CellStyle(IContainer element) => element.Border(1).Padding(5);
                    });

                    // Amount in words
                    col.Item().PaddingTop(10).Text(text =>
                    {
                        text.Span("Amount in Words: ").SemiBold();
                        text.Span(amountInWords);
                    });

                    // Terms and conditions
                    col.Item().PaddingTop(15).Text("Terms and Conditions").Bold().FontSize(14);
                    col.Item().Text("- This invoice is system-generated and does not require a signature.");
                    col.Item().Text("- Please make the payment within 7 days from the invoice date.");
                    col.Item().Text("- All rentals are subject to the company’s terms of service.");

                    // Signatures
                    col.Item().PaddingTop(30).Row(row =>
                    {
                        row.RelativeItem().AlignLeft().Text("Customer Signature").Underline();
                        row.RelativeItem().AlignRight().Text("Authorized Signature").Underline();
                    });
                });

                page.Footer().AlignCenter().Text("Thank you for choosing our service!").Italic();
            });
        }).GeneratePdf();
    }

    // Convert number to words (simple version)
    private static string NumberToWords(long number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string[] unitsMap = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten",
                              "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen",
                              "seventeen", "eighteen", "nineteen" };

        string[] tensMap = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty",
                             "seventy", "eighty", "ninety" };

        string words = "";

        if ((number / 100000) > 0)
        {
            words += NumberToWords(number / 100000) + " lakh ";
            number %= 100000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "") words += "and ";
            if (number < 20) words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0) words += "-" + unitsMap[number % 10];
            }
        }

        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words.Trim());
    }
}
