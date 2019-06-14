
$(function () {
    textHide();
    //even_search();
});
//文字的隐藏
function textHide() {
    var list = gel_class('index_header_form_text');
    for (var i = 0; i < list.length; i++) {
        showHide(list[i]);
    }
};

function showHide(obj) {
    obj.onfocus = function () {
        //alert("fjakfjakfj");
        $(this).toggleClass("search_focus");
        if ($(this).val().toString().trim().length > 0) {
            if ($(this).val().toString().trim() == "请输入搜索内容") {
                $(this).val("");
            }
            else {
                $(this).select();
            }
        }
    };
    obj.onblur = function () {
        //alert("fjakfjakfj
        $(this).toggleClass("search_focus");
        if ($(this).val().toString().trim().length <= 0) {
            $(this).val("请输入搜索内容");
        }
    };
}
function gel_id(id) {
    return document.getElementById(id);
}
function gel_class(c) {
    return document.getElementsByClassName(c);
}

//function even_search() {
//    var list = document.getElementsByClassName('index_header_form_button');
//    var str = document.getElementById('index_header_form_text').value;
//    search(list[0], str);
//}

//function search(obj, str) {
//    obj.onclick = function () {
//        alert(str);
//        window.location = "/Search/s/wd=" + str;
//    };
//}