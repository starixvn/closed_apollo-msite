var Index = function ($) {

    return {
        type: null,
        server: null,
        jobName: null,
        go: null,

        InitData: function () {
            Index.type = $('#type').val();
            Index.server = $('#server').val().replaceAll('_', '.');
            Index.jobName = $('#jobname').val();
            Index.go = $('#go').val();
        },

        GotoLogDetail: function (type, server, jobName) {
            if (type != '' && server != '') {
                if (type == '2') {
                    document.getElementById('centerFr').src = NAP.resolveClientUrl("JobManagement/JobLogManagement?server=" + server + "&job=" + jobName + "&top=1");
                }
                else {
                    document.getElementById('centerFr').src = NAP.resolveClientUrl("JobManagement/AgentLogManagement?server=" + server + "&top=1");
                }
            }
            else {
                document.getElementById('centerFr').src = NAP.resolveClientUrl("JobManagement/ServerManagement");
            }
        },

        Navigation: function (go) {
            if (go != '') {
                if (go == '1') {
                    document.getElementById('centerFr').src = NAP.resolveClientUrl("JobManagement/ScheduleManagement");
                    $("#leftPanel").attr("go", go);
                }
                if (go == '2') {
                    document.getElementById('centerFr').src = NAP.resolveClientUrl("JobManagement/JobManagement");
                    $("#leftPanel").attr("go", go);
                }
                if (go == '3') {
                    document.getElementById('centerFr').src = NAP.resolveClientUrl("JobManagement/JobLogManagement");
                    $("#leftPanel").attr("go", go);
                }
            }
        }
    };

} (jQuery);

$(document).ready(function () {
    Index.InitData();
    Index.GotoLogDetail(Index.type, Index.server, Index.jobName);
    Index.Navigation(Index.go);
});
