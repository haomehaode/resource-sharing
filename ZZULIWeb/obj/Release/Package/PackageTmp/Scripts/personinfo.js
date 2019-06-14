$(function () {
    //个人资料的弹出
    (function () {
        $('.person_data p a').click(function () {
            $('.person_data .form1').show();
        });
        $('.person_data .form1 h4 a').click(function () {
            $('.person_data .form1').hide();
        });
        $('.no').click(function () {
            $('.person_data .form1').hide();
        });
    })();

    (function () {
        $('.person_data ul a').click(function () {
            $('.person_data .form2').show();
        });
        $('.person_data .form2 h3 a').click(function () {
            $('.person_data .form2').hide();
        });
        $('#no').click(function () {
            $('.person_data .form2').hide();
        });
    })();

});