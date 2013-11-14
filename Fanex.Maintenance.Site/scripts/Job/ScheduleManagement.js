/* File Created: October 10, 2012 */
var ScheduleManagementPage = function ($) {

    return {
        dateFrom: null,
        dateTo: null,
        buttonImage: null,

        InitData: function () {
            ScheduleManagementPage.dateFrom = $("#dateFrom").val();
            ScheduleManagementPage.dateTo = $("#dateTo").val();
            ScheduleManagementPage.buttonImage = $('#imagePath').val();

        },

        //disbaled when datetype is all
        SelectDateType: function () {
            var type = $('#SearchDateType').val();
            if (type == "") {
                $('#From').attr('disabled', 'disabled');
                $('#To').attr('disabled', 'disabled');
            }
            else {
                $('#From').removeAttr('disabled', 'disabled');
                $('#To').removeAttr('disabled', 'disabled');
            }
        },

        InitDatetimePicker: function () {
            $("#From").val(ScheduleManagementPage.dateFrom);
            $("#To").val(ScheduleManagementPage.dateTo);

            $("#From").datepicker({
                //defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 1,
                dateFormat: "M dd,yy",
                showOn: "button",
                buttonImage: ScheduleManagementPage.buttonImage,
                buttonImageOnly: true,
                buttonText: "From",
                onSelect: function (selectedDate) {
                    $("#To").datepicker("option", "minDate", selectedDate);
                    if ($('#To').val() == "") {
                        $("#To").val($(this).val());
                    }
                }
            });
            $("#To").datepicker({
                //defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 1,
                dateFormat: "M dd,yy",
                showOn: "button",
                buttonImage: ScheduleManagementPage.buttonImage,
                buttonImageOnly: true,
                buttonText: "To",
                onSelect: function (selectedDate) {
                }
            });

        }
    };

} (jQuery);

$(document).ready(function () {
    ScheduleManagementPage.InitData();
    ScheduleManagementPage.InitDatetimePicker();
    ScheduleManagementPage.SelectDateType();

    $('#SearchDateType').bind('change', function () {
        ScheduleManagementPage.SelectDateType();
    });

    // #region actions dropdownlist
    var event = NAP.agentIsOldIe || NAP.agentIsApple ? 'click' : 'hover';

    $('.action-wrp').bind(event, function () {
        var $this = $(this),
			dropdown = $this.find('.actions');

        if (dropdown.css('visibility') == 'hidden') {
            $('.actions').css({ visibility: 'hidden', opacity: 0 });
            $this.find('.actions').css({ visibility: 'visible', opacity: 1 });
        } else {
            $this.find('.actions').css({ visibility: 'hidden', opacity: 0 });
        }
    });

}); 