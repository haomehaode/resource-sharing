// JavaScript Document
//window.onload = function () {
//    show();
//    totop();
//    //prevNext();
//}

$(function () {
    show();
    prevNext();
});


function prevNext() {
    var oNav = document.getElementById('index_nav');
    var oPrev = document.getElementById('index_prev');
    var oNext = document.getElementById('index_next');
    var arrUrl = ['url(/images/index/free1.png)', 'url(/images/index/free2.png)', 'url(/images/index/free3.png)', 'url(/images/index/free4.png)', 'url(/images/index/free5.png)'];
    var num = 0;

    var timer = null;
    autoPlay();
    function autoPlay() {
        timer = setInterval(function () {
            num++;
            num %= arrUrl.length;
            fnTab();
        }, 3000);
    }

    oNav.onmouseover = function () {
        clearTimeout(timer);
    };
    oNav.onmouseout = autoPlay;

    function fnTab() {
        oNav.style.background = arrUrl[num];
    };
    /*fnTab();*/


    oPrev.onclick = function () {
        num--;
        if (num == -1) {
            num = arrUrl.length - 1;
        }
        fnTab();
    };
    oNext.onclick = function () {
        num++;
        if (num == arrUrl.length) {
            num = 0;
        }
        fnTab();
    };
}


//function prevNext() {
//    var oNav = document.getElementById('index_nav');
//    var oPrev = document.getElementById('index_prev');
//    var oNext = document.getElementById('index_next');
//    var arr = ['images/index/free1.png', 'images/index/free2.png', 'images/index/free3.png', 'images/index/free4.png', 'images/index/free5.png'];
//    var num = 0;

//    oPrev.onclick = function () {
//        oNav.style.background = 'url(../images/index/free4.png)';
//    }
//    oNext.onclick = function () {
//        num++;
//        if (num === arr.length)
//            num = 0;
//        oImg.src = arr[num];
//    }
//}

function show() {
    var oNav = document.getElementById('index_nav');
    var aUl = oNav.getElementsByTagName('ul');
    var aLi = aUl[0].getElementsByTagName('li');
    for (var i = 0; i < aLi.length; i++) {
        aLi[i].onmouseover = function () {
            this.getElementsByTagName('div')[0].style.display = 'block';
            this.getElementsByTagName('a')[0].style.opacity = '0.5';
        }
        aLi[i].onmouseout = function () {
            this.getElementsByTagName('div')[0].style.display = 'none';
            this.getElementsByTagName('a')[0].style.opacity = '1';
        }
    }
};






