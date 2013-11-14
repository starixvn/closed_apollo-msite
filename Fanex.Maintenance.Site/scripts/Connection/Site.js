var Site = function ($) {

    return {
        grid: null,

        ShowNumOfAlias: function (siteId, siteName) {
            $.ajax({
                url: NAP.resolveClientUrl("ConnectionString/GetAliasBySite/" + siteId),
                type: 'GET',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    NAP.popup("Alias of site '" + siteName + "'", result);
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

        ShowSiteConnection: function (siteId, siteName) {
            $.ajax({
                url: NAP.resolveClientUrl("ConnectionString/ViewConnection?siteId=" + siteId),
                type: 'GET',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    NAP.popup("Connection of site '" + siteName + "'", result);
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

        UpdateStatusSite: function (siteId, isActive) {
            $.ajax({
                url: NAP.resolveClientUrl("ConnectionString/UpdateStatusSite"),
                data: { siteId: siteId, isActive: isActive },
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
                        NAP.redirect('ConnectionString/Site');
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
    Site.grid = $('.grid');

    $('.num-alias').click(function () {
        var $this = $(this);
        var siteId = $this.attr('sid');
        var siteName = $this.attr('sname');

        Site.ShowNumOfAlias(siteId, siteName);
    });

    $('.site-con').click(function () {
        var $this = $(this);
        var siteId = $this.attr('sid');
        var siteName = $this.attr('sname');

        Site.ShowSiteConnection(siteId, siteName);
    });

    Site.grid.on('mousedown', 'input[type=checkbox][sid]', function (e) {
        var $this = $(this);
        var siteId = $this.attr('sid');
        var siteName = $this.attr('sname');
        var active = !$this.is(':checked'),
			action = active ? 'activate' : 'deactivate',
             msg = "Are you sure you want to " + action + " '" + siteName + "' ?";

        NAP.headlineMsgBox.confirm(msg, function () {

            $this.attr('checked', active).change();

            Site.UpdateStatusSite(siteId, active);
        });
    });
});
