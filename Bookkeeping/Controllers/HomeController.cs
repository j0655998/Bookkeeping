using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookkeeping.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        // 新增功能
        public ActionResult Add() {
            string sendData = string.Empty;

            HttpRequestBase request = HttpContext.Request;

            request.InputStream.Position = 0;
            using (MemoryStream ms = new MemoryStream(HttpContext.Request.BinaryRead(HttpContext.Request.ContentLength)))
            {
                sendData = new StreamReader(ms).ReadToEnd();
            }

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(sendData);

            // 初始化變數
            DataSet ds = new DataSet();

            // 宣告一個接收錯誤訊息的字串
            // 資料型態 string
            // 宣告一個名為errMsg的字串, 並初始化其值為「空」字串
            string errMsg = string.Empty;
            
            ConnectionStringSettings Connection1 = ConfigurationManager.ConnectionStrings["Connection1"];

            using (SqlConnection conn = new SqlConnection(Connection1.ConnectionString))
            using (SqlDataAdapter adp = new SqlDataAdapter("addBookkeeping", conn))
            {
                // 設定執行SP
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;

                // 加入參數
                adp.SelectCommand.Parameters.Add(new SqlParameter("@Date", data["Date"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@Bank", data["Bank"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@TransferIn", data["TransferIn"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@TransferOut", data["TransferOut"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@Summary", data["Summary"]) { Direction = ParameterDirection.Input });

                try
                {
                    // 連接SP
                    adp.Fill(ds);
                }
                catch (Exception e)
                {
                    ds = new DataSet();

                    errMsg = e.Message;
                }
            }

            ds = ds ?? new DataSet();

            return Content(JsonConvert.SerializeObject(new
            {
                data = new DataTable(),
                errMsg = errMsg
            }));
        }

        // 查詢功能
        [HttpPost]
        public ActionResult Select() {
            string sendData = string.Empty;

            HttpRequestBase request = HttpContext.Request;

            request.InputStream.Position = 0;
            using (MemoryStream ms = new MemoryStream(HttpContext.Request.BinaryRead(HttpContext.Request.ContentLength)))
            {
                sendData = new StreamReader(ms).ReadToEnd();
            }

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(sendData);

            // 初始化變數
            DataSet ds = new DataSet();

            // 宣告一個接收錯誤訊息的字串
            // 資料型態 string
            // 宣告一個名為errMsg的字串, 並初始化其值為「空」字串
            string errMsg = string.Empty;
            
            ConnectionStringSettings Connection1 = ConfigurationManager.ConnectionStrings["Connection1"];

            using (SqlConnection conn = new SqlConnection(Connection1.ConnectionString))
            using (SqlDataAdapter adp = new SqlDataAdapter("selectBookkeeping", conn))
            {
                // 設定執行SP
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;

                // 加入參數
                adp.SelectCommand.Parameters.Add(new SqlParameter("@Date1", data["Date1"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@Date2", data["Date2"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@Bank", data["Bank"]) { Direction = ParameterDirection.Input });

                try
                {
                    // 連接SP
                    adp.Fill(ds);
                }
                catch (Exception e)
                {
                    ds = new DataSet();

                    errMsg = e.Message;
                }

            }
            ds = ds ?? new DataSet();

            return Content(JsonConvert.SerializeObject(new
            {
                data = ds.Tables[0],
                errMsg = errMsg
            }));
        }

        // 修改
        public ActionResult Update() {
            string sendData = string.Empty;

            HttpRequestBase request = HttpContext.Request;

            request.InputStream.Position = 0;
            using (MemoryStream ms = new MemoryStream(HttpContext.Request.BinaryRead(HttpContext.Request.ContentLength)))
            {
                sendData = new StreamReader(ms).ReadToEnd();
            }

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(sendData);

            // 初始化變數
            DataSet ds = new DataSet();

            // 宣告一個接收錯誤訊息的字串
            // 資料型態 string
            // 宣告一個名為errMsg的字串, 並初始化其值為「空」字串
            string errMsg = string.Empty;
            
            ConnectionStringSettings Connection1 = ConfigurationManager.ConnectionStrings["Connection1"];

            using (SqlConnection conn = new SqlConnection(Connection1.ConnectionString))
            using (SqlDataAdapter adp = new SqlDataAdapter("updateBookkeeping", conn))
            {
                // 設定執行SP
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;

                // 加入參數
                adp.SelectCommand.Parameters.Add(new SqlParameter("@sysid", data["sysid"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@Date", data["Date"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@Bank", data["Bank"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@TransferIn", data["TransferIn"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@TransferOut", data["TransferOut"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@Summary", data["Summary"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@lastdate", data["lastdate"]) { Direction = ParameterDirection.Input });
                
                try
                {
                    // 連接SP
                    adp.Fill(ds);
                }
                catch (Exception e)
                {
                    ds = new DataSet();

                    errMsg = e.Message;
                }

            }
            ds = ds ?? new DataSet();

            return Content(JsonConvert.SerializeObject(new
            {
                data = ds.Tables[0],
                errMsg = errMsg
            }));
        }

        // 刪除功能
        public ActionResult Delete() {
            string sendData = string.Empty;

            HttpRequestBase request = HttpContext.Request;

            request.InputStream.Position = 0;
            using (MemoryStream ms = new MemoryStream(HttpContext.Request.BinaryRead(HttpContext.Request.ContentLength)))
            {
                sendData = new StreamReader(ms).ReadToEnd();
            }

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(sendData);

            // 初始化變數
            DataSet ds = new DataSet();

            // 宣告一個接收錯誤訊息的字串
            // 資料型態 string
            // 宣告一個名為errMsg的字串, 並初始化其值為「空」字串
            string errMsg = string.Empty;
            
            ConnectionStringSettings Connection1 = ConfigurationManager.ConnectionStrings["Connection1"];

            using (SqlConnection conn = new SqlConnection(Connection1.ConnectionString))
            using (SqlDataAdapter adp = new SqlDataAdapter("deleteBookkeeping", conn))
            {
                // 設定執行SP
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;

                // 加入參數
                adp.SelectCommand.Parameters.Add(new SqlParameter("@Date", data["Date"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@Bank", data["Bank"]) { Direction = ParameterDirection.Input });
                adp.SelectCommand.Parameters.Add(new SqlParameter("@lastdate", data["lastdate"]) { Direction = ParameterDirection.Input });

                try
                {
                    // 連接SP
                    adp.Fill(ds);
                }
                catch (Exception e)
                {
                    ds = new DataSet();

                    errMsg = e.Message;
                }

            }
            ds = ds ?? new DataSet();

            return Content(JsonConvert.SerializeObject(new
            {
                data = ds.Tables[0],
                errMsg = errMsg
            }));
        }
    }
}
