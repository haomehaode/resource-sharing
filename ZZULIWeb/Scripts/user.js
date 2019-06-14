$(function () {
    nav_mouseaction();
});

function nav_mouseaction() {
    //var ul = gel_class('person_content_ul');
    //var li = ul[0].getElementsByTagName('li');
    for (var i = 0; i < li.length; i++) {
        li[i].onmouseover = function () {
            if (this.className != 'person_action') {
                this.getElementsByTagName('a')[0].style.color = '#000';
                this.getElementsByTagName('a')[0].style.opacity = '1';
            }
        };
        li[i].onmouseout = function () {
            if (this.className != 'person_action') {
                this.getElementsByTagName('a')[0].style.color = '#666';
                this.getElementsByTagName('a')[0].style.opacity = '0.5';
            }
        };
    }
}