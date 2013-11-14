function ShowTime(isShow) {
    if (isShow == true) {
        $("#optime").show();
        $("#refreshtime").hide();

    }
    else {
        $("#optime").hide();
        $("#refreshtime").show();
        time = $("#seltime").val();
    }
}

function ShowChart() {
    var top = $('#pageSize').val();
    $.ajax({
        url: NAP.resolveClientUrl("DatabaseReport/UserOnlineChart"),
        data: { size: top },
        type: 'POST',
        dataType: 'html',
        cache: false,
        beforeSend: function () {
            NAP.loadingBox.show();
        },
        complete: function () {
            NAP.loadingBox.close();
        },
        success: function (result) {
            if (result.length > 0) {
                CreateChartLine(result, obj('chksuper').checked, obj('chkmaster').checked, obj('chkagent').checked, obj('chkaccounting').checked, obj('chkaffiliate').checked);
                obj('reloadIcon').style.display = "none";
            }
            else {
            }
        },
        error: function (xhr, status, err) {
            NAP.handleAjaxError(xhr, status, err);
        }
    });
}
function RefreshChart() {
    $('#loader').show();
    ShowChart();

}
function showOption() {
    $('#loader').show();
    GetUserOnline();
}
var time = 50;
function CountDown(isChart) {
    if (time > 0) {
        time -= 1;
        var min = Math.floor(time / 60);
        var sec = time % 60;
        min = (min < 10 ? "0" + min : min);
        sec = (sec < 10 ? "0" + sec : sec);
        $("#dvtime").html(min + ":" + sec);
    }
    else {
        if (isChart)
            ShowChart();
        else
            showOption();
        time = $("#seltime").val();
    }
}

function GetUserOnline() {
    var top = $('#pageSize').val();
    $.ajax({
        url: NAP.resolveClientUrl("DatabaseReport/UserOnline"),
        data: { size: top },
        type: 'POST',
        dataType: 'html',
        cache: false,
        beforeSend: function () {
            NAP.loadingBox.show();
        },
        complete: function () {
            NAP.loadingBox.close();
        },
        success: function (result) {
            if (result.length > 0) {
                $('#contentUserOnline').html(result);
            }
            else {
            }
        },
        error: function (xhr, status, err) {
            NAP.handleAjaxError(xhr, status, err);
        }
    });
}