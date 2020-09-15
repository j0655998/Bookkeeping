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
            type: 'POST',
            url: 'Select',
            dataType: 'json',
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

});