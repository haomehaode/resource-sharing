$(function () {
    totop();
});
function getStyle(obj, attr) { return obj.currentStyle ? obj.currentStyle[attr] : getComputedStyle(obj)[attr]; }

function totop() {
    var oDiv1 = document.getElementsByClassName('a index_content_ul_div');
    for (var i = 0; i < oDiv1.length; i++) {
        totop1(oDiv1[i]);
    }

    function totop1(obj) {
        obj.parentNode.onmouseover = function () {
            doMove(obj, 20, 120);
        }
        obj.parentNode.onmouseout = function () {
            doMove(obj, -20, 80);
        }
    }

    function doMove(obj, dir, target) {
        clearInterval(obj.timer);

        obj.timer = setInterval(function () {

            var speed = parseInt(getStyle(obj, 'height')) + dir;			// 步长

            if (speed > target && dir > 0) {		// 往前跑
                speed = target;
            }

            if (speed < target && dir < 0) {		// 往后跑
                speed = target;
            }

            obj.style.height = speed + 'px';

            if (speed == target) {
                clearInterval(obj.timer);
            }

        }, 120);
    }

};