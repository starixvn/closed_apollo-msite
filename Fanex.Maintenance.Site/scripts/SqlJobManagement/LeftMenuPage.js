/* File Created: October 8, 2012 */
$(function () {
    $("#browser").treeview({
        animated: "medium"
    });
    $("#browser li span").hover(function () {
        $(this).addClass("hover");
    }, function () {
        $(this).removeClass("hover");
    }).click(function () {
        var $this = $(this);
        if ($this.parent().not(":has(ul)").length > 0) {
            $("#browser li span.choosen").removeClass("choosen");
            $this.not(":has(ul)").addClass("choosen");
        }
    });
});
var LeftMenuPage = function ($) {
    return {};
} (jQuery);

LeftMenuPage.onRedirect = function (urlRedirect) {
    parent.$("#centerFr").attr("src", urlRedirect);
};