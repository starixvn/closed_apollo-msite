var Connection = function ($) {

    return {
        grid: null,
        aliasName: null,
        aliasId: null,

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
                    }
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

         ShowConnectionString: function (conectionId) {
            $.ajax({
                url: NAP.resolveClientUrl("ConnectionString/GetSecondaryConnection/" + conectionId),
                type: 'GET',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    NAP.popup("Secondary conection string", result);
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

    };

} (jQuery);

$(document).ready(function () {
    Connection.grid = $('.grid');
    Connection.aliasName = $('#aliasName').val();
    Connection.aliasId = $('#aliasId').val();

    Connection.grid.find('[name=connection-default]').click(function () {

        var msg = "Are you sure you want to change default connection of " + Connection.aliasName;
        var cid = $(this).attr('cid');

        NAP.headlineMsgBox.confirm(msg, function () {
            Connection.UpdateDefaultConnection(Connection.aliasId, cid);
        });
    });

    $('.second-conection').click(function () {
        var conectionId = $(this).attr('cid');
        Connection.ShowConnectionString(conectionId);
    });

});
