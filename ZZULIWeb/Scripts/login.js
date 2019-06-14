var w, h, className;
function getSrceenWH() {
    w = $(window).width();
    h = $(window).height();
    $('#dialogBg').width(w).height(h);
}

window.onresize = function () {
    getSrceenWH();
}
$(window).resize();
var res = false;

function checkAccount() {
    if ($('#User_Account').val().toString().trim().length <= 0) {
        $('.dialogIco').attr("src", "/images/class/header.png");
        res = false;
        $('#valid-account').css('display', 'block').text("请输入用户名！");
        return;
    } else if ($('#User_Account').val().toString().trim().length < 4 || $('#User_Account').val().toString().trim().length > 16) {
        res = false;
        $('#valid-account').css('display', 'block').text("用户名必须为4-16位！");
        return;
    } else {
        $.ajax({
            type: 'POST',
            url: "/Login/GetHeader",
            data: { "account": $('#User_Account').val().toString().trim() },
            success: function (data) {
                var json = (new Function("", "return " + data))();
                if (json.status == "1") {
                    $('#valid-account').css('display', 'none');
                    res = true;
                    $('.dialogIco').attr("src", "/images/userImg/" + json.u_id + ".png");
                }
                else {
                    $('.dialogIco').attr("src", "/images/class/header.png");
                    res = false;
                    $('#valid-account').css('display', 'block').text("该用户不存在！");
                }
            },
            error: function () {
                res = false;
                swal("系统异常！");
            },
            dataType: 'json'
        });
    }
}
function checkPwd() {
    if ($('#User_Password').val().length <= 0) {
        res = false;
        $('#valid-pwd').css('display', 'block').text("请输入登录密码！");
        return;
    } else if ($('#User_Password').val().length < 6 || $('#User_Password').val().length > 16) {
        res = false;
        $('#valid-pwd').css('display', 'block').text("密码必须为6-16位！");
        return;
    } else {
        res = true;
        $('#valid-pwd').css('display', 'none');
    }
}

$(function () {
    getSrceenWH();

    //显示弹框
    //$('.box a').click(function () {
    className = $('.box a').attr('class');
    className = $(this).attr('class');
    $('#dialogBg').fadeIn(100);
    $('#dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
    //});

    //关闭弹窗
    $('.claseDialogBtn').click(function () {
        $('#dialogBg').fadeOut(100, function () {
            $('#dialog').addClass('bounceOutUp').fadeOut();
        });
        var url = document.referrer.replace("http://", "");
        var index = url.indexOf('/');
        url = url.substr(index);
        if (document.referrer == window.location.href || url == "/Login/Register") {
            window.location = "/Home/Index";
            return;
        }
        else {
            window.location = document.referrer;
        }
    });

    $('#User_Account').blur(function () {
        checkAccount();
    });

    //$('#User_Account').keyup(function () {
    //    $('#User_Account').val($('#User_Account').val().replace(/[^\d]/g, ''));
    //})
    $('#User_Password').blur(function () {
        checkPwd();
    });


    $('.submitBtn').click(function () {
        checkAccount();
        checkPwd();
        if (res) {
            $.ajax({
                type: "POST",
                url: "/Login/ValidateLogin",
                data: { "account": $('#User_Account').val().toString().trim(), "pwd": $('#User_Password').val(), "repwd": $('#repwd').is(':checked') },
                success: function (data) {
                    data = (new Function("", "return " + data))();
                    if (data.status == "1") {
                        $('.claseDialogBtn').click();
                        if (document.referrer == window.location.href) {
                            window.location = "/Home/Index";
                        }
                        else {
                            window.location = document.referrer;
                        }
                    }
                    else {
                        swal("密码错误！", "", "error");
                    }
                },
                error: function () {
                    alert("登录过程出错！");
                },
                dataType: 'json'
            });
        }
        else {
        }
    });
});