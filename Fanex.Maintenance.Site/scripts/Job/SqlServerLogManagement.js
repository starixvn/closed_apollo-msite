/* File Created: October 10, 2012 */

var SqlServerLogManagement = function ($) {

    return {
        dateFrom: null,
        dateTo: null,
        buttonImage: null,
        serverIpOld: null,

        InitData: function () {
            SqlServerLogManagement.dateFrom = $("#dateFrom").val();
            SqlServerLogManagement.dateTo = $("#dateTo").val();
            SqlServerLogManagement.buttonImage = $('#imagePath').val();
            SqlServerLogManagement.serverIp = $('#Server').val();
            $('#PageSizeSelect').val($('#PageSize').val());
        },

        InitDatetimePicker: function () {
            $("#From").val(SqlServerLogManagement.dateFrom);
            $("#To").val(SqlServerLogManagement.dateTo);

            $("#From").datepicker({
                //defaultDate: "+5d",
                changeMonth: true,
                numberOfMonths: 1,
                dateFormat: "M dd,yy",
                showOn: "button",
                buttonImage: SqlServerLogManagement.buttonImage,
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
                //defaultDate: "+5d",
                changeMonth: true,
                numberOfMonths: 1,
                dateFormat: "M dd,yy",
                showOn: "button",
                buttonImage: SqlServerLogManagement.buttonImage,
                buttonImageOnly: true,
                buttonText: "To",
                onSelect: function (selectedDate) {
                }
            });
        },
        OnNextPage: function (moveIndex) {
            var currentIndex = parseInt($('#PageIndex').val());
            if (currentIndex + moveIndex >= 0) {
                $('#PageIndex').val(currentIndex + moveIndex);
                $('.content form').submit();
            }
        },
        OnChangePageSize: function () {
            $('#PageSize').val($('#PageSizeSelect').val());
            SqlServerLogManagement.UpdatePaging();
            $('.content form').submit();
        },
        UpdatePaging: function () {
            $('#PageIndex').val(0);
            $('#PageSize').val($('#PageSizeSelect').val());
        },
        UpdateNavigation: function () {
            var count = $('#countData').val();
            if (count == 0) {
                $('.pagiNext').hide();
            }
            if ($('#PageIndex').val() == 0) {
                $('.pagiPre').hide();
            }
        }
    };

} (jQuery);

$(document).ready(function () {
    SqlServerLogManagement.InitData();
    SqlServerLogManagement.InitDatetimePicker();
    SqlServerLogManagement.UpdateNavigation();
});     