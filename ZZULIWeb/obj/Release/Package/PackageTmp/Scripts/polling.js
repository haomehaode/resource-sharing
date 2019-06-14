$(function () {
    var type = $('.tab_active').attr('id');
    switch (type) {
        case "cc":
            $.ajax({
                type: 'post',
                url: "/Common/Link",
                data: { 'type': "cc" },
                async: true,
                success: function (res) {
                    var data = eval("(" + res + ")");
                    if (data.status == "1") {
                        $('.tab_active').click();
                    }
                }
            });
            break;
        case "cn":
            $.ajax({
                type: 'post',
                url: "/Common/Link",
                data: { 'type': "cn" },
                async: true,
                success: function (res) {
                    var data = eval("(" + res + ")");
                    if (data.status == "1") {
                        $('.tab_active').click();
                    }
                }
            });
            break;
        case "cq":
            $.ajax({
                type: 'post',
                url: "/Common/Link",
                data: { 'type': "cq" },
                async: true,
                success: function (res) {
                    var data = eval("(" + res + ")");
                    if (data.status == "1") {
                        $('.tab_active').click();
                    }
                }
            });
        default:
            break;

    }
});

