


function geturl(obj) {
    var data = $(obj).attr("data-id");
    var id = data.split("|")[0];
    var pageIndex = parseInt(data.split("|")[1]);

    var strPageIndex = pageIndex == 0 ? "" : "page_" + pageIndex;

    var stateObject = {};
    var title = "";

    switch ($(obj).attr("id")) {
        case "cc"://课程评论
            var showurl = "/Learn/v/" + id;
            var dataurl = "/Learn/Comment/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
            break;
        case "cq"://课程问题
            var showurl = "/Learn/v/" + id + "/tab_2";
            var dataurl = "/Learn/Question/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
            break;
        case "cn"://课程笔记
            var showurl = "/Learn/v/" + id + "/tab_3";
            var dataurl = "/Learn/Note/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
            break;

        case "uc"://用户学习课程
            var showurl = "/User/Course/" + id;
            var dataurl = "/User/LearnedCourse/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
            break;
        case "ucn"://用户课程笔记
            var showurl = "/User/Course/" + id + "/tab_2";
            var dataurl = "/User/cn/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
            break;
        case "ucq"://用户课程问题
            var showurl = "/User/Course/" + id + "/tab_3";
            var dataurl = "/User/cq/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
            break;
        case "ucc"://用户课程收藏
            var showurl = "/User/Course/" + id + "/tab_4";
            var dataurl = "/User/collect/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
            break;
        case "uq"://用户问题
            var showurl = "/User/Question/" + id;
            var dataurl = "/User/q/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
            break;
        case "ua"://用户回答
            var showurl = "/User/Question/" + id + "/tab_2";
            var dataurl = "/User/an/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
        case "uqc"://用户问题收藏
            var showurl = "/User/Question/" + id + "/tab_3";
            var dataurl = "/User/qc/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
            break;
        case "un":
            var showurl = "/User/Note/" + id;
            var dataurl = "/User/n/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
            break;
        case "unc":
            var showurl = "/User/Note/" + id + "/tab_2";
            var dataurl = "/User/nc/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
            break;
        case "unr":
            var showurl = "/User/Note/" + id + "/tab_3";
            var dataurl = "/User/nr/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
            break;
        case "uncollect":
            var showurl = "/User/Note/" + id + "/tab_4";
            var dataurl = "/User/notecollect/" + id + "/" + strPageIndex;
            history.replaceState(stateObject, title, showurl);
            return dataurl;
            break;
        case "follow":
            var dataurl = "/User/Follow/" + id + "/" + strPageIndex;
            return dataurl;
            break;
    }
}

function replacecontent(url) {
    $.ajax({
        type: 'POST',
        url: url,
        data: {},
        success: function (d) {
            $('#replace').html(d);
        }
    });
}

$(function () {
    $('.ajax_goto').on("click", function () {
        $('#replace').text("");
        $('#content-null').css('display', 'block');
        var dataurl = geturl(this);
        replacecontent(dataurl);
        $('#content-null').css('display', 'none');
    })
});