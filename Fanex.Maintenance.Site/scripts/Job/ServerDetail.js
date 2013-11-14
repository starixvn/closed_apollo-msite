/* File Created: October 30, 2012 */
var ServerDetail = function ($) {
    return {
        Server: null,
        CpuServer: null,
        MemoryServer: null,
        FirstTime: true,
        MaxMemory: 0,
        ResizeCount: 0,
        Init: function (resourceInfo) {
            ServerDetail.CpuServer = resourceInfo.CpuUtilization;
            ServerDetail.MemoryServer = resourceInfo.MemoryUtilization;
            var sqlCpuChart = ServerDetail.RenderChart("slqCpuContainer", 1, "Sql Server Cpu", "%", "Cpu Usage");
            var memoryChart = ServerDetail.RenderChart("memoryContainer", 2, "Sql Server Memory Usage", "%", "Memory Usage");

            if (ServerDetail.CpuServer != null && ServerDetail.MemoryServer != null && ServerDetail.FirstTime == true) {
                ServerDetail.FirstTime = false;
                $('#ListServer').change(function () {
                    var queryString = NAP.resolveClientUrl("JobManagement/ServerDetail?serverIp=" + $(this).val());
                    parent.$("#centerFr").attr("src", queryString);
                });
                $('span.backlink').live("click", function () {
                    parent.$("#centerFr").attr("src", NAP.resolveClientUrl("JobManagement/ServerManagement"));
                });

                $(window).resize(function () {
                    if (ServerDetail.ResizeCount > 1) {
                        ServerDetail.ResizeCount = 0;
                    }
                    else {
                        ServerDetail.ResizeCount++;
                        $('.resourceinfo').css("width", $(window).width() - 12);
                        $(window).trigger("resize");
                    }


                });

                $(window).trigger("resize");
                NAP.loadingBox.close();
            }
        },
        UpdateServerData: function (resourceInfo) {
            if (ServerDetail.Server.ServerIp == resourceInfo.ServerIp) {
                ServerDetail.Server = resourceInfo;
            }
        },
        UpdateServerUtilizeCpu: function (cpuInfo) {
            if (ServerDetail.CpuServer.ServerIp == cpuInfo.ServerIp) {
                ServerDetail.CpuServer = cpuInfo;
            }

        },
        UpdateServerUtilizeMemory: function (memoryInfo) {
            if (ServerDetail.MemoryServer.ServerIp == memoryInfo.ServerIp) {
                ServerDetail.MemoryServer = memoryInfo;
            }

        },
        GetDataFromServerName: function (type) {
            if (type == 1) {
                return ServerDetail.CpuServer.SqlCpuPercent;
            }
            else {
                return ServerDetail.MemoryServer.MemoryUsage;
            }
        },
        RenderChart: function (charid, type, title, ytitle, pointTitle) {
            var pointColor = (type == 1 ? '#4572A7' : '#ff7200');
            var getDataInterval = (type == 1 ? getCpuUtilizationInterval : getMemoryUtilizationInterval)
            return new Highcharts.Chart({
                name: charid + type,
                chart: {
                    renderTo: charid,
                    type: 'spline',
                    marginRight: 25,
                    //width: 1000,
                    events: {
                        load: function () {

                            // set up the updating of the chart each second
                            var series = this.series[0];
                            setInterval(function () {
                                var x = (new Date()).getTime() // current time
                                y = ServerDetail.GetDataFromServerName(type);
                                if (type != 1) {
                                    y = Math.round(y * 100 / ServerDetail.MemoryServer.TotalMemory)
                                    $('#currentmemory').text(y);
                                }
                                else {
                                    $('#currentcpu').text(y);
                                }
                                series.addPoint([x, y], true, true);
                            }, getDataInterval);
                        }
                    }
                },
                title: {
                    text: title
                },
                xAxis: {
                    type: 'datetime',
                    tickPixelInterval: 100
                },
                yAxis: {
                    title: {
                        text: ytitle
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#8011111'
                    }],
                    min: 0,
                    max: 100
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.series.name + '</b><br/>' +
                                Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br/>' +
                                Highcharts.numberFormat(this.y, 2);
                    }
                },
                legend: {
                    enabled: false
                },
                exporting: {
                    enabled: false
                },
                series: [{
                    name: pointTitle,
                    color: pointColor,
                    data: (function () {
                        // generate an array of random data
                        var data = [],
                                time = (new Date()).getTime(),
                                i;
                        var yValue = ServerDetail.GetDataFromServerName(type);
                        if (type != 1) {
                            yValue = Math.round(yValue * 100 / ServerDetail.MemoryServer.TotalMemory)
                            $('#currentmemory').text(yValue);
                        }
                        else {
                            $('#currentcpu').text(yValue);
                        }
                        for (i = -10; i <= 0; i++) {
                            data.push({
                                x: time + i * getDataInterval,
                                y: yValue
                            });

                        }
                        return data;
                    })()
                }]
            });
        }
    };
} (jQuery);



$(function () {
    $('#ListServer').val(serverIp);
    Highcharts.setOptions({
        global: {
            useUTC: false
        }
    });

    // Proxy created on the fly
    var serverInfoHub = $.connection.serverInfoHub;

    // Declare a function on the chat hub so the server can invoke it
    serverInfoHub.client.addMessage = function (message) {
        alert(message);
    };

    serverInfoHub.client.firstMessage = function (resourceInfoes) {
        ServerDetail.Init(resourceInfoes);
    };

    serverInfoHub.client.gotMessage = function (resourceInfo) {
        if (!ServerDetail.FirstTime) {
            ServerDetail.UpdateServerData(resourceInfo);
        }
    };

    serverInfoHub.client.getCpuInfo = function (resourceInfo) {
        ServerDetail.UpdateServerUtilizeCpu(resourceInfo);
    };

    serverInfoHub.client.getMemoryInfo = function (resourceInfo) {
        ServerDetail.UpdateServerUtilizeMemory(resourceInfo);
    };

    $.connection.hub.start().done(function () {
        $.ajax({
            url: NAP.resolveClientUrl("JobManagement/GetServerResourceInfo"),
            data: { serverIp: serverIp },
            type: 'POST',
            dataType: 'JSON',
            cache: false,
            beforeSend: function () {
                NAP.loadingBox.show();
            },
            complete: function () {
                NAP.loadingBox.close();
            },
            success: function (resourceInfo) {
                if (resourceInfo != null) {
                    ServerDetail.Init(resourceInfo);
                }
                else {
                    
                }
            },
            error: function (xhr, status, err) {
                NAP.handleAjaxError(xhr, status, err);
            }
        });
    });
});