$(function () {
    $('.like').on("click", function () {
        var data = this.getAttribute('data-id');
        //var data = $(this).attr('data-id');
        var ids = data.split('|');
        var target_id = ids[0];
        var optionType = ids[1];
        var obj = this;
        //取消点赞
        if ($(this.childNodes[0]).hasClass("class_like")) {
            $.ajax({
                type: 'POST',
                url: '/Common/AbortLike',
                data: { 'target_id': target_id, 'optionType': optionType },
                success: function (d) {
                    var da = eval("(" + d + ")");
                    if (da.status == "1") {
                        var model = $(obj).children(":first");
                        var text = model.text().split(" ")[0];
                        var oldvalue = parseInt(model.text().split(' ')[1]);
                        var newvalue = parseInt(oldvalue - 1);
                        model.text(text + " " + newvalue);
                        model.removeClass("class_like");
                        if (optionType == 11 || optionType == 15 || optionType == 16) {
                            $(obj).parent().next().css('display', 'block');
                        }
                    }
                    else if (da.status == "0") {//失败
                        swal({ title: "Error!", text: "取消点赞失败!", type: "error", timer: 2000 });
                    }
                    else {
                        window.location = "~/Login/Index";
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    swal({ title: "Error!", text: err.textStatus, type: "error", timer: 2000 });
                }
            });
        }
            //点赞
        else {
            $.ajax({
                type: 'POST',
                url: '/Common/Like',
                data: { 'target_id': target_id, 'optionType': optionType },
                success: function (d) {
                    var da = eval("(" + d + ")");
                    if (da.status == "1") {
                        var model = $(obj).children(":first");
                        var text = model.text().split(" ")[0];
                        var oldvalue = parseInt(model.text().split(' ')[1]);
                        var newvalue = parseInt(oldvalue + 1);
                        model.text(text + " " + newvalue);
                        model.addClass("class_like");
                        if (optionType == 11 || optionType == 15 || optionType == 16) {
                            $(obj).parent().next().css('display', 'none');
                        }
                    }
                    else if (da.status == "0") {//失败
                        swal({ title: "Error!", text: "点赞失败，请重试!", type: "error", timer: 2000 });
                    }
                    else {//用户未登录
                        window.location = "/Login/Index";
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    swal({ title: "Error!", text: textStatus, type: "error", timer: 2000 });
                }
            });
        }
    });

    $('.notlike').on("click", function () {
        var data = this.getAttribute('data-id');
        var ids = data.split('|');
        var target_id = ids[0];
        var optionType = ids[1];
        var obj = this;
        //取消踩
        if ($(this.childNodes[0]).hasClass("class_notlike")) {
            $.ajax({
                type: 'POST',
                url: '/Common/AbortNotLike',
                data: { 'target_id': target_id, 'optionType': optionType },
                success: function (d) {
                    var da = eval("(" + d + ")");
                    if (da.status == "1") {
                        var model = $(obj).children(":first");
                        var text = model.text().split(" ")[0];
                        var oldvalue = parseInt(model.text().split(" ")[1]);
                        var newvalue = parseInt(oldvalue - 1);
                        model.text(text + " " + newvalue);
                        //obj.childNodes[0].innerHTML = obj.childNodes[0].innerHTML.split(' ')[0] + " " + value;
                        //$(obj.childNodes[0]).removeClass("class_notlike");
                        model.removeClass("class_notlike");
                        //$(obj.childNodes[0]).css('color', 'black');
                        if (optionType == 18 || optionType == 19 || optionType == 20) {
                            //this.parentNode.previousSibling.previousSibling.style.display = "block";
                            //$(this).parent().prev().prev().css('display', 'block');
                            $(obj).parent().prev().css('display', 'block');
                        }
                    }
                    else if (da.status == "0") {//失败
                        alert("fail");
                    }
                }
            });
        }
            //踩
        else {
            $.ajax({
                type: 'POST',
                url: '/Common/NotLike',
                data: { 'target_id': target_id, 'optionType': optionType },
                success: function (d) {
                    var da = eval("(" + d + ")");
                    if (da.status == "1") {
                        var model = $(obj).children(":first");
                        var text = model.text().split(" ")[0];
                        var oldvalue = parseInt(model.text().split(" ")[1]);
                        var newvalue = parseInt(oldvalue + 1);
                        model.text(text + " " + newvalue);
                        //obj.childNodes[0].innerHTML = obj.childNodes[0].innerHTML.split(' ')[0] + " " + value;
                        //$(obj.childNodes[0]).addClass("class_notlike");
                        model.addClass("class_notlike");
                        //$(obj.childNodes[0]).css('color', 'red');
                        if (optionType == 18 || optionType == 19 || optionType == 20) {
                            //this.parentNode.previousSibling.previousSibling.style.display = "none";
                            //$(this).parent().prev().prev().css('display', 'none');
                            $(obj).parent().prev().css('display', 'none');
                        }

                    }
                    else if (da.status == "0") {//失败
                        alert("fail");
                    }
                    else {//用户未登录
                        window.location = "/Login/Index";
                    }
                }
            });
        }
    })

})