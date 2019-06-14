$(function () {
    $('#lb_nav0').on("change", function () {
        var nav0 = $('#lb_nav0').val();
        if (nav0 == -1) {
            $('#lb_nav1').html("<option>--请选择--</option>");
        } else {
            $.ajax({
                type: 'POST',
                url: "/Common/getnav1",
                data: { 'nav0': nav0 },
                success: function (d) {
                    $('#lb_nav1').html(d);
                }
            });
        }
    });
    $('#search').on("click", function () {
        $('#replace').text("");
        $('#content-null').css('display', 'block');
        var nav0 = $('#lb_nav0').val();
        var nav1 = $('#lb_nav1').val();
        //var url = "";
        //alert(nav0);
        //if (nav0 == -1 && nav1 == -1) {
        //    url = "UpLoad/getcontent/nav_0/nav_0/page_1";
        //}
        //else if (nav0 != -1) {
        //    url = "UpLoad/getcontent/nav_" + nav0 + "/nav_" + -1 + "/page_1";
        //}
        //else {
        //    url = "UpLoad/getcontent/nav_" + nav0 + "/nav_" + nav1 + "/page_1";
        //}
        var pageIndex = parseInt($(this).attr("data-id"));
        //var strPageIndex = pageIndex == 0 ? "" : "page_" + pageIndex;
        replace(nav0, nav1, pageIndex);
    });

})

function replace(nav0, nav1, pageIndex) {
    $.ajax({
        type: 'POST',
        url: "/DownLoad/getcontent",
        data: { 'nav0': nav0, 'nav1': nav1, 'pageIndex': pageIndex },
        success: function (d) {
            $('#content-null').css('display', 'none');
            $('#replace').html(d);
        }
    });
}