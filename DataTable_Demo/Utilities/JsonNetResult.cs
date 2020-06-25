using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoGrid_Demo.Utilities
{
    public class JsonNetResult : System.Web.Mvc.JsonResult
    {
        public object Data { get; set; }
        public JsonNetResult()
        {
        }
    }
}