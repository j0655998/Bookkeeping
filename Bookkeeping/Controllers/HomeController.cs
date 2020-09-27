using Bookkeeping.Models;
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
        public Home GetHome = new Home();

        public ActionResult Index()
        {
            return View();
        }

        // 新增功能
        public ActionResult Add()
        {
            string sendData = string.Empty;

            HttpRequestBase request = HttpContext.Request;//傳回HTTP post內容

            request.InputStream.Position = 0; //傳入HTTP內容 資料流位子=0  從0開始
            using (MemoryStream ms = new MemoryStream(HttpContext.Request.BinaryRead(HttpContext.Request.ContentLength)))//讀到最後
            {
                sendData = new StreamReader(ms).ReadToEnd(); //取出來 字串
            }

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(sendData);

            string errMsg = GetHome.Add(data); //呼叫models 取得回傳結果

            return Content(JsonConvert.SerializeObject(new
            {
                data = new DataTable(),
                errMsg = errMsg
            }));
        }

        // 查詢功能
        public ActionResult Select()
        {
            string sendData = string.Empty;

            HttpRequestBase request = HttpContext.Request;

            request.InputStream.Position = 0;
            using (MemoryStream ms = new MemoryStream(HttpContext.Request.BinaryRead(HttpContext.Request.ContentLength)))
            {
                sendData = new StreamReader(ms).ReadToEnd();
            }

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(sendData);

            DataTable dt = new DataTable();

            string errMsg = GetHome.Select(data, ref dt);

            return Content(JsonConvert.SerializeObject(new
            {
                data = dt,
                errMsg = errMsg
            }));
        }

        // 修改
        public ActionResult Update()
        {
            string sendData = string.Empty;

            HttpRequestBase request = HttpContext.Request;

            request.InputStream.Position = 0;
            using (MemoryStream ms = new MemoryStream(HttpContext.Request.BinaryRead(HttpContext.Request.ContentLength)))
            {
                sendData = new StreamReader(ms).ReadToEnd();
            }

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(sendData);

            string errMsg = GetHome.Update(data);

            return Content(JsonConvert.SerializeObject(new
            {
                data = new DataTable(),
                errMsg = errMsg
            }));
        }

        // 刪除功能
        public ActionResult Delete()
        {
            string sendData = string.Empty;

            HttpRequestBase request = HttpContext.Request;

            request.InputStream.Position = 0;
            using (MemoryStream ms = new MemoryStream(HttpContext.Request.BinaryRead(HttpContext.Request.ContentLength)))
            {
                sendData = new StreamReader(ms).ReadToEnd();
            }

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(sendData);

            string errMsg = GetHome.Delete(data);

            return Content(JsonConvert.SerializeObject(new
            {
                data = new DataTable(),
                errMsg = errMsg
            }));
        }

        // 結帳功能
        public ActionResult Close()
        {
            string sendData = string.Empty;

            HttpRequestBase request = HttpContext.Request;

            request.InputStream.Position = 0;
            using (MemoryStream ms = new MemoryStream(HttpContext.Request.BinaryRead(HttpContext.Request.ContentLength)))
            {
                sendData = new StreamReader(ms).ReadToEnd();
            }

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(sendData);

            string errMsg = GetHome.Close(data);

            return Content(JsonConvert.SerializeObject(new
            {
                data = new DataTable(),
                errMsg = errMsg
            }));
        }
    }
}
