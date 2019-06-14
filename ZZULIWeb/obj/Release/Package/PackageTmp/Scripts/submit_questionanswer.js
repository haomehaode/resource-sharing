$(function () {
    $('.submit_questionanswer').on('click', function () {
        var content = $('#content').val().toString().trim();
        alert(content);
        if (content.length <= 0) {
            swal({ title: "请填写回复内容!", timer: 2000 });
            return;
        }
        var qid = $("#submitform").attr('data-id');
        alert(qid);
        $.ajax({
            type: 'post',
            url: '/Common/Submit_QuestionAnswer',
            data: { 'qid': qid, 'content': content },
            success: function (res) {
                var data = eval("(" + res + ")");
                if (data.status == "1") {
                    swal({ title: 'Success!', text: '回复成功!', type: 'success', timer: 2000 });
                    window.location = "/Question/des/" + qid;
                } else if (data.status == "0") {
                    swal({ title: 'Error!', text: '回复失败!', type: 'error', timer: 2000 });
                } else {
                    window.location = "~/Login/Index";
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrow) {
                swal({ title: "Error!", text: textStatus, type: 'error', timer: 2000 });
            }
        });
    });

    $('.notes_answer').on('click', function () {
        var data = $(this).attr('data-id');
        var datas = data.split('|');
        qid = datas[0];
        aid = datas[1];

        $('#question_particulars_content_bottom_form').show();
    });
    $('.question_particulars_content_bottom_form_no').on('click', function () {
        $('#answercontent').val("");
        $('#question_particulars_content_bottom_form').hide();
    });

    $('#submit_questionanswerofanswer').on('click', function () {
        alert(qid + ":" + aid);
        if (qid == "" || aid == "") {
            swal({ title: "Error!", text: "系统错误!", type: "error", timer: 2000 });
            return;
        }
        var content = $('#answercontent').val().toString().trim();
        alert(content);
        if (content.length <= 0) {
            swal({ title: "请输入回复内容!", timer: 2000 });
            return;
        }
        $.ajax({
            type: 'post',
            url: '/Common/Submit_QuestionAnswerOfAnswer',
            data: { 'qid': qid, 'aid': aid, 'content': content },
            success: function (res) {
                var data = eval("(" + res + ")");
                if (data.status == "1") {
                    $('.question_particulars_content_bottom_form_no').click();
                    swal({ title: "Success!", text: '回复成功!', type: 'success', timer: 2000 });
                    window.location = "/Question/des/" + qid;
                } else if (data.status == "0") {
                    swal({ title: "Error!", text: '回复失败，请重试!', type: 'error', timer: 2000 });
                } else {
                    window.location = "~/Login/Index";
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrow) {
                swal({ title: "Error!", text: textStatus, type: 'error', timer: 2000 });
            }
        });
    });
    //out($('.notes_answer'), $('#notes_particulars_content_bottom_form'), $('.notes_particulars_content_bottom_form_no'));
    //out($('.notes_answer'), $('#question_particulars_content_bottom_form'), $('.question_particulars_content_bottom_form_no'));
});
var qid, aid;


//function out(obj1, obj2, obj3) {
//    obj1.click(function () {
//        obj2.show();
//    });
//    obj3.click(function () {
//        obj2.hide();
//    })
//};

