
var JobManagementPage = function ($) {

    return {

        GetSchedule: function (server) {
            //disabled when server is all
            //            $("#Schedule").trigger("liszt:updated"); 
            if (server == '') {
                $('#ScheduleId').attr('disabled', 'disabled');
            }
            else {
                $('#ScheduleId').removeAttr('disabled', 'disabled');

                $.ajax({
                    url: NAP.resolveClientUrl("JobManagement/GetSchedule"),
                    data: { server: server },
                    type: 'POST',
                    dataType: 'JSON',
                    cache: false,
                    beforeSend: function () {
                        //                    NAP.loadingBox.show();
                        $('#ScheduleId').attr('disabled', 'disabled');
                    },
                    complete: function () {
                        //                    NAP.loadingBox.close();
                        $('#ScheduleId').removeAttr('disabled', 'disabled');
                    },
                    success: function (result) {
                        $("#ScheduleId").html("");
                        $("#ScheduleId").append("<option value=''>All</option>");
                        $("#ScheduleId").append($("#templateSchedule").render(result));
                    },
                    error: function (xhr, status, err) {
                        NAP.handleAjaxError(xhr, status, err);
                    }
                });
            }
        },
        StartJob: function (server, jobName) {
            $.ajax({
                url: NAP.resolveClientUrl("JobManagement/StartJob"),
                data: { server: server, jobName: jobName },
                type: 'POST',
                dataType: 'JSON',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    if (result.Status.IsSuccessful) {
                        NAP.headlineMsgBox.showInfo("Start Job " + jobName + " Successfully !", { closeBtn: false });
                        setTimeout(function () {
                            JobManagementPage.UpdateJobInfo(server, jobName);
                        }, 5000);
                    }
                    else {
                        NAP.headlineMsgBox.showInfo("Start Job " + jobName + " Fail !<br />Message: " + result.Status.Message, { closeBtn: false });
                    }
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },
        StopJob: function (server, jobName) {
            $.ajax({
                url: NAP.resolveClientUrl("JobManagement/StopJob"),
                data: { server: server, jobName: jobName },
                type: 'POST',
                dataType: 'JSON',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    if (result.Status.IsSuccessful) {
                        NAP.headlineMsgBox.showInfo("Stop Job " + jobName + " Successfully !", { closeBtn: false });
                        setTimeout(function () {
                            JobManagementPage.UpdateJobInfo(server, jobName);
                        }, 5000);
                    }
                    else {
                        NAP.headlineMsgBox.showInfo("Stop Job " + jobName + " Fail !<br />Message: " + result.Status.Message, { closeBtn: false });
                    }
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },
        UpdateJobInfo: function (server, jobName) {
            $.ajax({
                url: NAP.resolveClientUrl("JobManagement/GetJob"),
                data: { server: server, jobName: jobName },
                type: 'POST',
                dataType: 'JSON',
                cache: false,
                beforeSend: function () {
                },
                complete: function () {
                },
                success: function (result) {
                    var jobTR = $("#" + result.JobUpdated.ServerIp + "_" + result.JobUpdated.Job.Name);
                    jobTR.find("td.executestatus").html(result.ExecuteStatus);
                    jobTR.find("td.lastrundate").html(result.LastRunDate);
                    jobTR.find("td.nextrundate").html(result.NextRunDate);
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        }
    };

} (jQuery);

$(document).ready(function () {

    //disabled when server is all
    if ($('#Server').val() == '') {
        $('#ScheduleId').attr('disabled', 'disabled');
    }
    else {
        $('#ScheduleId').removeAttr('disabled', 'disabled');
    }

    $('#Server').bind('change', function () {
        var server = $(this).val();
        JobManagementPage.GetSchedule(server);
    });

    // #region actions dropdownlist
    var event = NAP.agentIsOldIe || NAP.agentIsApple ? 'click' : 'hover';

    $(document).on("click", ".action-wrp", function (e) {
        //e.preventDefault();  // if you want to prevent default form submission
//        alert('test');
//    });

//    $('.action-wrp').on("click", function (e) {
        var $this = $(this),
    			dropdown = $this.find('.actions');

        if (dropdown.css('visibility') == 'hidden') {
            $('.actions').css({ visibility: 'hidden', opacity: 0 });
            $this.find('.actions').css({ visibility: 'visible', opacity: 1 });
        } else {
            $this.find('.actions').css({ visibility: 'hidden', opacity: 0 });
        }

        e.stopPropagation();
    });
    // #endregion

    // hides action-panel when clicking outside the action buttons
    $('body, header, #headline, #body').on('click', function () {
        $('.action-wrp').find('.actions').css({ visibility: 'hidden', opacity: 0 });
    });
});