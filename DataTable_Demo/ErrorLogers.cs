using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KendoGrid_Demo
{
    public class ErrorLogers
    {
        public static void CreateLogFiles()
        {

        }

        public static void ErrorLog(Exception ex, HttpContext httpContext)
        {
            var wrapper = new HttpContextWrapper(httpContext);

            string url = HttpContext.Current.Request.RawUrl;
            RouteData route = RouteTable.Routes.GetRouteData(wrapper);
            UrlHelper urlHelper = new UrlHelper(new RequestContext(wrapper, route));

            var routeValueDictionary = urlHelper.RequestContext.RouteData.Values;
            string controllerName = routeValueDictionary["controller"].ToString();
            string actionName = "";
            try
            {
                actionName = routeValueDictionary["action"].ToString();
            }
            catch (Exception)
            {
                actionName = "";
            }
            string sLogFormat;
            string sErrorTime;
            Random rnd = new Random();
            sLogFormat = rnd.Next(1, 1000).ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss");

            sErrorTime = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString();

            StreamWriter sw = File.CreateText(System.Web.HttpContext.Current.Request.MapPath("~/Logs/") + sLogFormat + ".txt");
            sw.WriteLine(controllerName + " " + actionName);
            sw.WriteLine("--------------------------------------------------------------------------------------------");
            sw.WriteLine("Time: " + sErrorTime);
            sw.WriteLine("--------------------------------------Message------------------------------------------------------");
            sw.WriteLine(ex.Message);
            sw.WriteLine("--------------------------------------StackTrace--------------------------------------------------");
            sw.WriteLine(ex.StackTrace);
            sw.WriteLine("--------------------------------------InnerException------------------------------------------------------");
            sw.WriteLine(ex.InnerException);
            sw.Flush();
            sw.Close();
            rnd = null;
        }
        public static void ErrorLog(Exception ex)
        {
            string sLogFormat;
            string sErrorTime;
            Random rnd = new Random();
            sLogFormat = rnd.Next(1, 1000).ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss");
            sErrorTime = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString();

            StreamWriter sw = File.CreateText(System.Web.HttpContext.Current.Request.MapPath("~/Logs/") + sLogFormat + ".txt");
            sw.WriteLine("Time: ----------------------------------------------------------------------------------------------");
            sw.WriteLine(sErrorTime);
            sw.WriteLine("");

            sw.WriteLine("Message: -------------------------------------------------------------------------------------------");
            sw.WriteLine(ex.Message);
            sw.WriteLine("");

            sw.WriteLine("StackTrace: ----------------------------------------------------------------------------------------");
            sw.WriteLine(ex.StackTrace);
            sw.WriteLine("");

            sw.WriteLine("InnerException: ------------------------------------------------------------------------------------");
            sw.WriteLine(ex.InnerException);
            sw.WriteLine("");

            sw.WriteLine("Source: --------------------------------------------------------------------------------------------");
            sw.WriteLine(ex.Source);
            sw.Flush();
            sw.Close();
            rnd = null;
        }   
    }
}