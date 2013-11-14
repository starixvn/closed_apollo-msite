/* File Created: October 29, 2012 */
var ServerManagement = function ($) {
    return {
        Servers: [],
        CpuServers: [],
        MemoryServers: [],
        FirstTime: true,
        MaxMemory: 0,
        ResizeCount: 0,
        IsHighLightTime: false,
        InitEvent: function () {
            //// Click toggle to collapse
            $('.subcontainer .toggler').live("click", function () {
                var $this = $(this);

                var grantParentNode = $this.parent().parent();
                var serverName = $this.attr("servername");
                var serverShow = $this.attr("servershow");
                var $sqlCpu = $("#" + serverName + "SqlCpu");
                var $sqlMemory = $("#" + serverName + "Memory");
                if (serverShow == "1") {
                    $sqlCpu.hide("fast");
                    $sqlMemory.hide("fast");
                    $this.attr("servershow", "0")
                .removeClass("collapse")
                .addClass("expand");

                    grantParentNode.css("height", 30);
                }
                else {
                    $sqlCpu.show("fast");
                    $sqlMemory.show("fast");
                    $this.attr("servershow", "1")
                .removeClass("expand")
                .addClass("collapse");
                    grantParentNode.css("height", 480);
                }
            });

            //// Handle collapse all container
            $('#togglerall').live("click", function () {
                var $this = $(this);
                var serverShow = $this.attr("servershow");

                if (serverShow == "1") {
                    $(".subcontainer .toggler").attr("servershow", "1")
                .trigger("click");

                    $this.attr("servershow", "0")
                .removeClass("collapse")
                .addClass("expand");
                }
                else {
                    $(".subcontainer .toggler").attr("servershow", "0")
                .trigger("click");

                    $this.attr("servershow", "1")
                .removeClass("expand")
                .addClass("collapse");
                }
            });

            //// Load other server detail
            $(".serverinfo .serveripchart").live("click", function () {
                var serverIp = $(this).attr("serverdetailip");
                var queryString = NAP.resolveClientUrl("JobManagement/ServerDetail?serverIp=" + serverIp);
                parent.$("#centerFr").attr("src", queryString);
            });

            //// Draggable
            $("#resourceInfoContainerLeft, #resourceInfoContainerRight").sortable({
                connectWith: ".connectedSortable"
            }).disableSelection();

            setInterval(ServerManagement.HighLightServer, 1000);
        },
        UpdateServerData: function (resourceInfo) {
            var isExist = false;
            for (var i = 0; i < ServerManagement.Servers.length; i++) {
                if (ServerManagement.Servers[i].ServerIp == resourceInfo.ServerIp) {
                    ServerManagement.Servers[i] = resourceInfo;
                    isExist = true;
                    break;
                }
            }

            if (isExist == false) {
                ServerManagement.Servers.push(resourceInfo);
                var sqlCpuChart = ServerManagement.RenderChart(resourceInfo.ServerName, 1, "Sql Server Cpu", "%", "Cpu Usage");
                var sqlMemoryChart = ServerManagement.RenderChart(resourceInfo.ServerName, 2, "Sql Server Memory Usage", "%", "Memory Usage");
            }
        },
        UpdateServerUtilizeCpu: function (cpuInfo) {
            var isExist = false;
            for (var i = 0; i < ServerManagement.CpuServers.length; i++) {
                if (ServerManagement.CpuServers[i].ServerIp == cpuInfo.ServerIp) {
                    ServerManagement.CpuServers[i] = cpuInfo;
                    isExist = true;
                    break;
                }
            }

            if (isExist == false) {
                ServerManagement.CpuServers.push(cpuInfo);
                var sqlCpuChart = ServerManagement.RenderChart(cpuInfo.ServerName, 1, "Sql Server Cpu", "%", "Cpu Usage");
            }
        },
        UpdateServerUtilizeMemory: function (memoryInfo) {
            var isExist = false;
            for (var i = 0; i < ServerManagement.MemoryServers.length; i++) {
                if (ServerManagement.MemoryServers[i].ServerIp == memoryInfo.ServerIp) {
                    ServerManagement.MemoryServers[i] = memoryInfo;
                    isExist = true;
                    break;
                }
            }

            if (isExist == false) {
                ServerManagement.MemoryServers.push(memoryInfo);
                var sqlMemoryChart = ServerManagement.RenderChart(memoryInfo.ServerName, 2, "Sql Server Memory Usage", "%", "Memory Usage");
            }
        },
        GetMaxMemory: function (serverName) {
            for (var i = 0; i < ServerManagement.MemoryServers.length; i++) {
                var server = ServerManagement.MemoryServers[i];
                if (server.ServerName == serverName) {
                    return server.TotalMemory;
                }
            }
        },
        GetDataFromServerName: function (serverName, type) {
            if (type == 1) {
                for (var i = 0; i < ServerManagement.CpuServers.length; i++) {
                    var server = ServerManagement.CpuServers[i];
                    if (server.ServerName == serverName) {
                        //var containerdiv = $("#" + serverName + (type == 1 ? "SqlCpu" : "Memory")).prev();
                        var parentContainerDiv = $("#" + serverName + (type == 1 ? "SqlCpu" : "Memory")).parent();

                        //// Check if New Sql Cpu new is difference with Old then change the text color to red
                        var oldSqlCpu = parentContainerDiv.find(".servermemoryusagevalue").text();
                        var newSqlCpu = server.SqlCpuPercent;
                        if (oldSqlCpu != newSqlCpu) {
                            var serverCpu = parentContainerDiv.find(".servercpu");
                            var currentColor = $(body).css("color");
                            serverCpu.css("color", "Red");
                            setTimeout(function () {
                                serverCpu.css("color", currentColor);
                            }, 3000);
                        }
                        parentContainerDiv.find(".servermemoryusagevalue").text(newSqlCpu);

                        //// For HighLightServer
                        parentContainerDiv.attr("informvalue", newSqlCpu);

                        //// return info
                        return server.SqlCpuPercent;
                    }
                }
            }
            else {
                for (var i = 0; i < ServerManagement.MemoryServers.length; i++) {
                    var server = ServerManagement.MemoryServers[i];
                    if (server.ServerName == serverName) {
                        var parentContainerDiv = $("#" + serverName + (type == 1 ? "SqlCpu" : "Memory")).parent();

                        //// Check if New Sql Ram different with Old, then change the text color to Red
                        var oldSqlRam = parentContainerDiv.find(".servermemoryusage").text();
                        var newSqlRam = Math.round(server.MemoryUsage * 100 / server.TotalMemory);
                        if (oldSqlRam != newSqlRam) {
                            var serverMemory = parentContainerDiv.find(".servermemory");
                            var currentColor = $(body).css("color");
                            serverMemory.css("color", "Red");
                            setTimeout(function () {
                                serverMemory.css("color", currentColor);
                            }, 3000);
                        }
                        parentContainerDiv.find(".servermemoryusage").text(newSqlRam);

                        //// return info
                        return server.MemoryUsage;
                    }
                }
                ///return server.MemoryUsage;
            }
        },
        HighLightServer: function () {
            $('.subcontainer').each(function () {
                var $this = $(this);
                var currentValue = parseInt($this.attr("informvalue"));
                var ishighlight = $this.attr("ishightlight");
                if (ishighlight == "0") {
                    if (currentValue > 50) {
                        if (currentValue > 75) {
                            $this.css("border", "2px solid #FF0000")
                        }
                        else {
                            $this.css("border", "2px solid #FF9933")
                        }
                    }
                    else {
                        $this.css("border", "1px solid #C8C8C8");
                    }
                }
                else {
                    $this.css("border", "1px solid #C8C8C8");
                }
                $this.attr("ishightlight", (ishighlight == "0") ? "1" : "0");
            });
        },
        RenderChart: function (charid, type, title, ytitle, pointTitle) {
            // Type 1 is Sql Cpu, else Memory
            var charIdCombine = charid + (type == 1 ? "SqlCpu" : "Memory");
            var pointColor = (type == 1 ? '#4572A7' : '#ff7200');
            var getDataInterval = (type == 1 ? getCpuUtilizationInterval : getMemoryUtilizationInterval)
            var yAxisCustom = (type == 1 ? {
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
            } : {
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
            });
            return new Highcharts.Chart({
                name: charid,
                chart: {
                    renderTo: charIdCombine,
                    borderRadius: 0,
                    type: 'spline',
                    marginRight: 25,
                    events: {
                        load: function () {

                            // set up the updating of the chart each second
                            var series = this.series[0];
                            setInterval(function () {
                                var x = (new Date()).getTime();
                                var y = ServerManagement.GetDataFromServerName(charid, type);

                                if (type != 1) {
                                    y = Math.round(y * 100 / ServerManagement.GetMaxMemory(charid))
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
                    tickPixelInterval: 75
                },
                yAxis: yAxisCustom,
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
                        var data = [];
                        var time = (new Date()).getTime();
                        var i;

                        for (i = -10; i <= 0; i++) {
                            var yValue = ServerManagement.GetDataFromServerName(charid, type);
                            if (type != 1) {
                                yValue = Math.round(yValue * 100 / ServerManagement.GetMaxMemory(charid))
                            }
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
    // Proxy created on the fly
    var serverInfoHub = $.connection.serverInfoHub;

    // Declare a function on the chat hub so the server can invoke it
    serverInfoHub.client.addMessage = function (message) {
        alert(message);
    };

    serverInfoHub.client.gotMessage = function (resourceInfo) {
        ServerManagement.UpdateServerData(resourceInfo);
    };

    serverInfoHub.client.getCpuInfo = function (resourceInfo) {
        ServerManagement.UpdateServerUtilizeCpu(resourceInfo);
    };

    serverInfoHub.client.getMemoryInfo = function (resourceInfo) {
        ServerManagement.UpdateServerUtilizeMemory(resourceInfo);
    };

    ServerManagement.InitEvent();
    
    $.connection.hub.start(function () {
    });
});
