$(function () {

    //提交课程笔记
    $('.submit_coursenote').on("click", function () {
        var arr = $('#submitform').attr('data-id').split("|");
        var id = arr[0];
        var movid = arr[1];
        var content = $('#content').val().trim();
        if (content == "") {
            swal({ title: "请填写笔记内容!", timer: 2000 });
            return;
        }
        $.ajax({
            type: 'post',
            url: '/Common/Submit_CourseNote',
            data: { 'id': id, 'movid': movid, 'content': content },
            success: function (res) {
                var data = eval("(" + res + ")");
                switch (data.status) {
                    case "0":
                        swal({ title: "Error!", text: "您的笔记未提交!", timer: 2000, type: "error" });
                        break;
                    case "1":
                        $('#content').text("");
                        //$('.tab_active').click();
                        swal({ title: "Success!", text: "您的笔记已提交!", timer: 2000, type: "success" });
                        break;
                    case "2":
                        window.location = "/Login/Index";
                        break;
                }
            },
            error: function (err) {
                swal({ title: "Error!", text: "您的笔记未提交!", timer: 2000, type: "error" });
            }
        });

    });


});