// JavaScript Document
window.onload = function(){
	var oUl = document.getElementById('list');
	var aH1 = oUl.getElementsByTagName('h1');
	var aUl = oUl.getElementsByTagName('ul');
	var aLi = null;
	var arrLi = [];
	
	for( var i=0; i<aH1.length; i++ ){
		aH1[i].index = i;
		aH1[i].onclick = function (){
			
			for( var i=0; i<aH1.length; i++ ){
				if( i != this.index ){
					aUl[i].style.display = 'none';
					aH1[i].className = '';
				}
			}
			
			if( this.className == '' ){
				aUl[this.index].style.display = 'block';
				this.className = 'active';
			} else {
				aUl[this.index].style.display = 'none';
				this.className = '';
			}
		};
	}
	
	for( var i=0; i<aUl.length; i++ ){
		aLi = aUl[i].getElementsByTagName('li');
		for( var j=0; j<aLi.length; j++ ){
			arrLi.push( aLi[j] );
		}
	}
	
	for( var i=0; i<arrLi.length; i++ ){
		arrLi[i].onclick = function (){
			
			for( var i=0; i<arrLi.length; i++ ){
				if( arrLi[i] !=this ){
					arrLi[i].className = '';
				}
			}
			if( this.className == '' ){
				this.className = 'hover';
			}else{
				this.className = '';
			}
		};
	}	
}