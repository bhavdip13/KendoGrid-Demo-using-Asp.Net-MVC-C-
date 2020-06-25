using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KendoGrid_Demo.Utilities
{
    public class Common : System.Web.UI.Page
    {
        List<string> LogicalOperator = new List<string>();
        List<string> finalLogicalOperator = new List<string>();

        bool isCheck = true;
        int cnt = 0;
        public DataTable GetFilter(List<DemoFilterDescriptor> allFilterDescriptors)
        {
            var res = new List<FilterDescriptor>();

            if (allFilterDescriptors.Count > 0)
                RecurseFilterDescriptors((IEnumerable<IFilterDescriptor>)allFilterDescriptors, res);


            DataTable Filter = new DataTable();
            Filter.Columns.Add("Logic", typeof(string));
            Filter.Columns.Add("Field", typeof(string));
            Filter.Columns.Add("Operator", typeof(string));
            Filter.Columns.Add("Value", typeof(string));
            Filter.Columns.Add("Field1", typeof(string));
            Filter.Columns.Add("Operator1", typeof(string));
            Filter.Columns.Add("Value1", typeof(string));

            int dupRecord = LogicalOperator.Count - cnt;
            int count = 0;
            foreach (var item in LogicalOperator)
            {
                count++;
                if (dupRecord < count)
                {
                    finalLogicalOperator.Add(item);
                }
            }
            var strLogicOpr = finalLogicalOperator;

            int j = 0;
            DataRow dr = Filter.NewRow();
            for (int k = 0; k < res.Count; k++)
            {
                if (k == 0)
                {
                    dr = Filter.NewRow();
                    dr["Field"] = res[k].Member;
                    dr["Operator"] = operators[res[k].Operator.ToString()];
                    dr["Value"] = res[k].Value;

                }
                if (k > 0 && res[k].Member != res[k - 1].Member)
                {
                    dr = Filter.NewRow();
                    dr["Field"] = res[k].Member;
                    dr["Operator"] = operators[res[k].Operator.ToString()];
                    dr["Value"] = res[k].Value;

                }
                if (k > 0 && res[k].Member == res[k - 1].Member)
                {

                    dr["Logic"] = strLogicOpr[j];
                    dr["Field1"] = res[k].Member;
                    dr["Operator1"] = operators[res[k].Operator.ToString()];
                    dr["Value1"] = res[k].Value;
                    Filter.Rows.Add(dr);
                    j = j + 1;
                    continue;
                }
                if (k != (res.Count - 1) && res[k].Member != res[k + 1].Member)
                {
                    Filter.Rows.Add(dr);
                }
                if (k == (res.Count - 1) && res.Count == 1)
                {
                    Filter.Rows.Add(dr);

                }
                else if (k == (res.Count - 1) && res[k].Member != res[k - 1].Member)
                {
                    Filter.Rows.Add(dr);

                }


            }
            return Filter;

        }
        public void RecurseFilterDescriptors(IEnumerable<IFilterDescriptor> requestFilters, List<FilterDescriptor> allFilterDescriptors)
        {
            foreach (var filterDescriptor in requestFilters)
            {
                if (filterDescriptor is FilterDescriptor)
                {
                    isCheck = true;
                    allFilterDescriptors.Add((FilterDescriptor)filterDescriptor);
                }
                else if (filterDescriptor is CompositeFilterDescriptor)
                {
                    if (isCheck)
                    {
                        cnt++;
                        isCheck = false;
                    }
                    LogicalOperator.Add(((CompositeFilterDescriptor)filterDescriptor).LogicalOperator.ToString());
                    RecurseFilterDescriptors(((CompositeFilterDescriptor)filterDescriptor).FilterDescriptors, allFilterDescriptors);
                }
            }
        }
        public readonly IDictionary<string, string> operators = new Dictionary<string, string>
        {

            //Number Condition
            {"IsEqualTo", "="},
            {"IsNotEqualTo", "!="},
            {"IsLessThan", "<"},
            {"IsLessThanOrEqualTo", "<="},
            {"IsGreaterThan", ">"},
            {"IsGreaterThanOrEqualTo", ">="},

           
            //Date Condition
            {"IsAfterOrEqualTo", ">="},
            {"IsAfter", ">"},
            {"IsBeforeOrEqualTo", "<="},
            {"IsBefore", "<"},
         

            //string Condition
            {"StartsWith", "StartsWith"},
            {"EndsWith", "EndsWith"},
            {"Contains", "Contains"},
            {"IsContainedIn", "IsContainedIn"},
            {"DoesNotContain", "DoesNotContain"},
            {"IsNull", "IsNull"},
            {"IsNotNull", "IsNotNull"},
            {"IsEmpty", "IsEmpty"},
            {"IsNotEmpty", "IsNotEmpty"}
        };
    }
}