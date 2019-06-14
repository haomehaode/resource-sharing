$(function () {
    $('.submit_coursequestion').on('click', function () {
        var data = $('.submitform').attr('data-id');
        var datas = data.split('|');
        var cid = datas[0];
        var mid = datas[1];

        var title = $('#title').val().toString().trim();
        if (title.length <= 5) {
            swal({ title: "问题标题长度应不小于5!", timer: 2000 });
            return;
        }
        var content = $('#content').val().toString().trim();
        $.ajax({
            type: 'post',
            url: '/Common/Submit_CourseQuestion',
            data: { 'cid': cid, 'mid': mid, 'title': title, 'content': content },
            success: function (res) {
                var data = eval("(" + res + ")");
                if (data.status == "1") {
                    $('#title').val("");
                    $('#content').val("");
                    swal({ title: 'Success!', text: '问题提交成功!', type: 'success', timer: 2000 });
                } else if (data.status == "0") {
                    swal({ title: 'Error!', text: '问题未提交!', type: 'error', timer: 2000 });
                } else {
                    window.location = "/Login/Index";
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrow) {
                swal({ title: 'Error!', text: textStatus, type: 'error', timer: 2000 });
            }
        });
    });
});