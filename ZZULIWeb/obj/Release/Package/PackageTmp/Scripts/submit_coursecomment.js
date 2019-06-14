$(function () {

    //提交课程评论
    $(".submit_coursecomment").on("click", function () {
        var arr = $('#submitform').attr('data-id').split("|");
        var id = arr[0];
        var movid = arr[1];
        var content = $('#content').val().toString().trim();
        if (content.length <= 0) {
            swal({ title: "请填写评论内容!", timer: 2000 });
            return;
        }

        $.ajax({
            type: 'post',
            url: '/Common/Submit_CourseComment',
            data: { 'id': id, 'movid': movid, 'content': content },
            success: function (res) {
                var data = eval("(" + res + ")");
                switch (data.status) {
                    case "0":
                        swal({ title: "Error!", text: "您的评论未提交!", timer: 2000, type: "error" });
                        break;
                    case "1":
                        $('#content').val("");
                        swal({ title: "Success!", text: "您的评论已提交!", timer: 2000, type: "success" });
                        break;
                    case "2":
                        window.loaction = "/Login/Index";
                        break;
                }
            },
            error: function (err) {
                swal({ title: "Error!", text: "你的评论未提交!", timer: 2000, type: "error" });
            }
        });
    });

});