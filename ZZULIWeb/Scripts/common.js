$(function () {
    $('.quitLogin').on('click', function () {
        var url = window.location.href;
        alert(url);
        $.ajax({
            type: 'post',
            url: '/Common/Clear',
            data: { },
            success: function (res) {
                alert("fjakjf");
                var data = eval("(" + res + ")");
                if (data.status == "0") {
                    swal({ title: 'Error!', text: '退出失败，请重试!', type: 'error', timer: 2000 });
                }
                window.location.reload();
            },
            error: function (XMLHttpRequest, textStatus, errorThrow) {
                swal({ title: 'Error!', text: textStatus, type: 'error', timer: 2000 });
            }
        });

    });

});
function createXmlHttp() {
    xhr = false;
    try {
        xhr = new ActiveXObject("Msxml2.XMLHTTP");
    } catch (e) {
        try {
            xhr = new ActiveXObject("Microsoft.XMLHTTP");
        } catch (e2) {
            xhr = false;
        }
    }

    if (!xhr && typeof XMLHttpRequest != 'unfefined') {
        xhr = new XMLHttpRequest();
    }
    return xhr;
}