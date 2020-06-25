using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace KendoGrid_Demo.Utilities
{
    public class ServiceResponse<T>
    {
        public bool OperationStatus { get; set; }
        public string OperationMessage { get; set; }
        public int OperationCode { get; set; }
        public int ServiceCode { get; set; }
        public T Data { get; set; }
    }
}