/* File Created: October 17, 2012 */
// Array Remove All elements matched the criteria
// Proxy created on the fly
var notitficationHub = $.connection.notificationHub;

// Declare a function on the chat hub so the server can invoke it
notitficationHub.client.addMessage = function (message) {
    alert(message);
};

notitficationHub.client.gotMessage = function (notifyInfoes) {
    for (var i = 0; i < notifyInfoes.length; i++) {
        var notifyInfo = notifyInfoes[i];
        var queryString = "type=" + notifyInfo.Type + "&server=" + notifyInfo.ServerIp;
        if (notifyInfo.Type == 2) {
            queryString += "&jobname=" + encodeURI(notifyInfo.JobName);
        }

        var message = "Server: " + notifyInfo.ServerIp.split('_').join('.') + "<br/>"
                + (notifyInfo.Type == 1 ? "Sql Server Agent Error" : "Job Error") + "<br />"
                + "Occur at " + notifyInfo.OccurTime + "<br />"
                + "Message: " + notifyInfo.Message + "<br />"
                + "<a href=" + NAP.resolveClientUrl("JobManagement?" + encodeURI(queryString)) + ">View Detail</a>";

        $().toastmessage('showToast', {
            text: message,
            //                sticky: true,
            stayTime: 20000,
            position: 'bottom-right',
            type: 'error',
            closeText: '',
            close: function () {
                //                    console.log("toast is closed ...");
            }
        });
    }
}


$(function () {
    $.connection.hub.start(function () {
    });
});