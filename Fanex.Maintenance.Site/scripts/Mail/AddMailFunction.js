var AddMailConfigPopUp = function ($) {

    return {

    };

} (jQuery);

$(document).ready(function () {

    $('.addconfig').click(function () {

        $.ajax({
            url: NAP.resolveClientUrl("Mail/AddMailConfigPopUp"),
            type: 'GET',
            dataType: 'html',
            cache: false,
            beforeSend: function () {
                //NAP.loadingBox.show();
            },
            complete: function () {
                //NAP.loadingBox.close();
            },
            success: function (result) {
                if (result.length > 0) {
                    NAP.popup("Create New Mail Setting", result);
                }
            },
            error: function (xhr, status, err) {
                //NAP.handleAjaxError(xhr, status, err);
            }
        });
    });

    $('.addsetting').click(function () {

        $.ajax({
            url: NAP.resolveClientUrl("Mail/AddMailSettingPopUp"),
            type: 'GET',
            dataType: 'html',
            cache: false,
            beforeSend: function () {
                //NAP.loadingBox.show();
            },
            complete: function () {
                //NAP.loadingBox.close();
            },
            success: function (result) {
                if (result.length > 0) {
                    NAP.popup("Create New Mail Setting", result);
                }
            },
            error: function (xhr, status, err) {
                //NAP.handleAjaxError(xhr, status, err);
            }
        });
    });
});