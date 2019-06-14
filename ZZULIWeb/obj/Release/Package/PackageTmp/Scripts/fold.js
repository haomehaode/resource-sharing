
$(function () {
    $('#hidden_top').on("click", function () {
        $(this).css('display', 'none');
        $(this).next().css('display', 'block');
        $(this).next().next().css('display', 'none');
    });
    $('#hidden_bottom').on("click", function () {
        $(this).parent().css('display', 'none');
        $(this).parent().prev().css('display', 'block');
        $(this).parent().prev().prev().css('display', 'none');
    });
    $('#show_all').on("click", function () {
        $(this).parent().css('display', 'none');
        $(this).parent().next().css('display', 'block');
        $(this).parent().prev().css('display', 'block');
    });
});