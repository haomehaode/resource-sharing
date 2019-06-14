// JavaScript Document
$(function () {

    //登录框的弹出
    (function () {
        $('.denglu').click(function () {

            var oLogin = $('<div id="login"><ul><li class="login_active"><a href="#" style="color:red;" >登录</a></li><li><a href="#">注册</a></li></ul><form><input class="login_txt" type="text" value="请输入登录邮箱/手机号" /><input class="login_pass" type="text" value="6-18位密码，区分大小写，不能用空格"/><input class="login_ch" type="checkbox" checked /><p>下次自动登录</p><span><a href="#">忘记密码</a></span>	<strong><a href="#">登录</a></strong></form><div id="close">X</div></div>');

            $('body').append(oLogin);
            $('#login').css('background', '#fff');

            oLogin.css('left', ($(window).width() - oLogin.outerWidth()) / 2);
            oLogin.css('top', ($(window).height() - oLogin.outerHeight()) / 2);

            $('#close').click(function () {

                oLogin.remove();

            });


            $(window).on('resize scroll', function () {

                oLogin.css('left', ($(window).width() - oLogin.outerWidth()) / 2);
                oLogin.css('top', ($(window).height() - oLogin.outerHeight()) / 2 + $(window).scrollTop());

            });

        });
    })();

    (function () {
        $('.zhuce').click(function () {

            var oLogin = $('<div id="login"><ul><li><a href="#">登录</a></li><l class="login_active"i><a href="#" style="color:red;">注册</a></li></ul><form><input class="login_txt" type="text" value="请输入登录邮箱/手机号" /><input class="login_pass" type="text" value="6-18位密码，区分大小写，不能用空格"/><input class="login_ch" type="checkbox" checked /><p>下次自动登录</p><span><a href="#">忘记密码</a></span>	<strong><a href="#">登录</a></strong></form><div id="close">X</div></div>');

            $('body').append(oLogin);
            $('#login').css('background', '#fff');

            oLogin.css('left', ($(window).width() - oLogin.outerWidth()) / 2);
            oLogin.css('top', ($(window).height() - oLogin.outerHeight()) / 2);


            $('#close').click(function () {

                oLogin.remove();

            });


            $(window).on('resize scroll', function () {

                oLogin.css('left', ($(window).width() - oLogin.outerWidth()) / 2);
                oLogin.css('top', ($(window).height() - oLogin.outerHeight()) / 2 + $(window).scrollTop());

            });

        });
    })();



});