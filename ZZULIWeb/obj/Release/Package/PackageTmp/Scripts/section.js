window.onload = function () {
    qq();
    learn_goto();
}
function qq() {
    var oUl = document.getElementById('list');
    var aH2 = oUl.getElementsByTagName('h2');
    var aUl = oUl.getElementsByTagName('ul');
    var aLi = null;
    var arrLi = [];

    for (var i = 0; i < aH2.length; i++) {
        aH2[i].index = i;
        aH2[i].onclick = function () {


            if (this.className == '') {
                aUl[this.index].style.display = 'block';
                this.className = 'active';
            } else {
                aUl[this.index].style.display = 'none';
                this.className = '';
            }

        };
    }

    for (var i = 0; i < aUl.length; i++) {
        aLi = aUl[i].getElementsByTagName('li');
        for (var j = 0; j < aLi.length; j++) {
            arrLi.push(aLi[j]);
        }
    }

    for (var i = 0; i < arrLi.length; i++) {
        arrLi[i].onmouseover = function () {
            for (var i = 0; i < arrLi.length; i++) {
                if (arrLi[i] != this) {
                    arrLi[i].className = '';
                }
            }
            if (this.className == '') {
                this.className = 'hover';
            } else {
                this.className = '';
            }
        };
    }
};

function learn_goto() {
    $('.learn_goto').on("click", function () {
        var moveid = $(this).attr('data-id');
        window.location = "/Learn/v/" + moveid;
    });
}
