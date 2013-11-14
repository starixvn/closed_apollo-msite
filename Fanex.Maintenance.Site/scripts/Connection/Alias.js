var Alias = function ($) {

    return {
        grid:null,
        
        ShowNumOfSite: function (alaisId, aliasName) {
            $.ajax({
                url: NAP.resolveClientUrl("ConnectionString/GetSiteByAlias/" + alaisId),
                type: 'GET',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    NAP.popup("Site of alias '" + aliasName + "'", result);
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

        ShowConnectionString: function (alaisId, aliasName) {
            $.ajax({
                url: NAP.resolveClientUrl("ConnectionString/GetConnectionByAlias/" + alaisId),
                type: 'GET',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    NAP.popup("Connection string of alias '" + aliasName + "'", result);
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

        UpdateDefaultConnection: function (alaisId, conectionId) {
            $.ajax({
            	url: NAP.resolveClientUrl("ConnectionString/UpdateDefaultConnection"),
                data: { aliasId: alaisId, conectionId: conectionId },
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
                        NAP.redirect('ConnectionString/Alias');
                    }
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

        UpdateStatusAlias: function (alaisId, isActive) {
            $.ajax({
                url: NAP.resolveClientUrl("ConnectionString/UpdateStatusAlias"),
                data: { aliasId: alaisId, isActive: isActive },
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
                        NAP.redirect('ConnectionString/Alias');
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
    Alias.grid = $('.grid');

    $('.numofsite').click(function () {
        var $this = $(this);
        var aliasId = $this.attr('aid');
        var aliasName = $this.attr('aname');

        Alias.ShowNumOfSite(aliasId, aliasName);
    });

    $('.change-connection').click(function () {
        var $this = $(this);
        var aliasId = $this.attr('aid');
        var aliasName = $this.attr('aname');

        Alias.ShowConnectionString(aliasId, aliasName);
    });

    Alias.grid.on('mousedown', 'input[type=checkbox][aid]', function (e) {
        var $this = $(this);
        var aliasId = $this.attr('aid');
        var aliasName = $this.attr('aname');
        var active = !$this.is(':checked'),
			action = active ? 'activate' : 'deactivate',
             msg = "Are you sure you want to " + action + " '" + aliasName + "' ?";

        NAP.headlineMsgBox.confirm(msg, function () {

            $this.attr('checked', active).change();

            Alias.UpdateStatusAlias(aliasId, active);
        });
    });

    $('#btnChangeConnection').live('click', function () {
        var $this = $(this);
        $('#dialog-popup').find('[name=conection]:checked').each(function () {
            var currentConnection = $(this);
            var connectionId = currentConnection.attr('cid');
            var aliasId = currentConnection.attr('aid');

            Alias.UpdateDefaultConnection(aliasId, connectionId);
        });
    });
});
