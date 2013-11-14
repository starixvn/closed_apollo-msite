
var Layout = function ($) {

    return {
        body: null
    };

} (jQuery);

$(document).ready(function () {
    Layout.body = $('body');

    setTimeout('Layout.body.fadeIn()', 200);
});