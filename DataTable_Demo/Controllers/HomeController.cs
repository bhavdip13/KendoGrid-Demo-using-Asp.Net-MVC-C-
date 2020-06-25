using DataTable_Demo;
using KendoGrid_Demo.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoGrid_Demo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private PMSEntities context = new PMSEntities();
        private Common cmn = new Common();

        public ActionResult Index()
        {
            ViewBag.Menus = context.Menus.ToList();
            return View();
        }
        public JsonResult SaveAndUpdateProduct(int PID,string Name, string Description, float Price)
        {
            var result = new jsonMessage();
            try
            {
                //define the model
                Mst_Product _Mst_Product = new Mst_Product();
                _Mst_Product.PID = PID;
                _Mst_Product.Name = Name;
                _Mst_Product.Description = Description;
                _Mst_Product.Price = Price;


               //for insert recored..
                if (_Mst_Product.PID == 0)
                {
                    context.Mst_Product.Add(_Mst_Product);
                    result.Message = "your product has been saved success..";
                    result.Status = true;
                }
                else  //for update recored..
                {
                    context.Entry(_Mst_Product).State = EntityState.Modified;
                    result.Message = "your product has been updated successfully..";
                    result.Status = true;
                }
                context.SaveChanges();
                

            }
            catch (Exception ex)
            {
                ErrorLogers.ErrorLog(ex);
                result.Message = "We are unable to process your request at this time. Please try again later.";
                result.Status = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonNetResult GetProductsWithServerSideFilters(string Filters, string PageSize, string Page, string SortOn, string SortType)
        {
            ServiceResponse<object> objModel = new ServiceResponse<object>();
            string result = string.Empty;

            try
            {
                List<DemoFilterDescriptor> objFilters = new List<DemoFilterDescriptor>();
                if (!string.IsNullOrWhiteSpace(Filters))
                {
                    objFilters = JsonConvert.DeserializeObject<List<DemoFilterDescriptor>>(Filters);
                }

                DataSet ds = new DataSet();

                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {

                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_SELECT_PRODUCTS";

                    DataTable dtFilter = cmn.GetFilter(objFilters);
                    if (dtFilter.Rows.Count > 0)
                    {
                        TempData["PTotal"] = null;
                    }

                    var parameterPage = new SqlParameter("@PageNo", SqlDbType.Int);
                    parameterPage.Value = Page == null ? "1" : Page;
                    var parameterPageSize = new SqlParameter("@PageSize", SqlDbType.Int);
                    parameterPageSize.Value = PageSize == null ? "25" : PageSize;

                    if (!string.IsNullOrEmpty(SortType) && !string.IsNullOrEmpty(SortOn))
                    {
                        var parameterSortFiled = new SqlParameter("@OrderBy", SqlDbType.VarChar);
                        parameterSortFiled.Value = SortOn;

                        var parameterSortDir = new SqlParameter("@sortDir", SqlDbType.VarChar);
                        parameterSortDir.Value = SortType == "asc" ? "asc" : "desc";

                        cmd.Parameters.Add(parameterSortFiled);
                        cmd.Parameters.Add(parameterSortDir);
                    }
                    else
                    {

                        var parameterSortFiled = new SqlParameter("@OrderBy", SqlDbType.VarChar);
                        parameterSortFiled.Value = "";

                        var parameterSortDir = new SqlParameter("@sortDir", SqlDbType.VarChar);
                        parameterSortDir.Value = "";

                        cmd.Parameters.Add(parameterSortFiled);
                        cmd.Parameters.Add(parameterSortDir);

                    }
                    int Total = 0;
                    if (TempData["TempData_TotalRecords"] != null)
                    {
                        Total = Convert.ToInt32(TempData["TempData_TotalRecords"].ToString());
                    }
                    var parameterCount = new SqlParameter("@Count", SqlDbType.Int);
                    parameterCount.Value = Total;

                    var parameterFilterData = new SqlParameter("@TPV_Filter", SqlDbType.Structured);
                    parameterFilterData.Value = dtFilter;
                    parameterFilterData.TypeName = "dbo.TPV_Filter";

                    //if you need to pass other parameters
                    //var parameter = new SqlParameter("@Parameter", SqlDbType.VarChar);
                    //parameter.Value = parametervalue;
                    //cmd.Parameters.Add(parameter);

                    cmd.Parameters.Add(parameterFilterData);
                    cmd.Parameters.Add(parameterPage);
                    cmd.Parameters.Add(parameterPageSize);
                    cmd.Parameters.Add(parameterCount);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    connection.Close();
                }

                if (ds != null)
                {
                    objModel.Data = MapObject(ds);
                    objModel.OperationStatus = true;
                    objModel.OperationMessage = "Success";

                    TempData["TempData_TotalRecords"] = ds.Tables[1].Rows[0]["Total"];
                    TempData.Keep("TempData_TotalRecords");
                }
                else
                {

                    objModel.OperationStatus = false;
                    objModel.OperationMessage = "Error: No Data found";
                }
                return new JsonNetResult() { Data = objModel.Data, MaxJsonLength = 2147483644 };
            }
            catch (Exception ex)
            {
                objModel.OperationStatus = false;
                objModel.OperationMessage = "Error:" + ex.Message;
                return new JsonNetResult() { Data = objModel, MaxJsonLength = 2147483644 };

            }
           
        }
        public JsonResult GetProducts()
        {
            ServiceResponse<object> objModel = new ServiceResponse<object>();
            string result = string.Empty;

            try
            {
               
                DataSet ds = new DataSet();

                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {

                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_SELECT_PRODUCTS";

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    connection.Close();
                }

                if (ds != null)
                {
                    objModel.Data = MapObject(ds);
                    objModel.OperationStatus = true;
                    objModel.OperationMessage = "Success";

                    TempData["TempData_TotalRecords"] = ds.Tables[1].Rows[0]["Total"];
                    TempData.Keep("TempData_TotalRecords");
                }
                else
                {

                    objModel.OperationStatus = false;
                    objModel.OperationMessage = "Error: No Data found";
                }
                return Json(objModel.Data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                objModel.OperationStatus = false;
                objModel.OperationMessage = "Error:" + ex.Message;
                return Json(objModel.Data, JsonRequestBehavior.AllowGet);

            }

        }
        public object MapObject(DataSet ds)
        {
            var data = ds.Tables[0].AsEnumerable().Select(m => new
            {
                PID = m.Field<string>("PID"),
                CreationDate = m.Field<string>("CreationDate"),
                Name = m.Field<string>("Name"),
                Description = m.Field<string>("Description"),
                Price = m.Field<string>("Price")
            }).ToList();

            var Returndata = new
            {
                data = data,
                Total = Convert.ToInt32(ds.Tables[1].Rows[0]["Total"].ToString()),
                OperationStatus = true
            };

            return Returndata;
        }
        public JsonResult GetProduct()
        {

            List<Mst_Product> _list = new List<Mst_Product>();
           
            try
            {
                _list = context.Mst_Product.ToList();
                     var  result = from c in _list
                         select new[]
                         {
                          Convert.ToString( c.PID ),  // 0   
                          Convert.ToString( c.Name ),  // 1   
                          Convert.ToString( c.Description ),  // 2   
                          Convert.ToString( c.Price ),  // 3   
                                                   };

                     return Json(new
                     {
                        aaData= result
                     }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                ErrorLogers.ErrorLog(ex);
                return Json(new
                {
                    aaData = new List<string[]> { }
                }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public JsonResult DeleteProduct(int id)
        {
            var result = new jsonMessage();
            try
            {

                var product = new Mst_Product { PID = id };
                context.Entry(product).State = EntityState.Deleted;
                context.SaveChanges();

                
                result.Message = "your product has been deleted successfully..";
                result.Status = true;

            }
            catch (Exception ex)
            {
                ErrorLogers.ErrorLog(ex);
                result.Message = "We are unable to process your request at this time. Please try again later.";
                result.Status = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
    }
}
