$(function () {
    $('.collect').on('click', function () {

        var data = this.getAttribute('data-id');
        var ids = data.split('|');
        var target_id = ids[0];
        var optionType = ids[1];
        var obj = this;
        //取消收藏
        if ($(this.childNodes[0]).hasClass("class_collect")) {
            $.ajax({
                type: 'POST',
                url: '/Common/AbortCollect',
                data: { 'target_id': target_id, 'optionType': optionType },
                success: function (d) {
                    var da = eval("(" + d + ")");
                    if (da.status == "1") {
                        var value = parseInt(obj.childNodes[0].innerHTML.split(' ')[1]) - 1;
                        obj.childNodes[0].innerHTML = obj.childNodes[0].innerHTML.split(' ')[0] + " " + value;
                        $(obj.childNodes[0]).toggleClass("class_collect");
                    }
                    else if (da.status == "0") {//失败
                        swal({ title: "Error!", text: "取消收藏失败，请重试!", type: "error", timer: 2000 });
                    }
                    else {
                        window.location = "~/Login/Index";
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    swal({ title: "Error!", text: textStatus, type: "error", timer: 2000 });
                }
            });
        }
            //收藏
        else {
            $.ajax({
                type: 'POST',
                url: '/Common/Collect',
                data: { 'target_id': target_id, 'optionType': optionType },
                success: function (d) {
                    var da = eval("(" + d + ")");
                    if (da.status == "1") {
                        var value = parseInt(obj.childNodes[0].innerHTML.split(' ')[1]) + 1;
                        obj.childNodes[0].innerHTML = obj.childNodes[0].innerHTML.split(' ')[0] + " " + value;
                        $(obj.childNodes[0]).toggleClass("class_collect");
                        //if (target_id == 11 || target_id == 15 || target_id == 16) {
                        //    this.parentNode.nextSibling.nextSibling.style.display = "none";
                        //}
                    }
                    else if (da.status == "0") {//失败
                        swal({ title: "Error!", text: "收藏失败，请重试!", type: "error", timer: 2000 });
                    }
                    else {//用户未登录
                        window.location = "/Login/Index";
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrow) {
                    swal({ title: "Error!", text: textStatus, type: "error", timer: 2000 });
                }
            });
        }
    });
});

