﻿using Bookkeeping.Models;
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

            string errMsg = GetHome.Add(data);

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

            DataTable dt = new DataTable();

            string errMsg = GetHome.Select(data, ref dt);

            return Content(JsonConvert.SerializeObject(new
            {
                data = dt,
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

            string errMsg = GetHome.Update(data);

            return Content(JsonConvert.SerializeObject(new
            {
                data = new DataTable(),
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

            string errMsg = GetHome.Delete(data);

            return Content(JsonConvert.SerializeObject(new
            {
                data = new DataTable(),
                errMsg = errMsg
            }));
        }
    }
}
