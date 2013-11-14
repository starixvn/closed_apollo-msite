function CreateChartLine(strData, IsSuper, IsMaster, IsAgent, IsAccounting, IsAffiliate) {
    if (strData != "") {
        var LableLine = strData.split("^");
        var i, j, k;
        var Super = new Array();
        var Master = new Array();
        var Agent = new Array();
        var Accounting = new Array();
        var Affiliate = new Array();
        var maxValue = 0;
        for (i = 0; i < LableLine.length; i++) {
            var NumLine = LableLine[i].split(";");
            if (IsSuper == true) {
                Super.push([parseInt(NumLine[0]), parseInt(NumLine[1])]);
            }
            if (IsMaster == true) {
                Master.push([parseInt(NumLine[0]), parseInt(NumLine[2])]);
            }
            if (IsAgent == true) {
                Agent.push([parseInt(NumLine[0]), parseInt(NumLine[3])]);
            }
            if (IsAccounting == true) {
                Accounting.push([parseInt(NumLine[0]), parseInt(NumLine[4])]);
            }
            if (IsAffiliate == true) {
                Affiliate.push([parseInt(NumLine[0]), parseInt(NumLine[5])]);
            }
            if (maxValue < parseInt(NumLine[1]))
                maxValue = parseInt(NumLine[1]);
            if (maxValue < parseInt(NumLine[2]))
                maxValue = parseInt(NumLine[2]);
            if (maxValue < parseInt(NumLine[3]))
                maxValue = parseInt(NumLine[3]);
            if (maxValue < parseInt(NumLine[4]))
                maxValue = parseInt(NumLine[4]);
            if (maxValue < parseInt(NumLine[5]))
                maxValue = parseInt(NumLine[5]);
        }

        var myChart = new JSChart('dvChart', 'line');
        if (IsSuper == true) {
            myChart.setDataArray(Super, 'SuperColor'); //Super
            myChart.setLineColor('#EC7A00', 'SuperColor');
        }
        if (IsMaster == true) {
            myChart.setDataArray(Master, 'MasterColor'); //Master
            myChart.setLineColor('#FCD200', 'MasterColor');
        }
        if (IsAgent == true) {
            myChart.setDataArray(Agent, 'AgentColor'); //Agent
            myChart.setLineColor('#81C714', 'AgentColor');
        }
        if (IsAccounting == true) {
            myChart.setDataArray(Accounting, 'AccountingColor'); //Accounting
            myChart.setLineColor('#60B6F0', 'AccountingColor');
        }
        if (IsAffiliate == true) {
            myChart.setDataArray(Affiliate, 'AffiliateColor'); //Affiliate
            myChart.setLineColor('#9CCEF0', 'AffiliateColor');
        }

        myChart.setBackgroundColor('#EEE');
        myChart.setAxisPaddingBottom(40);
        myChart.setTextPaddingBottom(10);
        myChart.setAxisValuesNumberY(0);
        //myChart.setIntervalStartY(-(parseInt(maxValue) + 2));
        //myChart.setIntervalEndY(parseInt(maxValue) + 2);
        myChart.setIntervalStartY(0);

        var _temp = 10;
        var _max = maxValue / _temp;
        while (_max >= 10) {
            _temp *= 10;
            _max = maxValue / _temp;
        }
        _max = Math.ceil(_max) * _temp;
        if (_max == 0) _max += 10;

        myChart.setIntervalEndY(_max);

        for (i = 0; i < LableLine.length; i++) {
            var NumLine = LableLine[i].split(";");
            myChart.setLabelX([parseInt(NumLine[0]), ChangeDateToHour(NumLine[0])]);
        }
        myChart.setAxisValuesNumberX(10);
        myChart.setShowXValues(false);
        myChart.setTitleColor('#383838');
        myChart.setTitle('User Online Charts');
        myChart.setAxisValuesColor('#383838');

        for (i = 0; i < LableLine.length; i++) {
            var NumLine = LableLine[i].split(";");
            myChart.setTooltip([parseInt(NumLine[0])]);
        }
        myChart.setFlagColor('#9D16FC');
        myChart.setFlagRadius(4);
        //myChart.setBackgroundImage('App_Themes/images/chart_bg.jpg');
        myChart.setAxisNameFontSize(10);
        myChart.setGridColor('#383838');
        myChart.setSize(900, 600);
        myChart.draw();
    }
}
function ChangeDateToHour(MyDate) {
       
    var h = Math.floor(parseInt(MyDate) / 60);
    var m = parseInt(MyDate) % 60;
    return (h < 10 ? '0' + h : h) + ":" + (m < 10 ? '0' + m : m) ;
}
