﻿$(document).ready(function () {

    // 設定一個儲存資料的變數
    let l_data = [];

    // 查詢功能
    $("#S_Search").on("click", function (e) {
        //let sendData = {
        //    Date1: $("#S_date1").val(),
        //    Date2: $("#S_date2").val(),
        //    Bank: $("#S_bank").val()
        //}

        let sendData = {};
        $("#Search").find("[param]").each(function () {
            let element = $(this);
            sendData[element.attr("param")] = element.val();
        });

        if (sendData.Date1 == "" || sendData.Date2 == "") {
            alert("請輸入日期");
            return;
        }

        // 清空資料
        $("#Edit").hide();
        $("#Detailed tbody").empty();

        $.ajax({
            type: "POST",
            url: "Select",
            dataType: "json",
            data: JSON.stringify(sendData),
            success: function (result) { //成功之後要執行
                let data = result["data"];
                let errMsg = result["errMsg"];

                if (errMsg == "") {
                    // 寫入資料到變數
                    l_data = data;
                    // 顯示資料
                    for (let i = 0; i < data.length; i++) {
                        // 是否允許修改及刪除
                        let allowedit = data[i]["allowedit"] == "1" ? "" : " disabled"
        //<button name="editData" class="btn btn-primary" rowid="${data[i]["sysid"]}"${allowedit}>修改</button>
        //<button name="deleteData" class="btn btn-danger" rowid="${data[i]["sysid"]}"${allowedit}>刪除</button>
                        $("#D_table tbody").append(`
<tr rowid="${data[i]["sysid"]}">
    <td>
        <button name="editData" class="btn btn-dark" rowid="${data[i]["sysid"]}"${allowedit}>修改</button>
        <button name="deleteData" class="btn btn-dark" rowid="${data[i]["sysid"]}"${allowedit}>刪除</button>
    </td>
    <td>
        ${data[i]["Bank"]}
    </td>
    <td>
        ${data[i]["Date"]}
    </td>
    <td>
        ${data[i]["TransferIn"]}
    </td>
    <td>
        ${data[i]["TransferOut"]}
    </td>
    <td>
        ${data[i]["Balance"]}
    </td>
    <td>
        ${data[i]["Summary"]}
    </td>
</tr>
`);
                    }

                } else {
                    // 提示錯誤訊息
                    alert(errMsg);
                }
            },
            error: function (error) {
                alert(error.responseText);

            }
        });
    });

    // 新增按鈕
    $("#S_Add").on("click", function (e) {
        $("#Edit [param]").val("");
        $("#E_date").val(dateToString(new Date()));
        $("#E_bank")[0].selectedIndex = 0;
        $("#Close").hide();
        $("#Edit").removeAttr("rowid").show();
        $("#E_Save").focus();
    });

    // 儲存按鈕
    $("#E_Save").on("click", function (e) {

        if ($("#Edit [param][required]").toArray().some(e => $(e).val() == "")) {
            alert("請填寫必填欄位。");
            return;
        };

        let rowid = $("#Edit").attr("rowid");

        let data = l_data.filter(x => x["sysid"] == rowid)[0];

        let editData = {};
        $("#Edit").find("[param]").each(function () {
            let element = $(this);
            editData[element.attr("param")] = element.val();
        });

        let sendData = $.extend(data, editData);

        $.ajax({
            type: "POST",
            url: rowid ? "Update" : "Add",
            dataType: "json",
            data: JSON.stringify(sendData),
            success: function (result) {
                let data = result["data"];
                let errMsg = result["errMsg"]; // 錯誤訊息

                // if = 如果, errMsg = 錯誤訊息, "" = 空字串
                // 如果錯誤訊息為空
                if (errMsg == "") {
                    // 執行這裡

                    // 如果查詢日期為空,塞入E區日期到查詢區域日期
                    if ($("#S_date1").val() == "") {
                        $("#S_date1").val($("#E_date").val());
                    }
                    if ($("#S_date2").val() == "") {
                        $("#S_date2").val($("#E_date").val());
                    }

                    $("#Edit").removeAttr("rowid").hide();

                    $("#S_Search").trigger("click");
                } else {
                    // else = 否則
                    // 提示錯誤訊息
                    alert(errMsg);
                }
            },
            error: function (error) {
                alert(error.responseText);
            }
        });
    });

    // 修改按鈕
    $("#Detailed tbody").on("click", `button[name="editData"]`, function (e) {
        // 取得元件
        let element = $(this);
        // 取得系統id
        let sysid = element.attr("rowid");
        // 根據系統ID取得對應的資料
        let data = l_data.filter(x => x["sysid"] == sysid)[0];
        // 把資料塞到編輯區域
        Object.keys(data).forEach(function (key) {
            let element = $(`#Edit [param="${key}"]`);
            if (element.length > 0) {
                element.val(data[key]);
            }
        });
        // 顯示編輯區域
        $("#Close").hide();
        $("#Edit").attr("rowid", sysid).show();
        $("#E_Save").focus();
    });

    // 刪除
    $("#Detailed tbody").on("click", `button[name="deleteData"]`, function (e) {
        // 取得元件
        let element = $(this);
        // 取得系統id
        let sysid = element.attr("rowid");
        // 根據系統ID取得對應的資料
        let data = l_data.filter(x => x["sysid"] == sysid)[0];

        $.ajax({
            type: "POST",
            url: "Delete",
            dataType: "json",
            data: JSON.stringify(data),
            success: function (result) {
                let data = result["data"];
                let errMsg = result["errMsg"]; // 錯誤訊息

                // if = 如果, errMsg = 錯誤訊息, "" = 空字串
                // 如果錯誤訊息為空
                if (errMsg == "") {
                    $("#S_Search").trigger("click");
                } else {
                    // else = 否則
                    // 提示錯誤訊息
                    alert(errMsg);
                }
            },
            error: function (error) {
                alert(error.responseText);
            }
        });
    });

    // 開啟結帳區域
    $("#S_Close").on("click", function (e) {
        $("#Close [param]").val("");
        $("#C_date").val(dateToString(new Date()));
        $("#C_bank")[0].selectedIndex = 0;
        $("#Edit").removeAttr("rowid").hide();
        $("#Close").show();
        $("#C_Save").focus();
    });

    // 結帳儲存
    $("#C_Save").on("click", function (e) {
        if ($("#Close [param][required]").toArray().some(e => $(e).val() == "")) {
            alert("請填寫必填欄位。");
            return;
        };

        let rowid = $("#Close").attr("rowid");

        let data = l_data.filter(x => x["sysid"] == rowid)[0];

        let editData = {};
        $("#Close").find("[param]").each(function () {
            let element = $(this);
            editData[element.attr("param")] = element.val();
        });

        let sendData = $.extend(data, editData);

        $.ajax({
            type: "POST",
            url: "Close",
            dataType: "json",
            data: JSON.stringify(sendData),
            success: function (result) {
                let data = result["data"];
                let errMsg = result["errMsg"]; // 錯誤訊息

                // if = 如果, errMsg = 錯誤訊息, "" = 空字串
                // 如果錯誤訊息為空
                if (errMsg == "") {
                    // 執行這裡

                    // 如果查詢日期為空,塞入日期預設值到查詢區域日期
                    let setDate = new Date();
                    setDate.setDate(1);
                    if ($("#S_date1").val() == "") {
                        $("#S_date1").val(dateToString(setDate));
                    }
                    setDate.setMonth(setDate.getMonth() + 1);
                    setDate.setDate(setDate.getDate() - 1);
                    if ($("#S_date2").val() == "") {
                        $("#S_date2").val(dateToString(setDate));
                    }

                    $("#Close").hide();

                    $("#S_Search").trigger("click");
                } else {
                    // else = 否則
                    // 提示錯誤訊息
                    alert(errMsg);
                }
            },
            error: function (error) {
                alert(error.responseText);
            }
        });
    });

    // 取消結帳
    $("#C_Cancel").on("click", function (e) {
        if ($("#Close [param][required]").toArray().some(e => $(e).val() == "")) {
            alert("請填寫必填欄位。");
            return;
        };

        let rowid = $("#Close").attr("rowid");

        let data = l_data.filter(x => x["sysid"] == rowid)[0];

        let editData = {};
        $("#Close").find("[param]").each(function () {
            let element = $(this);
            editData[element.attr("param")] = element.val();
        });

        let sendData = $.extend(data, editData);

        $.ajax({
            type: "POST",
            url: "CancelClose",
            dataType: "json",
            data: JSON.stringify(sendData),
            success: function (result) {
                let data = result["data"];
                let errMsg = result["errMsg"]; // 錯誤訊息

                // if = 如果, errMsg = 錯誤訊息, "" = 空字串
                // 如果錯誤訊息為空
                if (errMsg == "") {
                    // 執行這裡

                    // 如果查詢日期為空,塞入日期預設值到查詢區域日期
                    let setDate = new Date();
                    setDate.setDate(1);
                    if ($("#S_date1").val() == "") {
                        $("#S_date1").val(dateToString(setDate));
                    }
                    setDate.setMonth(setDate.getMonth() + 1);
                    setDate.setDate(setDate.getDate() - 1);
                    if ($("#S_date2").val() == "") {
                        $("#S_date2").val(dateToString(setDate));
                    }

                    $("#Close").hide();

                    $("#S_Search").trigger("click");
                } else {
                    // else = 否則
                    // 提示錯誤訊息
                    alert(errMsg);
                }
            },
            error: function (error) {
                alert(error.responseText);
            }
        });
    });

    // 設定日期預設值
    let _date = new Date();
    _date.setDate(1);
    $("#S_date1").val(dateToString(_date));
    _date.setMonth(_date.getMonth() + 1);
    _date.setDate(_date.getDate() - 1);
    $("#S_date2").val(dateToString(_date));

    // 必填欄位加上*號
    $("[required]").parent().addClass("requiredStar");

    // 進入後自動查詢
    $("#S_Search").trigger("click");
});

function dateToString(date) {
    return date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2) + "-" + ("0" + date.getDate()).slice(-2);
}