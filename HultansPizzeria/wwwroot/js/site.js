// Write your JavaScript code.
$(document).ready(function () {
    $('ul.tabs').tabs({
        swipeable: true
    });
    $('.modal').modal();
    $('.collapsible').collapsible();
    $(".button-collapse").sideNav();
    $('.tooltipped').tooltip({ delay: 50 });

});

//Sticky navigation 
var mn = $(".main-nav");
var m = $(".page-content");
mns = "main-nav-scrolled";
mmt = "main-margin-top";
hdr = $('.header').height();


$(window).scroll(function () {
    if ($(this).scrollTop() > hdr) {
        mn.addClass(mns);
        m.addClass(mmt);
    } else {
        mn.removeClass(mns);
        m.removeClass(mmt);
    }
});
