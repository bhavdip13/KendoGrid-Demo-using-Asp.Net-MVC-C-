using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoGrid_Demo.Utilities
{
    public class DemoFilterDescriptor : FilterDescriptor
    {
        private Dictionary<string, string> _fieldMappings = new Dictionary<string, string>
        {
            { "ORDERTYPEDESC","ORDERTYPE" }
        };

        private string _field;
    }
}