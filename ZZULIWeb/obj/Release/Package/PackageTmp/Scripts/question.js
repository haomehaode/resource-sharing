$(function () {
    even_follow();
});

function even_follow() {
    var nofollow = document.getElementsByClassName('no-follow');
    for (var i = 0; i < nofollow.length; i++) {
        follow(nofollow[i]);
    }
}

function follow(obj) {
    obj.onclick = function () {
        var target_id = $(this).attr('data-id');
        $.ajax({
            type: 'POST',
            url: '/Common/Follow',
            data: { 'target_id': target_id },
            success: function (data) {
                var data = (new Function("", "return " + data))();
                if (data.status == 0) {
                    swal({ title: "Error!", text: "关注过程发生错误!", type: "error", timer: 2000 });
                } else if (data.status == 1) {
                    window.location = "/Question/List";
                } else {
                    window.location = "/Login/Index";
                }
            },
            error: function (XMLHttpRequest,textStatus,ErrorThrow) {
                swal({ title: "Error!", text: "关注失败，请重试!", type: "error", timer: 2000 });
            }
        });
    }
}