$(document).ready(function () {
    FirstLoad();
    DisplayButton("", $('input[name=hdDate]').val());

    $('input[type=submit]').click(function () {
        console.log('a');
    });
})

//Method use load data default
function FirstLoad() {
    var isFirstLoad = $('input[name=chkFirstLoad]').val();
    if (isFirstLoad == "true") {
        ShowData("selectedDate", "<%= minDate %>", "<%= maxDate %>");
    }
}

/*------------------------------------------------------------------
Method use display button depend day is mindate or maxdate
if day is mindate then previous button disable
if day i maxdate then previous button disable
------------------------------------------------------------------*/
function DisplayButton(minDate, maxDate) {
    var dateSelected = $('input[name=hdDate]').val();
    $('#spDateSelected').text($('input[name=hdDate]').val());

    if (dateSelected == minDate)
        $('#btPreviousDay').attr("disabled", true);
    else
        $('#btPreviousDay').attr("disabled", false);

    if (dateSelected == maxDate)
        $('#btNextDay').attr("disabled", true);
    else
        $('#btNextDay').attr("disabled", false);
}

/*------------------------------------------------------------------
Method use display data depend user choose date, and it display
button is disable or enable depend mindate and maxdate
------------------------------------------------------------------*/

function ShowData(chooseDate, minDate, maxDate) {

    //Get month, date and year from hidden field
    var month = $('input[name=hdDate]').val().substring(0, 2);
    var date = $('input[name=hdDate]').val().substring(3, 5);
    var year = $('input[name=hdDate]').val().substring(6, 10);

    var valueDate = $('input[name=hdDate]').val();

    if (chooseDate == "previousDate" && valueDate != minDate) {
        var previousDate = parseFloat(date) - 1;

        /*---------------------------------------------------------------------------
        If date choose is 1 then previous date is 0 month will previous 1 month 
        from month choose and previous date will set last date of previous month
        If previous month is 0 then year previous - 1 year from year choose
        and previous month will set is 12
        ---------------------------------------------------------------------------*/
        if (previousDate == 0) {
            month = parseFloat(month) - 1;
            if (month == 0) {
                year = parseFloat(year) - 1;
                month = 12;
            }
            previousDate = new Date(year, month, 0).getDate();
            //format time follow MM/dd/YYYY
            month = month < 10 ? "0" + month : month;
        }
        else {
            //format time follow MM/dd/YYYY
            previousDate = previousDate < 10 ? "0" + previousDate : previousDate;
        }

        //Set again values for lable date time and calendar
        valueDate = month + "/" + previousDate + "/" + year;
        $('#txtDate').text(valueDate);
        $('input[name=hdDate]').val(valueDate);
        var valueDate1 = valueDate.replace("/", "-");
    }
    else if (chooseDate == "nextDate" && valueDate != maxDate) {
        var nextDate = parseFloat(date) + 1;
        var lastDateInMonth = new Date(year, month, 0).getDate();

        /*------------------------------------------------------------------------------
        If date choose is last date in month then next date is 1 month will next 1 
        month from month choose
        If next month is 13 then next year + 1 year from year choose and next month 
        will set is 1
        ------------------------------------------------------------------------------*/
        if (nextDate - 1 == lastDateInMonth) {
            month = parseFloat(month) + 1;
            if (month == 13) {
                year = parseFloat(year) + 1;
                month = 1;
            }
            nextDate = "01";
            //format time follow MM/dd/YYYY
            month = month < 10 ? "0" + month : month;
        }
        else {
            //format time follow MM/dd/YYYY
            nextDate = nextDate < 10 ? "0" + nextDate : nextDate;
        }
        //Set again values for lable date time and calendar
        valueDate = month + "/" + nextDate + "/" + year;
        $('#txtDate').text(valueDate);
        $('input[name=hdDate]').val(valueDate);
    }

    //Set again values for lable date time and calendar
    $('#txtDate').text(valueDate);
    $("#datepicker").datepicker("setDate", valueDate)
    $('#spDateSelected').text(valueDate);

    //Enable or disable button
    DisplayButton(minDate, maxDate);

    $('#loader').show();
    $.post("GetLicenseeData.aspx",
    {
        d: valueDate,
        filter: $("input[name='radDrawFilter']:checked").val()
    },
    function (res) {
        $("#dvLicenseedata").html(res);
    }); //dvLicenseedata

    $.post("GetCreditData.aspx",
    {
        d: valueDate,
        filter: $("input[name='radDrawFilter']:checked").val()
    },
    function (res) {
        $("#dvCreditdata").html(res);
        $('#loader').hide();
        $("#btnsubmit").show();
    }); //dvCreditdata
}

