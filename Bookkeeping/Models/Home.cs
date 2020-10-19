using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bookkeeping.Models
{
    public class Home
    {
        protected ConnectionStringSettings Connection1 { get; set; } = ConfigurationManager.ConnectionStrings["Connection1"];

        // 新增功能
        public string Add(Dictionary<string, dynamic> data)
        {
            // 初始化變 
            DataSet ds = new DataSet();

            // 宣告一個接收錯誤訊息的字串
            // 資料型態 string
            // 宣告一個名為errMsg的字串, 並初始化其值為「空」字串 
            string errMsg = string.Empty;

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

            return errMsg;
        }

        // 查詢功能
        public string Select(Dictionary<string, dynamic> data, ref DataTable dt)
        {
            // 宣告一個接收錯誤訊息的字串
            // 資料型態 string
            // 宣告一個名為errMsg的字串, 並初始化其值為「空」字串
            string errMsg = string.Empty;

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
                    adp.Fill(dt);
                }
                catch (Exception e)
                {
                    dt = new DataTable();
                    errMsg = e.Message;
                }

            }
            dt = dt ?? new DataTable();

            return errMsg;
        }

        // 修改功能
        public string Update(Dictionary<string, dynamic> data)
        {
            // 初始化變數
            DataSet ds = new DataSet();

            // 宣告一個接收錯誤訊息的字串
            // 資料型態 string
            // 宣告一個名為errMsg的字串, 並初始化其值為「空」字串
            string errMsg = string.Empty;

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

            return errMsg;
        }

        // 刪除功能
        public string Delete(Dictionary<string, dynamic> data)
        {
            // 初始化變數
            DataSet ds = new DataSet();

            // 宣告一個接收錯誤訊息的字串
            // 資料型態 string
            // 宣告一個名為errMsg的字串, 並初始化其值為「空」字串
            string errMsg = string.Empty;

            using (SqlConnection conn = new SqlConnection(Connection1.ConnectionString))
            using (SqlDataAdapter adp = new SqlDataAdapter("deleteBookkeeping", conn))
            {
                // 設定執行SP
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;

                // 加入參數
                adp.SelectCommand.Parameters.Add(new SqlParameter("@sysid", data["sysid"]) { Direction = ParameterDirection.Input });
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

            return errMsg;
        }

        // 結帳功能
        public string Close(Dictionary<string, dynamic> data)
        {
            // 初始化變數
            DataSet ds = new DataSet();

            // 宣告一個接收錯誤訊息的字串
            // 資料型態 string
            // 宣告一個名為errMsg的字串, 並初始化其值為「空」字串
            string errMsg = string.Empty;

            using (SqlConnection conn = new SqlConnection(Connection1.ConnectionString))
            using (SqlDataAdapter adp = new SqlDataAdapter("closeBookkeeping", conn))
            {
                // 設定執行SP
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;

                // 加入參數
                adp.SelectCommand.Parameters.Add(new SqlParameter("@CloseDate", data["CloseDate"]) { Direction = ParameterDirection.Input });
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

            return errMsg;
        }

        // 取消結帳功能
        public string CancelClose(Dictionary<string, dynamic> data)
        {
            // 初始化變數
            DataSet ds = new DataSet();

            // 宣告一個接收錯誤訊息的字串
            // 資料型態 string
            // 宣告一個名為errMsg的字串, 並初始化其值為「空」字串
            string errMsg = string.Empty;

            using (SqlConnection conn = new SqlConnection(Connection1.ConnectionString))
            using (SqlDataAdapter adp = new SqlDataAdapter("cancelClose", conn))
            {
                // 設定執行SP
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;

                // 加入參數
                adp.SelectCommand.Parameters.Add(new SqlParameter("@CloseDate", data["CloseDate"]) { Direction = ParameterDirection.Input });
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

            return errMsg;
        }
    }
}