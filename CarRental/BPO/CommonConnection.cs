using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.BPO
{
    public class CommonConnection
    {
        public static string Conn = "Driver={SQL Anywhere 17};UID=dba;PWD=c1;ServerName=CarRental;DBN=CarRental;";
        public static string ConnSlave = "Driver={SQL Anywhere 17};UID=dba;PWD=c1;ServerName=CarRental;DBN=CarRental;";

    }
}