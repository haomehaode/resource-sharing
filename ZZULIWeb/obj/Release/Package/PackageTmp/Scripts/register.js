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
    if ($('#User_Account').val().toString().trim().length > 0) {
        if ($('#User_Account').val().toString().trim().length < 4 || $('#User_Account').val().toString().trim().length > 16) {
            res = false;
            $('#valid-account').css('display', 'block').text("用户名必须为4-16位！");
        } else {
            $.ajax({
                type: 'POST',
                url: "/Login/AccountIsExist",
                data: { "account": $('#User_Account').val().toString().trim() },
                success: function (data) {
                    var json = (new Function("", "return " + data))();
                    if (json.status == "1") {
                        res = false;
                        $('#valid-account').css('display', 'block').text("该用户已存在！");
                    }
                    else {
                        res = true;
                        $('#valid-account').css('display', 'none');
                    }
                },
                error: function () {
                    res = false;
                    swal("系统异常！", "", "error");
                },
                dataType: 'json'
            });
        }
    } else {
        res = false;
        $('#valid-account').css('display', 'block').text("请输入用户名！");
    }
}
function checkPwd() {
    if ($('#User_Password').val().length <= 0) {
        res = false;
        $('#valid-pwd').css('display', 'block').text("请输入登录密码！");
    } else if ($('#User_Password').val().length < 6 || $('#User_Password').length > 16) {
        res = false;
        $('#valid-pwd').css('display', 'block').text("密码长度必须为6-16位！");
    } else {
        res = true;
        $('#valid-pwd').css('display', 'none');
    }
}
function checkPwd2() {
    if ($('#pwd2').val().length <= 0) {
        res = false;
        $('#valid-pwd2').css('display', 'block').text("请输入确认密码！");
    } else if ($('#pwd2').val().length < 6 || $('#pwd2').val().length > 16) {
        res = false;
        $('#valid-pwd2').css('display', 'block').text("密码长度必须为6-16位！");
    }
    else if ($('#pwd2').val() != $('#User_Password').val()) {
        res = false;
        $('#valid-pwd2').css('display', 'block').text("输入密码不一致！");
    } else {
        res = true;
        $('#valid-pwd2').css('display', 'none');
    }
}
function checkValidationCode() {
    if ($('#validationCode').val().toString().trim().length <= 0) {
        res = false;
        $('#valid-validCode').css('display', 'block').text("请输入验证码！");
    }
    else if ($('#validationCode').val().toString().trim().length < 6) {
        res = false;
        $('#valid-validCode').css('display', 'block').text("验证码输入错误，请重新输入！");
    }
    else {
        $.ajax({
            type: 'POST',
            url: "/Login/ValidValidationCode",
            data: { "ValidationCode": $('#validationCode').val().toString().trim() },
            dataType: 'json',
            success: function (data) {
                var json = (new Function("", "return " + data))();
                if (json.status == "1") {
                    res = true;
                    $('#valid-validCode').css('display', 'none');
                } else {
                    res = false;
                    $('#img-valid-code').attr('src', '/Login/GetValidationCode?i=' + new Date());
                    $('#valid-validCode').css('display', 'block').text("验证码输入错误，请重新输入！");
                }
            },
            error: function () {
                res = false;
                swal("系统异常！", "", "error");
            }
        });
    }
}
$(function () {
    getSrceenWH();

    //显示弹框
    //$('.box a').click(function () {
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
        if (document.referrer == window.location.href || url == "/Login" || url == "/Login/Index") {
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

    $('#User_Account').keyup(function () {
        //$('#User_Account').val($('#User_Account').val().replace(/[^\d]/g, ''));
    })

    $('#User_Password').blur(function () {
        checkPwd();
    });

    $('#pwd2').blur(function () {
        checkPwd2();
    });

    $('#validationCode').blur(function () {
        checkValidationCode();
    });

    $('#validationCode').focus(function () {
        $('#img-valid-code').css('display', 'block');
    });
    //$('#validationCode').blur(function () {
    //    $('#img-valid-code').css('display', 'none');
    //})
    $('#img-valid-code').click(function () {
        $('#validationCode').val("");
        $('#img-valid-code').attr('src', '/Login/GetValidationCode?i=' + new Date());
    })

    $('.submitBtn').click(function () {
        checkAccount();
        checkPwd();
        checkPwd2();
        checkValidationCode();
        //$('#User_Account').blur();
        //$('#User_Password').blur();
        //$('#pwd2').blur();
        //if ($('#confirmpwd').val().toString().trim().length <= 0) {
        //    $('#confirmpwd').val("请输入确认密码！");
        //    return;
        //}
        //if ($('#User_Account').val().toString().trim().length <= 0 || $('#User_Password').val().toString().trim().length <= 0) {
        //    swal("请输入正确的用户名和密码！", "", "error");
        //} else {
        if (res) {
            $.ajax({
                type: "POST",
                url: "/Login/DoRegister",
                data: { "account": $('#User_Account').val().toString().trim(), "pwd": $('#User_Password').val() },
                success: function (data) {
                    data = (new Function("", "return " + data))();
                    if (data.status == "1") {
                        swal("注册成功！", "", "success");
                        if (document.referrer == window.location.href) {
                            window.location = "/Home/Index";
                            return;
                        }
                        else {
                            window.location = document.referrer;
                        }

                    }
                    else {
                        swal("注册失败！", "", "error");
                    }
                },
                error: function () {
                    alert("注册过程出错！");
                },
                dataType: 'json'
            });
        }
        //}
    });

    //$('#pwd2').blur(function () {
    //    if ($('#User_Password').val().toString().trim().length <= 0) {
    //        $('#confirmpwd').hide();
    //    }
    //    else if ($('#User_Password').val().toString().trim() != $('#comfirmpwd').val().toString().trim()) {
    //        $('#confirmpwd').val("密码不一致22！");
    //        $('#confirmpwd').show();
    //    }
    //    else {
    //        $('#confirmpwd').hide();
    //    }
    //});
});