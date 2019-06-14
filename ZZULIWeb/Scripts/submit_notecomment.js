$(function () {
    $('.submit_notecomment').on('click', function () {
        var content = $('#content').val().toString().trim();
        if (content.length <= 0) {
            swal({ title: "请填写评论内容!", text: "", timer: 2000 });
            return;
        }
        var nid = $('#submitform').attr('data-id');
        $.ajax({
            type: 'post',
            url: '/Common/Submit_NoteComment',
            data: { 'nid': nid, 'content': content },
            success: function (res) {
                var data = eval("(" + res + ")");
                if (data.status == "1") {
                    $('#content').val("");
                    swal({ title: 'Success!', text: '评论成功!', type: 'success', timer: 4000 });
                    window.location = "/Note/n/" + nid;
                } else if (data.status == "0") {
                    swal({ title: 'Error!', text: '评论失败!', type: 'error', timer: 2000 });
                } else {
                    window.location = "~/Login/Index";
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrow) {
                swal({ title: 'Error!', text: textStatus, type: 'error' });
            }
        });

    });

});