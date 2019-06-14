$(function () {
    //提交问题
    $('.submit_question').on("click", function () {
        var title = $('#title').val().toString().trim();
        if (title.length < 5) {
            $('#title_length_warning').text("标题不能少于5个字!");
            swal({ title: "标题不能少于5个字!", timer: 2000 });
            return;
        }
        var content_style = $('#div1').html().trim().toString().trim();
        var content = $('#div1').text().trim().toString().trim();
        if (content.length <= 0) {
            swal({ title: "请输入您的问题内容!", timer: 2000 });
            return;
        }

        var tags = new Array();
        $('.tags_active').each(function (key, value) {
            tags[key] = $(this).val();
        });
        if (tags.length <= 0) {
            swal({ title: "请为您的问题选择标签!", timer: 2000 });
            return;
        }
        $.ajax({
            type: 'post',
            url: '/Common/Submit_Question',
            data: { 'title': title, 'content': content, 'content_style': content_style, 'tags': tags },
            traditional: true,
            success: function (res) {
                var data = eval("(" + res + ")");
                switch (data.status) {
                    case "0":
                        swal({ title: "Error!", text: "您的问题未提交!", type: "error", timer: 2000 });
                        break;
                    case "1":
                        $('#title').val("");
                        $('#myArea1').text("");
                        swal({ title: "Success!", text: "您的问题已提交!", type: "success", timer: 2000 });
                        break;
                    case "2":
                        window.loaction = "/Login/Index";
                        break;
                }
            },
            error: function (err) {
                swal({ title: "Error!", text: "您的问题未提交!", type: "error", timer: 2000 });
            }
        });
    });

    $('.tags').on('click', function () {
        if ($(this).hasClass('tags_active')) {
            $(this).removeClass('tags_active');
            $(this).attr('name', '');
        }
        else {
            $(this).addClass('tags_active');
            $(this).attr('name', 'tags');
        }
    });

    $('#title').on('blur', function () {
        var title = $('#title').val().toString().trim();
        if (title.length <= 5) {
            $('#title_length_warning').text("标题不能少于5个字!");
        }
    });

    $('#title').on('focus', function () {
        $('#title_length_warning').text("");
    });

})