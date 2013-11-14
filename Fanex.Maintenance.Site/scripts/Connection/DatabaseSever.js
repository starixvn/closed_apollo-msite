var DatabaseSever = function ($) {

    return {
        grid: null,

        ShowNumOfSite: function (dbServerId, dbServerName) {
            $.ajax({
                url: NAP.resolveClientUrl("ConnectionString/GetSiteByDbServer/" + dbServerId),
                type: 'GET',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    NAP.popup("Site of database server '" + dbServerName + "'", result);
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

        UpdateStatusDbServer: function (dbServerId, isActive) {
            $.ajax({
                url: NAP.resolveClientUrl("ConnectionString/UpdateStatusDbServer"),
                data: { dbServerId: dbServerId, isActive: isActive },
                type: 'POST',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    if (result == 'true') {
                        NAP.headlineMsgBox.showInfo('Your changes were saved!', { closeBtn: false });
                        NAP.redirect('ConnectionString/DatabaseServer');
                    }
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        }
    };

} (jQuery);

$(document).ready(function () {
    DatabaseSever.grid = $('.grid');

    $('.num-site').click(function () {
        var $this = $(this);
        var dbServerId = $this.attr('did');
        var dbServerName = $this.attr('dname');

        DatabaseSever.ShowNumOfSite(dbServerId, dbServerName);
    });

    DatabaseSever.grid.on('mousedown', 'input[type=checkbox][did]', function (e) {
        var $this = $(this);
        var dbServerId = $this.attr('did');
        var dbServerName = $this.attr('dname');
        var active = !$this.is(':checked'),
			action = active ? 'activate' : 'deactivate',
             msg = "Are you sure you want to " + action + " '" + dbServerName + "' ?";

        NAP.headlineMsgBox.confirm(msg, function () {

            $this.attr('checked', active).change();

            DatabaseSever.UpdateStatusDbServer(dbServerId, active);
        });
    });
});
