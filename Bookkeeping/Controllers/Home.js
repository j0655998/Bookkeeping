$(document).ready(function () {

    // 查詢功能
    $("#Q_Search").on("click", function (e) {

        let sendData = {
            Date1: $("#Q_date1").val(),
            Date2: $("#Q_date2").val(),
            Bank: $("#Q_bank").val()
        }

        // 清空資料
        $("#list tbody").empty();

        $.ajax({
            type: "POST",
            url: "Select",
            dataType: "json",
            data: JSON.stringify(sendData),
            success: function (result) {
                let data = result["data"];
                let errMsg = result["errMsg"];
                
                if (errMsg == "") {
                    // 顯示資料
                    for (let i = 0; i < data.length; i++) {
                        $("#list tbody").append(`
<tr>
    <td>
        ${data[i]["Bank"]}
    </td>
    <td>
        ${data[i]["Date"].split("T")[0]}
    </td>
    <td>
        ${data[i]["TransferIn"]}
    </td>
    <td>
        ${data[i]["TransferOut"]}
    </td>
    <td>
        ${data[i][""]}
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
    $("#Q_Add").on("click", function (e) {
        $("#Edit").show();
    });

    // 儲存按鈕
    $("#Q_Save").on("click", function (e) {
        let sendData = {
            Date: $("#E_date").val(),
            Bank: $("#E_bank").val(),
            TransferIn: $("#E_TransferIn").val(),
            TransferOut: $("#E_TransferOut").val(),
            Summary: $("#E_Summary").val()
        }

        // 清空資料
        $("#list tbody").empty();

        $.ajax({
            type: "POST",
            url: "Add",
            dataType: "json",
            data: JSON.stringify(sendData),
            success: function (result) {
                let data = result["data"];
                let errMsg = result["errMsg"]; // 錯誤訊息

                // if = 如果, errMsg = 錯誤訊息, "" = 空字串
                // 如果錯誤訊息為空
                if (errMsg == "") {
                    // 執行這裡
                    $("#Edit").hide();
                    $("#Q_Search").trigger("click");
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
});